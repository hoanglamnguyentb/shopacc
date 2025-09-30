using Autofac;
using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.ConfigRequestService;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Dapper;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using Hinet.API2.Core;
using System.Collections.Generic;

namespace Hinet.API2.Controllers
{
    [System.Web.Http.RoutePrefix("api/DuLieu")]
    public class DuLieuController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IComponentContext _componentContext;
        private IConfigRequestService _configRequestService;
        private IAppUserService _appUserService;
        private const string RoleActive = "API";

        public DuLieuController(IMapper mapper,
            IComponentContext componentContext,
            IConfigRequestService configRequestService,
            IAppUserService appUserService)
        {
            _mapper = mapper;
            this._componentContext = componentContext;
            _configRequestService = configRequestService;
            _appUserService = appUserService;
        }


        private bool IsValidTableName(string tableName)
        {
            return Regex.IsMatch(tableName, @"^[a-zA-Z0-9_]+$"); // Chỉ cho phép ký tự alphanumeric và dấu gạch dưới
        }

        //[System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [AuthorAPI(RoleActive)]
        [System.Web.Http.Route("GetDataTable")]
        public async Task<IHttpActionResult> GetTableData(string tableName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hinetcontext"].ConnectionString;

            if (string.IsNullOrWhiteSpace(tableName) || !IsValidTableName(tableName))
            {
                return BadRequest("Tên bảng không hợp lệ!");
            }

            string query = $"SELECT * FROM [{tableName}]";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    await connection.OpenAsync();

                    // Thực thi truy vấn SQL và ánh xạ kết quả vào đối tượng
                    var result = await connection.QueryAsync(query);

                    if (result == null || !result.Any())
                    {
                        return Json("Không tìm thấy dữ liệu");
                    }

                    return Json(result);

                    // Trả về kết quả
                }
            }
            catch (SqlException ex)
            {
                return InternalServerError(new Exception("Đã xảy ra lỗi khi truy vấn cơ sở dữ liệu", ex));
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Lỗi không xác định", ex));
            }
        }

        [System.Web.Http.HttpGet]
        [AuthorAPI(RoleActive)]
        [System.Web.Http.Route("GetDataTableByPage")]
        public async Task<IHttpActionResult> GetDataTableByPage(string tableName, int pageNumber = 1, int pageSize = 20)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hinetcontext"].ConnectionString;

            if (string.IsNullOrWhiteSpace(tableName) || !IsValidTableName(tableName))
            {
                return BadRequest("Tên bảng không hợp lệ!");
            }

            // Bảo đảm các tham số phân trang hợp lệ
            pageNumber = Math.Max(1, pageNumber);

            // Truy vấn dữ liệu và tổng số bản ghi
            string queryTotalCount = $"SELECT COUNT(*) FROM [{tableName}]";
            string queryData;

            if (pageSize == -1) // Lấy toàn bộ dữ liệu
            {
                queryData = $"SELECT * FROM [{tableName}]";
            }
            else
            {
                pageSize = Math.Max(1, pageSize); // Đảm bảo pageSize > 0
                queryData = $@"
            SELECT * 
            FROM [{tableName}] 
            ORDER BY (SELECT NULL) 
            OFFSET {(pageNumber - 1) * pageSize} ROWS 
            FETCH NEXT {pageSize} ROWS ONLY";
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Lấy tổng số bản ghi
                    var totalCount = await connection.ExecuteScalarAsync<int>(queryTotalCount);

                    // Lấy dữ liệu
                    var data = await connection.QueryAsync(queryData);

                    if (data == null || !data.Any())
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Không tìm thấy dữ liệu",
                            Data = new List<object>(),
                            TotalCount = 0,
                            TotalPages = 0,
                            PageNumber = pageNumber,
                            PageSize = pageSize
                        });
                    }

                    // Tính tổng số trang
                    int totalPages = (pageSize == -1) ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize);

                    // Trả về dữ liệu
                    return Json(new
                    {
                        Success = true,
                        Data = data,
                        TotalCount = totalCount,
                        TotalPages = totalPages,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });
                }
            }
            catch (SqlException ex)
            {
                return InternalServerError(new Exception("Đã xảy ra lỗi khi truy vấn cơ sở dữ liệu", ex));
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Lỗi không xác định", ex));
            }
        }


    }
}