using AutoMapper;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.HUYENService;
using Hinet.Service.QLDonViCungCapXangDauService;
using Hinet.Service.XAService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hinet.API2.Models.QLDonViCungCapXangDau;
using Hinet.Model.Entities;
using Hinet.Service.tempTest;
using System.IdentityModel.Tokens.Jwt;
using Hinet.API2.Common;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Hinet.Service.AppUserService;
using Hinet.Service.RoleService;
using Hinet.Service.Constant;

namespace Hinet.API2.Controllers
{
    [RoutePrefix("api/QLDonViCungCapXangDau")]

    public class LyLich2CController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IAppUserService _appUserService;
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        private readonly IQLDonViCungCapXangDauService _QLDonViCungCapXangDauService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IHUYENService _HUYENService;
        private readonly IXAService _XAService;
        private readonly IRoleService _roleService;
        public LyLich2CController(
            IAppUserService appUserService,
            IRoleService roleService,
                IQLDonViCungCapXangDauService QLDonViCungCapXangDauService,
                ILog Ilog,
                IHUYENService HUYENService,
                IXAService XAService, IDM_DulieuDanhmucService dM_DulieuDanhmucService,
                IMapper mapper)
        {
            _roleService = roleService;
            _appUserService = appUserService;
            _QLDonViCungCapXangDauService = QLDonViCungCapXangDauService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _HUYENService = HUYENService;
            _XAService = XAService;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [Route("get-all")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {

            var result = _QLDonViCungCapXangDauService.GetAll();
            if (result == null) return BadRequest("Không tồn tại phần nào tử trong bảng");
            return Ok(result);
        }
        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] QLDonViCungCapXangDauAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var EntityModel = _mapper.Map<QLDonViCungCapXangDau>(request);
                    _QLDonViCungCapXangDauService.Create(EntityModel);
                    return Ok(EntityModel);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }
        [Route("edit")]
        [HttpPost]
        public IHttpActionResult Edit([FromBody] QLDonViCungCapXangDauEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var obj = _QLDonViCungCapXangDauService.GetById(request.Id);
                    obj = _mapper.Map(request, obj);
                    _QLDonViCungCapXangDauService.Update(obj);
                    return Ok(obj);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }
        [Route("get-by-id")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Tham số chuyền vào null");
            }
            else
            {
                try
                {
                    var obj = _QLDonViCungCapXangDauService.GetById(id);
                    return Ok(obj);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }
        [Route("delete")]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Tham số chuyền vào null");
            }
            else
            {
                try
                {
                    var obj = _QLDonViCungCapXangDauService.GetById(id);
                    if (obj == null) return BadRequest("Không tồn tại đối tượng");
                    _QLDonViCungCapXangDauService.Delete(obj);
                    return Ok("Xóa thành công");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [Route("get-by-token")]
        [HttpGet]
        public IHttpActionResult GetByToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Tham số chuyền vào null");
            }
            else
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    var nameid = (long)Convert.ToInt64(jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value);
                    /*var unique_name = jwtSecurityToken.Claims.First(claim => claim.Type == "unique_name").Value;
                    var exp = jwtSecurityToken.Claims.First(claim => claim.Type == "exp").Value;*/
                    var getrolls = _roleService.GetAll();
                    var userDto = _appUserService.GetDtoById(nameid);
                    var consant = UserRoleConstant.QUANTRI;
                    foreach (var item in userDto.ListRoles.OrderByDescending(x=>x.Code))
                    {
                        if (item.Code== UserRoleConstant.QUANTRI)
                        {
                            break;
                        }
                        if(item.Code== UserRoleConstant.CHUYENVIEN)
                        {
                            break;
                        }
                        if (item.Code == UserRoleConstant.KIEMDUYET)
                        {
                            break;
                        }
                        if (item.Code == UserRoleConstant.NVXangDau)
                        {
                            break;
                        }
                    }
                    return Ok(new { userDto.ListRoles });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
