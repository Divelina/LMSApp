
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.Models.Courses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface ILectureciseService
    {
        bool Any(string id);

        IEnumerable<LectureciseShortViewModel> GetAll();

        Task SaveLecturecises();

        Task<LectureciseDetailsViewModel> GetById(string lectureciseId);

        Lecturecise GetByOriginal(string lectureciseId);

        IEnumerable<LectureciseShortViewModel> GetByCourseId(string courseId);

        IEnumerable<LectureciseShortViewModel> GetAllByEducator(string educatorId);

        Task EditLecturecise(LectureciseDetailsViewModel lectureciseModel);

        Task<string> CreateAsync(LectureciseCreateBindingModel lecturecise);

        Task<string> DeleteLectureciseById(string lectureciseId);
    }
}
