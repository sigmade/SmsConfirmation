using System.Threading.Tasks;

namespace WebApi.Providers
{
    public interface ISmsProvier
    {
        Task<bool> SendMessage(string phone, string message);
    }
}
