
using LMSApp.Data;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Models.Users;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LMSApp.Services.DataServices.Tests
{
    [Collection("LMSApp_Service_Tests")]
    public class EducatorServiceTests :IClassFixture<DatabaseEducatorFixture>
    {
        DatabaseEducatorFixture dbFixture;

        public EducatorServiceTests(DatabaseEducatorFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public void GetByIdGetsEducatorWithTheGivenUserId()
        {
            BaseServiceTests.Initialize();

            var userId = this.dbFixture.dbContext.Educator
                .Where(l => l.IsDeleted == false).Select(l => l.UserId).First();

            var repository = new DbRepository<Educator>(this.dbFixture.dbContext);
            var service = new EducatorService(repository, null);

            var result = service.GetByUserId(userId);

            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task CreateAsyncCreatesOneEducator()
        {
            BaseServiceTests.Initialize();

            var repository = new DbRepository<Educator>(dbFixture.dbContext);
            var service = new EducatorService(repository, null);

            var educatorCount = this.dbFixture.dbContext.Educator.Count();

            var userId = this.dbFixture.dbContext.Users
                .Where(u => u.FirstName == "V").First().Id;

            await service.CreateAsync(new EducatorBindingModel()
            {
               UserId = userId,
               FacultyName = FacultyOf.Physics
            });

            Assert.Equal(educatorCount + 1, dbFixture.dbContext.Educator.Count());
        }

        [Fact]
        public async Task DeleteEducatorByUserIdFlagsEducatorWithTheGivenUserIdAsDeleted()
        {
            var repository = new DbRepository<Educator>(dbFixture.dbContext);
            var service = new EducatorService(repository, null);

            var userId = this.dbFixture.dbContext.Users
               .Where(u => u.FirstName == "U").First().Id;

            await service.DeleteEducatorByUserId(userId);

            Assert.True(this.dbFixture.dbContext.Educator.Where(e => e.UserId == userId)
                .First().IsDeleted);
        }

        [Fact]
        public async Task UnDeleteEducatorByUserIdFlagsEducatorWithTheGivenUserIdAsNotDeleted()
        {
            var repository = new DbRepository<Educator>(dbFixture.dbContext);
            var service = new EducatorService(repository, null);

            var userId = this.dbFixture.dbContext.Users
               .Where(u => u.FirstName == "S").First().Id;

            await service.UnDeleteEducatorByUserId(userId);

            Assert.False(this.dbFixture.dbContext.Educator.Where(e => e.UserId == userId)
                .First().IsDeleted);
        }

        [Fact]
        public void GetAllGetsAllEducators()
        {

            BaseServiceTests.Initialize();

            var count = this.dbFixture.dbContext.Educator.Count();

            var repository = new DbRepository<Educator>(this.dbFixture.dbContext);
            var service = new EducatorService(repository, null);

            Assert.Equal(count, service.GetAll().Count());
        }
    }
}
