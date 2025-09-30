using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.ModuleRepository;
using Hinet.Repository.OperationRepository;
using Hinet.Repository.UserOperationRepository;
using Hinet.Service.Common;
using Hinet.Service.UserOperationService.Dto;
using log4net;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Hinet.Service.UserOperationService
{
    public class UserOperationService : EntityService<UserOperation>, IUserOperationService
    {
        private IUnitOfWork _unitOfWork;
        private IUserOperationRepository _UserOperationRepository;
        private ILog _loger;
        private IMapper _mapper;
        private IModuleRepository _moduleRepository;
        private IOperationRepository _operationRepository;

        public UserOperationService(IUnitOfWork unitOfWork,
        IUserOperationRepository UserOperationRepository,
        IOperationRepository operationRepository,
        IModuleRepository moduleRepository,

        ILog loger,

                IMapper mapper
            )
            : base(unitOfWork, UserOperationRepository)
        {
            _operationRepository = operationRepository;
            _moduleRepository = moduleRepository;
            _unitOfWork = unitOfWork;
            _UserOperationRepository = UserOperationRepository;
            _loger = loger;
            _mapper = mapper;
        }

        public PageListResultBO<UserOperationDto> GetDaTaByPage(UserOperationSearchDto searchModel, int pageIndex = 1, int pageSize = 10)
        {
            var query = from UserOperationtbl in _UserOperationRepository.GetAllAsQueryable()

                        select new UserOperationDto
                        {
                            IsAccess = UserOperationtbl.IsAccess,
                            CreatedDate = UserOperationtbl.CreatedDate,
                            UpdatedDate = UserOperationtbl.UpdatedDate,
                            Id = UserOperationtbl.Id,
                            UserId = UserOperationtbl.UserId,
                            OperationId = UserOperationtbl.OperationId,
                            CreatedID = UserOperationtbl.CreatedID,
                            UpdatedID = UserOperationtbl.UpdatedID,
                            CreatedBy = UserOperationtbl.CreatedBy,
                            UpdatedBy = UserOperationtbl.UpdatedBy
                        };

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                {
                    query = query.OrderBy(searchModel.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Id);
            }
            var resultmodel = new PageListResultBO<UserOperationDto>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = dataPageList;
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = dataPageList.ToList();
            }
            return resultmodel;
        }

        public UserOperation GetById(long id)
        {
            return _UserOperationRepository.GetById(id);
        }

        /// <summary>
        /// Lấy cấu hình quyền người dùng cá nhân
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ModuleService.DTO.ModuleDTO> GetConfigureOperation(long userID)
        {
            var queryAllModules = _moduleRepository.GetAllAsQueryable();
            var queryAllOperations = _operationRepository.GetAllAsQueryable();
            var queryUserOperation = _UserOperationRepository.GetAllAsQueryable().Where(x => x.UserId == userID);
            var GroupModules = (from module in queryAllModules.OrderBy(x => x.Order)
                                join operation in queryAllOperations
                                on module.Id equals operation.ModuleId
                                into groupModuleOperation
                                select new ModuleService.DTO.ModuleDTO()
                                {
                                    Id = module.Id,
                                    Name = module.Name,
                                    Order = module.Order,
                                    GroupOperations = groupModuleOperation.Select(y => new OperationService.DTO.OperationDTO()
                                    {
                                        Id = y.Id,
                                        Name = y.Name,
                                        IsAccess = queryUserOperation.Where(x => x.OperationId == y.Id && x.IsAccess > 0).Any()
                                    }).AsEnumerable()
                                }).ToList();

            return GroupModules;
        }
    }
}