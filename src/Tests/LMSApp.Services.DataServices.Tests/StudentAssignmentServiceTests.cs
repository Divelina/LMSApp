
using LMSApp.Data;
using LMSApp.Data.Models.AssignmentRelated;
using System.Linq;
using Xunit;

namespace LMSApp.Services.DataServices.Tests
{
    [Collection("LMSApp_Service_Tests")]
    public class StudentAssignmentServiceTests : IClassFixture<DatabaseStudentAssignmentFixture>
    {
        DatabaseStudentAssignmentFixture dbFixture;

        public StudentAssignmentServiceTests(DatabaseStudentAssignmentFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public void GetStudentAssignmentsByIdReturnsAllStudentAssignmentsWithTheGivenAssignmentId()
        {
            BaseServiceTests.Initialize();

            var assgnId = "Assgn1";

            var countAssgnWithId = this.dbFixture.dbContext.StudentAssignments
                .Where(sa => sa.AssignmentId == assgnId)
                .Count();

            var repository = new DbRepository<StudentAssignment>(this.dbFixture.dbContext);
            var service = new StudentAssignmentService(repository);

            Assert.Equal(countAssgnWithId, service.GetStudentAssignmentsById(assgnId).Count());
        }

        [Fact]
        public void GetStudentAssignmentsByStudentIdReturnsAllStudentAssignmentsWithTheGivenStudentId()
        {
            BaseServiceTests.Initialize();

            var studentId = "student2";

            var countAssgnWithId = this.dbFixture.dbContext.StudentAssignments
                .Where(sa => sa.StudentId == studentId)
                .Count();

            var repository = new DbRepository<StudentAssignment>(this.dbFixture.dbContext);
            var service = new StudentAssignmentService(repository);

            Assert.Equal(countAssgnWithId, service.GetStudentAssignmentsByStudentId(studentId).Count());
        }

        [Fact]
        public void GetOriginalReturnsEmptyStudentAssignmentWhenNoEntityExistsWithTheGivenAssignmenAndStudentId()
        {
            var assgnId = "NoSuchAssgn";
            var studentId = "NoSuchStudent";

            var repository = new DbRepository<StudentAssignment>(this.dbFixture.dbContext);
            var service = new StudentAssignmentService(repository);

            var assignment = service.GetOriginal(assgnId, studentId);

            Assert.Null(assignment);
        }

        [Fact]
        public void GetOriginalReturnsStudentAssignmentWithTheGivenAssignmenAndStudentId()
        {
            var assgnId = "Assgn1";
            var studentId = "student2";

            var repository = new DbRepository<StudentAssignment>(this.dbFixture.dbContext);
            var service = new StudentAssignmentService(repository);

            var assignment = service.GetOriginal(assgnId, studentId);

            Assert.Equal(assgnId, assignment.AssignmentId);
            Assert.Equal(studentId, assignment.StudentId);
        }
    }
}
