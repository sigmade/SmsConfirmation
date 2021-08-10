using WebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class SmsConfirmationService
    {
        private readonly ISmsProvier _smsProvier;
        private static HashSet<ConfirmModel> _confirmModels = new();

        public SmsConfirmationService(ISmsProvier smsProvier)
        {
            _smsProvier = smsProvier;
        }

        public async Task<bool> SendCode(SendCodeRequest request)
        {
            Random rnd = new();
            int code = rnd.Next(1010, 9090);
                
            var result = await _smsProvier.SendMessage(request.Phone, code.ToString());

            if(result)
            {
                _confirmModels.Add(new() { Phone = request.Phone, Code = code });
            }

            return result;
        }

        public async Task<bool> VerifyCode(VerifyRequest request)
        {
            var confirmRecord = _confirmModels
                .SingleOrDefault(c => c.Phone == request.Phone && 
                                     c.Code == request.Code &&
                                     c.AddedDate > DateTimeOffset.Now.AddMinutes(-1));

            if(confirmRecord is null)
            {
                return false;
            }

            _confirmModels.Remove(confirmRecord);

            return true;
        }
    }

    public class SendCodeRequest
    {
        public string Phone { get; set; }
    }

    public class VerifyRequest
    {
        public int Code { get; set; }
        public string Phone { get; set; }
    }

    public class ConfirmModel
    {
        public string Phone { get; set; }
        public int Code { get; set; }
        public DateTimeOffset AddedDate { get; set; } = DateTimeOffset.Now;
    }
}