
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
    public class GroupServiceTests : IClassFixture<DatabaseGroupFixture>
    {
        DatabaseGroupFixture dbFixture;

        public GroupServiceTests(DatabaseGroupFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact] void AnyGroupReturnsTrueIfGroupWithSuchMajorAndNumberExists()
        {
            var repository = new DbRepository<Group>(dbFixture.dbContext);
            var service = new GroupService(repository);

            Assert.True(service.AnyGroup(1, Major.Mixed));
        }

        [Fact]
        void AnyGroupReturnsFalseIfGroupWithSuchMajorAndNumberDoesNotExists()
        {
            var repository = new DbRepository<Group>(dbFixture.dbContext);
            var service = new GroupService(repository);

            Assert.False(service.AnyGroup(2, Major.Mixed));
        }

        [Fact]
        public async Task CreateAsyncCreatesOneGroup()
        {
            BaseServiceTests.Initialize();

            var repository = new DbRepository<Group>(dbFixture.dbContext);
            var service = new GroupService(repository);

            var groupCount = this.dbFixture.dbContext.Groups.Count();

            await service.CreateAsync(new GroupViewModel());

            Assert.Equal(groupCount + 1, dbFixture.dbContext.Groups.Count());
        }

        [Fact]
        public void GetAllGetsAllGroups()
        {

            BaseServiceTests.Initialize();

            var count = this.dbFixture.dbContext.Groups.Count();

            var repository = new DbRepository<Group>(this.dbFixture.dbContext);
            var service = new GroupService(repository);

            Assert.Equal(count, service.GetAll().Count());
        }
    }
}
