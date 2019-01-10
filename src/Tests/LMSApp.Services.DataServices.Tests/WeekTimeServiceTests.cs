
using LMSApp.Data;
using LMSApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LMSApp.Services.DataServices.Tests
{
    [Collection("LMSApp_Service_Tests")]
    public class WeekTimeServiceTests
    {
        [Fact]
        public async Task DeleteByIdMarksTheWeekTimeInstanceAsDeleted()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
              .UseInMemoryDatabase(databaseName: "CreateWeekTime_Database1")
              .Options;

            //BaseServiceTests.Initialize();

            int weekTimeId;

            using (var context = new LMSAppContext(options))
            {
                context.WeekTimes.Add(new WeekTime()
                {
                    DayOfWeek = DayOfWeek.Friday,
                    StartHour = "14h"
                }
                );

                context.SaveChanges();

                var weekTime = await context.WeekTimes.FirstAsync();

                weekTimeId = weekTime.Id;
            }

            using (var context = new LMSAppContext(options))
            {
                var repository = new DbRepository<WeekTime>(context);
                var service = new WeekTimeService(repository);

                await service.DeleteById(weekTimeId);

                Assert.True(context.WeekTimes.First().IsDeleted);
            }
        }
    }
}
