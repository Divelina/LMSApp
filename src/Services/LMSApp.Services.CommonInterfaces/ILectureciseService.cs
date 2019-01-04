
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface ILectureciseService
    {
        Task<string> DeleteLectureciseById(string lectureciseId);
    }
}
