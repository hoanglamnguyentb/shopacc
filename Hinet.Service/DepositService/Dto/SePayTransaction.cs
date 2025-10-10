using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DepositService.Dto
{
    public class SePayTransaction
    {
        public int Id { get; set; }                          // ID giao dịch trên SePay
        public string Gateway { get; set; }                  // Brand name của ngân hàng
        public DateTime TransactionDate { get; set; }        // Thời gian xảy ra giao dịch phía ngân hàng
        public string AccountNumber { get; set; }            // Số tài khoản ngân hàng
        public string Code { get; set; }                     // Mã code thanh toán
        public string Content { get; set; }                  // Nội dung chuyển khoản
        public string TransferType { get; set; }             // Loại giao dịch (in / out)
        public decimal TransferAmount { get; set; }          // Số tiền giao dịch
        public decimal Accumulated { get; set; }             // Số dư tài khoản (lũy kế)
        public string SubAccount { get; set; }               // Tài khoản ngân hàng phụ (nếu có)
        public string ReferenceCode { get; set; }            // Mã tham chiếu của tin nhắn SMS
        public string Description { get; set; }              // Toàn bộ nội dung tin nhắn SMS
    }
}