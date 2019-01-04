
using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LMSApp.Services.Models.Courses
{
    public class LectureciseCreateBindingModel : IMapTo<Lecturecise>
    {
        [Required]
        public LectureciseType Type { get; set; }

        [Required]
        public string CourseId { get; set; }

        public IEnumerable<string> EducatorIds { get; set; }

        public DayOfWeek? Day1 { get; set; }

        public DateTime? StartTime1 { get; set; }

        public DateTime? EndTime1 { get; set; }

        public DayOfWeek? Day2 { get; set; }

        public DateTime? StartTime2 { get; set; }

        public DateTime? EndTime2 { get; set; }

        //public IEnumerable<string> TheWeekTimes =>

        //    this.WeekTimes.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(wk => wk.Trim());


        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    configuration.CreateMap<LectureciseCreateBindingModel, Lecturecise>()
        //   .ForMember(x => x.WeekTimes,
        //               m => m.MapFrom(src => src.TheWeekTimes
        //               .Select(wk => wk.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //               .Select(wk => new WeekTime()
        //               {
        //                   DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), wk[0]),
        //                   StartHour = wk[1],
        //                   EndHour = wk.Count() > 2 ? wk[2] : null
        //               }).ToList()));
        //}
    }
}
