
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
   public interface IEducatorLectureciseService
    {
        Task<string> DeleteById(string lectureciseId);
    }
}
