
using LMSApp.Data;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LMSApp.Services.DataServices.Tests
{
    [Collection("LMSApp_Service_Tests")]
    public class EducatorLectureciseServiceTests : IClassFixture<DatabaseEducatorLectureciseFixture>
    {
        DatabaseEducatorLectureciseFixture dbFixture;

        public EducatorLectureciseServiceTests(DatabaseEducatorLectureciseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public async Task DeleteByIdDeletesAllEntitiesWithLectureciseId()
        {
            var lectureciseId = "lecturecise1";

            var countWithId = this.dbFixture.dbContext.LectureciseEducators
                .Where(le => le.LectureciseId == lectureciseId).Count();

            var repository = new DbRepository<EducatorLecturecise>(dbFixture.dbContext);
            var service = new EducatorLectureciseService(repository);

            await service.DeleteById(lectureciseId);

            Assert.Equal(countWithId - 1, this.dbFixture.dbContext.LectureciseEducators
                .Where(le => le.LectureciseId == lectureciseId).Count());
        }

    }
}
