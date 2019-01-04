
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.Models.Courses;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface ILectureciseService
    {
        bool Any(string id);

        Task SaveLecturecises();

        Task<LectureciseDetailsViewModel> GetById(string lectureciseId);

        Lecturecise GetByOriginal(string lectureciseId);

        Task EditLecturecise(LectureciseDetailsViewModel lectureciseModel);

        Task<string> CreateAsync(LectureciseCreateBindingModel lecturecise);

        Task<string> DeleteLectureciseById(string lectureciseId);
    }
}
