
using LMSApp.Data.Common;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.CommonInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using System.Linq;
using System.Collections.Generic;
using LMSApp.Services.Models.Users;
using LMSApp.Services.Mapping;
using LMSApp.Data.Models;
using System;

namespace LMSApp.Services.DataServices
{
    public class WeekTimeService : IWeekTimeService
    {
        private readonly IRepository<WeekTime> weekTimerepository;

        public WeekTimeService(IRepository<WeekTime> weekTimerepository)
        {
            this.weekTimerepository = weekTimerepository;
        }

        public async Task<int> DeleteById(int Id)
        {
            var weekTime = this.weekTimerepository.All()
                .Where(w => w.Id == Id)
                .FirstOrDefault();


            weekTime.IsDeleted = true;
            weekTime.DeletedOn = DateTime.UtcNow;

            this.weekTimerepository.Edit(weekTime);

            await this.weekTimerepository.SaveChangesAsync();

            return Id;
        }


    }
}
