using System.Threading.Tasks;

namespace Streamline.Application.Repositories
{
    public interface ILogRepository
    {
        Task Low(string message);
        Task Medium(string message);
        Task High(string message);
        Task Critical(string message);
    }
}
