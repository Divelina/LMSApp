
using System;
using System.Threading.Tasks;
using LMSApp.Data.Common;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;

namespace LMSApp.Services.DataServices
{
    public class LectureciseService : ILectureciseService
    {
        private readonly IRepository<Lecturecise> lectureciseRepository;

        public LectureciseService(IRepository<Lecturecise> lectureciseRepository)
        {
            this.lectureciseRepository = lectureciseRepository;
        }

        //Admin only
        public async Task<string> DeleteLectureciseById(string lectureciseId)
        {
            var lecturecise = await this.lectureciseRepository.FindbyId(lectureciseId);
            
            if (lecturecise != null && lecturecise.IsDeleted == false)
            {
                var courseId = lecturecise.CourseId;

                //this.lectureciseRepository.Delete(lecturecise);

                lecturecise.IsDeleted = true;
                lecturecise.DeletedOn = DateTime.UtcNow;

                this.lectureciseRepository.Edit(lecturecise);

                await this.lectureciseRepository.SaveChangesAsync();

                return courseId;
            }

            return null;
        }
    }
}
