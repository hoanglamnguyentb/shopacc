using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.ModuleRepository;
using Hinet.Repository.OperationRepository;
using Hinet.Repository.RoleOperationRepository;
using Hinet.Repository.RoleRepository;
using Hinet.Service.ModuleService.DTO;
using Hinet.Service.OperationService.DTO;
using Hinet.Service.RoleOperationService.DTO;
using log4net;
using System.Linq;

namespace Hinet.Service.RoleOperationService
{
    public class RoleOperationService : EntityService<RoleOperation>, IRoleOperationService
    {
        private IRoleOperationRepository _roleOperationRepository;
        private IModuleRepository _moduleRepository;
        private IRoleRepository _roleRepository;
        private IOperationRepository _operationRepository;
        private ILog _ilog;

        public RoleOperationService(IUnitOfWork unitOfWork,
            IRoleOperationRepository roleOperationRepository,
            IModuleRepository moduleRepository,
            IRoleRepository roleRepository,
            IOperationRepository operationRepository,
            ILog logger)

            : base(unitOfWork, roleOperationRepository)
        {
            _roleOperationRepository = roleOperationRepository;
            _moduleRepository = moduleRepository;
            _roleRepository = roleRepository;
            _operationRepository = operationRepository;
            _ilog = logger;
            logger.Info("");
        }

        /// <summary>
        /// Lấy danh sách module có cho phép phân quyền theo phạm vi
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleOperationDTO GetConfigureProvince(int roleId)
        {
            var queryRoleOperation = this._roleOperationRepository.GetAllAsQueryable()
                .Where(x => x.RoleId == roleId);
            var queryAllModules = this._moduleRepository.GetAllAsQueryable().Where(x => x.AllowFilterScope == true);
            var queryAllOperations = this._operationRepository.GetAllAsQueryable();

            var result = (from role in this._roleRepository.GetAllAsQueryable()
                          .Where(x => x.Id == roleId)
                          select new RoleOperationDTO()
                          {
                              RoleId = role.Id,
                              RoleName = role.Name,
                              GroupModules = (from module in queryAllModules
                                              join operation in queryAllOperations
                                              on module.Id equals operation.ModuleId
                                              into groupModuleOperation
                                              select new ModuleDTO()
                                              {
                                                  Id = module.Id,
                                                  Name = module.Name,
                                              }).AsEnumerable(),
                          }).FirstOrDefault();
            return result;
        }

        public RoleOperationDTO GetConfigureProvinceJoinTinhHuyenXa(int roleId)
        {
            var queryRoleOperation = this._roleOperationRepository.GetAllAsQueryable()
                .Where(x => x.RoleId == roleId);
            var queryAllModules = this._moduleRepository.GetAllAsQueryable().Where(x => x.AllowFilterScope == true);
            var queryAllOperations = this._operationRepository.GetAllAsQueryable();

            //var queryConfigureProvince = this._configureProvinceRepository.GetAllAsQueryable().Where(x => x.ChucNangId == roleId);

            var result = (from role in this._roleRepository.GetAllAsQueryable().Where(x => x.Id == roleId)

                          select new RoleOperationDTO()
                          {
                              RoleId = role.Id,
                              RoleName = role.Name,
                              GroupModules = (from module in queryAllModules
                                              join operation in queryAllOperations
                                              on module.Id equals operation.ModuleId
                                              into groupModuleOperation

                                              //join configureProvince in queryConfigureProvince
                                              //  on module.Id equals configureProvince.VaitroId into groupConfigureProvince
                                              //from configureProvincetbl in groupConfigureProvince.DefaultIfEmpty()

                                              select new ModuleDTO()
                                              {
                                                  Id = module.Id,
                                                  Name = module.Name
                                                  //MaTinh = configureProvincetbl.MaTinh,
                                                  //MaHuyen = configureProvincetbl.MaHuyen,
                                                  //MaXa = configureProvincetbl.MaXa
                                              }).AsEnumerable(),
                          }).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// @author:duynn
        /// @description: lấy danh sách phân quyền vai trò
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleOperationDTO GetConfigureOperation(int roleId)
        {
            var queryRoleOperation = this._roleOperationRepository.GetAllAsQueryable()
                .Where(x => x.RoleId == roleId);
            var queryAllModules = this._moduleRepository.GetAllAsQueryable();
            var queryAllOperations = this._operationRepository.GetAllAsQueryable();

            var result = (from role in this._roleRepository.GetAllAsQueryable()
                          .Where(x => x.Id == roleId)
                          select new RoleOperationDTO()
                          {
                              RoleId = role.Id,
                              RoleName = role.Name,
                              GroupModules = (from module in queryAllModules.OrderBy(x => x.Order)
                                              join operation in queryAllOperations
                                              on module.Id equals operation.ModuleId
                                              into groupModuleOperation
                                              select new ModuleDTO()
                                              {
                                                  Id = module.Id,
                                                  Name = module.Name,
                                                  Order = module.Order,
                                                  GroupOperations = groupModuleOperation.Select(y => new OperationDTO()
                                                  {
                                                      Id = y.Id,
                                                      Name = y.Name,
                                                      IsAccess = queryRoleOperation.Where(x => x.OperationId == y.Id && x.IsAccess > 0).Any()
                                                  }).AsEnumerable()
                                              }).AsEnumerable()
                          }).FirstOrDefault();
            return result;
        }
    }
}