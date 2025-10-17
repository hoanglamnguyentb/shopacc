using Hinet.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hinet.Service.TelegramService
{
    public interface ITelegramService
    {
        Task SendTelegramMessage(GiaoDich giaoDich);
    }
}