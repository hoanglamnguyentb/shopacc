using System;
using System.Web.Http;

namespace Hinet.API2.Controllers
{
    public class RechargeController : ApiController
    {
        [HttpPost]
        [Route("api/recharge/generate-code")]
        public IHttpActionResult GenerateCode(RechargeRequest request)
        {
            try
            {
                // Validate amount
                if (request.Amount < 10000)
                {
                    return BadRequest("Số tiền tối thiểu là 10,000 VND");
                }

                // Generate unique transaction code
                // You can customize this logic based on your requirements
                string code = GenerateTransactionCode(request.Amount);

                var response = new
                {
                    success = true,
                    code = code,
                    amount = request.Amount,
                    message = "Tạo mã giao dịch thành công"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Lỗi tạo mã giao dịch: " + ex.Message));
            }
        }

        private string GenerateTransactionCode(decimal amount)
        {
            // Generate a unique transaction code
            // Format: NAP + random number + timestamp
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);

            // You can customize this format according to your business logic
            return $"NAP{random}{timestamp.Substring(timestamp.Length - 6)}";
        }
    }

    public class RechargeRequest
    {
        public decimal Amount { get; set; }
    }
}