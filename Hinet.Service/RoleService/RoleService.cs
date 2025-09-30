using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.RoleRepository;
using Hinet.Service.Common;
using Hinet.Service.RoleService.DTO;
using log4net;
using PagedList;
using System.Linq;
using System.Linq.Dynamic;

namespace Hinet.Service.RoleService
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        private IRoleRepository _roleRepository;
        private ILog _iLog;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository, ILog logger) :
            base(unitOfWork, roleRepository)
        {
            _roleRepository = roleRepository;
            _iLog = logger;
            _iLog.Info("Khởi tạo RoleService");
        }

        public PageListResultBO<RoleDTO> GetDataByPage(RoleSearchDTO searchParams, int pageIndex = 1, int pageSize = 20)
        {
            var queryResult = (from module in this._roleRepository.GetAllAsQueryable()
                               select new RoleDTO()
                               {
                                   Id = module.Id,
                                   Name = module.Name,
                                   Code = module.Code
                               });
            if (searchParams != null)
            {
                if (!string.IsNullOrEmpty(searchParams.QueryName))
                {
                    searchParams.QueryName = searchParams.QueryName.Trim().ToLower();
                    queryResult = queryResult.Where(x => x.Name.Trim().ToLower().Contains(searchParams.QueryName));
                }

                if (!string.IsNullOrEmpty(searchParams.QueryCode))
                {
                    searchParams.QueryCode = searchParams.QueryCode.Trim().ToLower();
                    queryResult = queryResult.Where(x => x.Code.Trim().ToLower().Contains(searchParams.QueryCode));
                }

                if (!string.IsNullOrEmpty(searchParams.sortQuery))
                {
                    queryResult = queryResult.OrderBy(searchParams.sortQuery);
                }
                else
                {
                    queryResult = queryResult.OrderByDescending(x => x.Id);
                }
            }
            else
            {
                queryResult = queryResult.OrderByDescending(x => x.Id);
            }

            var result = new PageListResultBO<RoleDTO>();
            if (pageSize == -1)
            {
                var pagedList = queryResult.ToList();
                result.Count = pagedList.Count;
                result.TotalPage = 1;
                result.ListItem = pagedList;
            }
            else
            {
                var dataPageList = queryResult.ToPagedList(pageIndex, pageSize);
                result.Count = dataPageList.TotalItemCount;
                result.TotalPage = dataPageList.PageCount;
                result.ListItem = dataPageList.ToList();
            }
            return result;
        }

        public int? GetIdByCode(string code)
        {
            return _roleRepository.GetAllAsQueryable().Where(x => x.Code == code).Select(x => x.Id).FirstOrDefault();
        }
    }
}