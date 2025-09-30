using Autofac;
using AutoMapper;
using CommonHelper.String;
using Hinet.API2.Core;
using Hinet.Service;
using Hinet.Service.AppUserService;
using Hinet.Service.ConfigRequestService;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Hinet.API2.Controllers
{
    [System.Web.Http.RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IComponentContext _componentContext;
        private IConfigRequestService _configRequestService;
        private IAppUserService _appUserService;

        public TestController(IMapper mapper,
            IComponentContext componentContext,
            IConfigRequestService configRequestService,
            IAppUserService appUserService)
        {
            _mapper = mapper;
            this._componentContext = componentContext;
            _configRequestService = configRequestService;
            _appUserService = appUserService;
        }

        private async Task<bool> CheckAccessApi(string role)
        {
            var Icheck = _configRequestService.FindBy(x => x.Code == role).Any();

            return Icheck;
        }

        [HttpGet]
        [Route("get-all")]
        [AuthorAPI]
        public async Task<IHttpActionResult> GetAll(string name)
        {
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            //var userInfo = _appUserService.GetDtoById(CurrentUserId);
            var convertName = _configRequestService.GetByCode(name);
            var check = await CheckAccessApi((name + "-GetAll"));
            if (check == false)
            {
                return BadRequest("Không có quyền");
            }
            Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
            var assemblyName = assembly.GetReferencedAssemblies().FirstOrDefault(x => x.Name == "Hinet.Model");
            Assembly assembly1 = Assembly.Load(assemblyName);
            Type objectType = assembly1.GetTypes()
                .Where(x => x.Namespace == "Hinet.Model.Entities").Where(x => x.Name == name).FirstOrDefault();

            object obj = Activator.CreateInstance(objectType);
            var service = _componentContext.ResolveNamed<IService>($"{name}Service");
            //Lấy method trong service

            IEnumerable<object> list = null;
            List<string> listString = new List<string>();
            var checkAccessInfor = _configRequestService.FindBy(x => x.Code.Equals((name + "-GetAll"))).FirstOrDefault();

            if (checkAccessInfor != null)
            {
                listString = checkAccessInfor.AccessInfor.Split(',').ToList();
            }
            var result = new List<Dictionary<string, object>>();
            var getAllMethod = service.GetType().GetMethod("GetAll");
            list = getAllMethod.Invoke(service, null) as IEnumerable<object>;
            var props = objectType.GetProperties();
            foreach (var item in list)
            {
                var data = new Dictionary<string, object>();
                foreach (var access in listString)
                {
                    var prop = props.FirstOrDefault(x => x.Name == access);
                    if (prop != null)
                    {
                        data.Add(access, prop.GetValue(item));
                    }
                    else
                    {
                        data.Add(access, null);
                    }
                }
                result.Add(data);
            }
            // Lọc dữ liệu theo các trường trong listString

            // Nếu không phải danh sách dữ liệu, trả về giá trị mặc định
            return Ok(result);
        }

        [HttpGet]
        [AuthorAPI]
        public async Task<IHttpActionResult> GetById(string name, long id)
        {
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            //var userInfo = _appUserService.GetDtoById(CurrentUserId);
            var convertName = _configRequestService.GetByCode(name);
            var check = await CheckAccessApi((name + "GET"));
            if (check == false)
            {
                return BadRequest("Không có quyền");
            }
            Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
            var assemblyName = assembly.GetReferencedAssemblies().FirstOrDefault(x => x.Name == "Hinet.Model");
            Assembly assembly1 = Assembly.Load(assemblyName);
            Type objectType = assembly1.GetTypes()
                .Where(x => x.Namespace == "Hinet.Model.Entities").Where(x => x.Name == name).FirstOrDefault();

            object obj = Activator.CreateInstance(objectType);
            var service = _componentContext.ResolveNamed<IService>($"{name}Service");

            var getAll = service.GetType().GetMethod("GetById", new Type[] { typeof(long) });
            //Lấy method trong service
            object list = null;
            list = getAll.Invoke(service, new object[] { id });

            return Ok(list);
        }

        [HttpPost]
        [Route("create")]
        [AuthorAPI]
        public async Task<IHttpActionResult> Create(string name, [FromForm] Dictionary<string, string> keyValuePairs)
        {
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            //var userInfo = _appUserService.GetDtoById(CurrentUserId);
            var convertName = _configRequestService.GetByCode(name);
            var check = await CheckAccessApi((name + "-Create"));
            if (check == false)
            {
                return BadRequest("Không có quyền");
            }
            Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
            var assemblyName = assembly.GetReferencedAssemblies().FirstOrDefault(x => x.Name == "Hinet.Model");
            Assembly assembly1 = Assembly.Load(assemblyName);
            Type objectType = assembly1.GetTypes()
                .Where(x => x.Namespace == "Hinet.Model.Entities").Where(x => x.Name == name).FirstOrDefault();

            object obj = Activator.CreateInstance(objectType);
            var service = _componentContext.ResolveNamed<IService>($"{name}Service");

            //Lấy method trong service
            var create = service.GetType().GetMethod("Create");
            //For object Entites
            //var listproAudi = typeof(AuditableEntity<long>).GetProperties().Select(x => x.Name);
            var checkRequestTable = _configRequestService.FindBy(x => x.Code.Equals((name + "-Create"))).FirstOrDefault();
            List<string> listString = new List<string>();
            if (checkRequestTable != null)
            {
                listString = checkRequestTable.AccessInfor.Split(',').ToList();
            }

            objectType.GetProperties().ForEach(x =>
            {
                //keyValuePairs: Lấy dữ liệu gửi lên từ client
                if (keyValuePairs.TryGetValue(x.Name, out var val))
                {
                    try
                    {
                        //Check kiểu dữ liệu rồi convvert lại
                        if (x.PropertyType == typeof(double) || x.PropertyType == typeof(double?))
                        {
                            if (Double.TryParse(val, out double db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else if (x.PropertyType == typeof(decimal) || x.PropertyType == typeof(decimal?))
                        {
                            if (decimal.TryParse(val, out decimal db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else if (x.PropertyType == typeof(int) || x.PropertyType == typeof(int?))
                        {
                            if (int.TryParse(val, out int db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else if (x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?))
                        {
                            if (DateTime.TryParse(val, out DateTime db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else
                        {
                            //Set dữ liệu truyền lên vào entites
                            if (listString.Contains(x.Name))
                            {
                                x.SetValue(obj, Convert.ChangeType(val, x.PropertyType));
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            //Lưu lại
            create.Invoke(service, new[] { obj });
            return Ok(obj);
        }

        [HttpPost]
        [Route("edit")]
        [AuthorAPI]
        public async Task<IHttpActionResult> Update(string name, [FromForm] Dictionary<string, string> keyValuePairs)
        {
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            //var userInfo = _appUserService.GetDtoById(CurrentUserId);
            var convertName = _configRequestService.GetByCode(name);
            var check = await CheckAccessApi((name + "-Update"));
            if (check == false)
            {
                return BadRequest("Không có quyền");
            }
            //Lấy Entitiy
            Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
            var assemblyName = assembly.GetReferencedAssemblies().FirstOrDefault(x => x.Name == "Hinet.Model");
            Assembly assembly1 = Assembly.Load(assemblyName);
            Type objectType = assembly1.GetTypes()
                .Where(x => x.Namespace == "Hinet.Model.Entities").Where(x => x.Name == name).FirstOrDefault();

            object obj = Activator.CreateInstance(objectType);

            var service = _componentContext.ResolveNamed<IService>($"{name}Service");

            //Lấy method trong service
            var create = service.GetType().GetMethod("Update");
            //var listproAudi = typeof(AuditableEntity<long>).GetProperties().Select(x => x.Name);
            //For object Entites
            var checkRequestTable = _configRequestService.FindBy(x => x.Code.Equals((name + "-Update"))).FirstOrDefault();
            List<string> listString = new List<string>();
            if (checkRequestTable != null)
            {
                listString = checkRequestTable.AccessInfor.Split(',').ToList();
            }

            objectType.GetProperties().ForEach(x =>
            {
                //keyValuePairs: Lấy dữ liệu gửi lên từ client
                if (keyValuePairs.TryGetValue(x.Name, out var val))
                {
                    try
                    {
                        //Check kiểu dữ liệu rồi convvert lại
                        if (x.PropertyType == typeof(double) || x.PropertyType == typeof(double?))
                        {
                            if (Double.TryParse(val, out double db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else if (x.PropertyType == typeof(decimal) || x.PropertyType == typeof(decimal?))
                        {
                            if (decimal.TryParse(val, out decimal db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else if (x.PropertyType == typeof(int) || x.PropertyType == typeof(int?))
                        {
                            if (int.TryParse(val, out int db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else if (x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?))
                        {
                            if (DateTime.TryParse(val, out DateTime db))
                            {
                                x.SetValue(obj, db);
                            }
                        }
                        else
                        {
                            //Set dữ liệu truyền lên vào entites
                            if (listString.Contains(x.Name) || x.Name == "Id")
                            {
                                x.SetValue(obj, Convert.ChangeType(val, x.PropertyType));
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            //Lưu lại
            create.Invoke(service, new[] { obj });
            return Ok(obj);
        }

        [HttpPost]
        [Route("delete")]
        [AuthorAPI]
        public async Task<IHttpActionResult> Delete(string name, long id)
        {
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            //var userInfo = _appUserService.GetDtoById(CurrentUserId);
            var convertName = _configRequestService.GetByCode(name);
            var check = await CheckAccessApi((name + "-Delete"));
            if (check == false)
            {
                return BadRequest("Không có quyền");
            }
            //Lấy Entitiy
            Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
            var assemblyName = assembly.GetReferencedAssemblies().FirstOrDefault(x => x.Name == "Hinet.Model");
            Assembly assembly1 = Assembly.Load(assemblyName);
            Type objectType = assembly1.GetTypes()
                .Where(x => x.Namespace == "Hinet.Model.Entities").Where(x => x.Name == name).FirstOrDefault();

            object obj = Activator.CreateInstance(objectType);

            var service = _componentContext.ResolveNamed<IService>($"{name}Service");

            //Lấy method trong service
            var getById = service.GetType().GetMethod("GetById", new Type[] { typeof(long) });
            var delete = service.GetType().GetMethod("Delete");
            var item = getById.Invoke(service, new object[] { id });
            //Lưu lại
            delete.Invoke(service, new[] { item });
            return Ok();
        }

        [HttpGet]
        [Route("get-all-by-role")]
        [AuthorAPI]
        public async Task<IHttpActionResult> GetAllByRole(string token, string name)
        {
            if (name == null)
            {
                return BadRequest("Điền đầy đủ");
            }
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            var userInfo = _appUserService.GetDtoById(CurrentUserId);
            var convertName = _configRequestService.GetByCode(name);
            if (convertName == null || userInfo.ListRoles == null || CurrentUserId == 0)
            {
                return BadRequest();
            }
            var check = await CheckAccessApi((name + "GET"));
            if (check == false)
            {
                return BadRequest("Không có quyền");
            }
            // your code
            //Lấy Entitiy
            Assembly assembly = Assembly.GetExecutingAssembly(); // Get the current assembly
            var assemblyName = assembly.GetReferencedAssemblies().FirstOrDefault(x => x.Name == "Hinet.Model");
            Assembly assembly1 = Assembly.Load(assemblyName);
            Type objectType = assembly1.GetTypes()
                .Where(x => x.Namespace == "Hinet.Model.Entities").Where(x => x.Name == name).FirstOrDefault();

            object obj = Activator.CreateInstance(objectType);
            var service = _componentContext.ResolveNamed<IService>($"{name}Service");

            //Lấy method trong service
            object list = null;
            List<string> listString = new List<string>();
            foreach (var op in _configRequestService.GetAll())
            {
                listString = op.AccessInfor.Split(',').ToList();
                if (op.Code == (name + "-GET"))
                {
                    var getTake = service.GetType().GetMethod("GetByConditionList3");
                    list = getTake.Invoke(service, new[] { listString });
                }
            }
            return Ok(list);
        }
    }
}