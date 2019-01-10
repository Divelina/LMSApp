
using LMSApp.Data;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Services.Models.Assignments;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LMSApp.Services.DataServices.Tests
{
    [Collection("LMSApp_Service_Tests")]
    public class AssignmentServiceTests : IClassFixture<DatabaseAssignmentFixture>
    {
        DatabaseAssignmentFixture dbFixture;

        public AssignmentServiceTests(DatabaseAssignmentFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public async Task CreateAsyncCreatesOneEntity()
        {
            BaseServiceTests.Initialize();

            var repository = new DbRepository<Assignment>(dbFixture.dbContext);
            var service = new AssignmentService(repository);

            var assgnCount = this.dbFixture.dbContext.Assignments.Count();

            var lecturecise = this.dbFixture.dbContext.Lecturecises.First();
            var lectureciseId = lecturecise.Id;

            var educator = this.dbFixture.dbContext.Educator.First();
            var educatorId = educator.Id;

            await service.CreateAsync(new AssignmentCreateBindingModel()
            {
                Name = "Assgn2",
                MaxGrade = 10m,
                EducatorId = educatorId,
                LectureciseId = lectureciseId
            });

            Assert.Equal(assgnCount + 1, dbFixture.dbContext.Assignments.Count());
        }

        [Fact]
        public void GetByCourseIdReturnsAllCoursesWithTheGivenId()
        {
            var courseId = "test";

            BaseServiceTests.Initialize();

            var countWithId = this.dbFixture.dbContext.Assignments
                .Where(a => a.IsDeleted == false && a.Lecturecise.CourseId == courseId).Count();

            var repository = new DbRepository<Assignment>(this.dbFixture.dbContext);
            var service = new AssignmentService(repository);

            Assert.Equal(countWithId, service.GetByCourseId(courseId).Count());
        }

        [Fact]
        public void GetByIdGetsAssignmentWithTheGivenId()
        {
            BaseServiceTests.Initialize();

            var id = this.dbFixture.dbContext.Assignments
                .Where(l => l.IsDeleted == false).Select(l => l.Id).First();

            var repository = new DbRepository<Assignment>(this.dbFixture.dbContext);
            var service = new AssignmentService(repository);

            var result = service.GetById(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void GetByIdOriginalReturnsNullIfAssignmentWithIdNotFound()
        {
            BaseServiceTests.Initialize();

            var id = "NoSuchId";

            var repository = new DbRepository<Assignment>(this.dbFixture.dbContext);
            var service = new AssignmentService(repository);

            var result = service.GetByIdOriginal(id);

            Assert.Null(result);
        }

        [Fact]
        public void GetByIdOriginalGetsAssignmentWithTheGivenId()
        {
            BaseServiceTests.Initialize();

            var id = this.dbFixture.dbContext.Assignments
                .Where(l => l.IsDeleted == false).Select(l => l.Id).First();

            var repository = new DbRepository<Assignment>(this.dbFixture.dbContext);
            var service = new AssignmentService(repository);

            var result = service.GetByIdOriginal(id);

            Assert.Equal(id, result.Id);
        }
    }
}

