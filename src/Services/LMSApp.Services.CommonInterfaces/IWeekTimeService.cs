using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface IWeekTimeService
    {
        Task<int> DeleteById(int Id);
    }
}