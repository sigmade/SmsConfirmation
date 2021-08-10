using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApi.Providers
{
    public class SmsCenter : ISmsProvier
    {
        private readonly SmsCenterHttpClient _httpClient;

        public SmsCenter(SmsCenterHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendMessage(string phone, string code)
        {
            var res = await _httpClient.SendCode(phone, code);

            return true;
        }
    }

    public class SmsCenterHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiPass;
        private readonly string _apiLogin;
        private readonly string _apiUrl;

        public SmsCenterHttpClient(
            HttpClient httpClient,
            IConfiguration config)
        {
            _httpClient = httpClient;

            var smsCenterSettings = config.GetSection("SmsCenterSettings");

            _apiPass = smsCenterSettings["password"];
            _apiLogin = smsCenterSettings["login"];
            _apiUrl = smsCenterSettings["url"];
        }

        public async Task<HttpResponseMessage> SendCode(string phone, string code)
        {
            var response = await _httpClient
                .GetAsync($"{_apiUrl}send.php?login={_apiLogin}&psw={_apiPass}&phones={phone}&mes=CODE:{code}");

            return response;
        }
    }
}
