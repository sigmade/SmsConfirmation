using System;
using System.Threading.Tasks;

namespace WebApi.Providers
{
    public class StubProvider : ISmsProvier
    {
        public async Task<bool> SendMessage(string phone, string message)
        {
            Console.WriteLine($"Sended code - {message} to phone number - {phone}");
            return true;
        }
    }
}
