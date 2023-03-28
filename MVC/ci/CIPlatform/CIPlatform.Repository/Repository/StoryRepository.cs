using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly CIPlatformDbContext _ciPlatformDbContext;

        public StoryRepository(CIPlatformDbContext cIPlatformDbContext)
        {
            _ciPlatformDbContext = cIPlatformDbContext;
        }

        IEnumerable<City> IStoryRepository.getCities()
        {
            return _ciPlatformDbContext.Cities;
        }

       IEnumerable<Country> IStoryRepository.getCountries()
        {
            return _ciPlatformDbContext.Countries;

        }

        IEnumerable<Skill> IStoryRepository.getSkills()
        {
            return _ciPlatformDbContext.Skills;
       }

        IEnumerable<MissionTheme> IStoryRepository.getThemes()
        {
            return _ciPlatformDbContext.MissionThemes;
        }

        public PaginationMission Storydata(int pageNumber)
        {

            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output1 = new SqlParameter("@missionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
             PaginationMission pagination = new PaginationMission();
            List<StoryModel> storydatalist = _ciPlatformDbContext.Storylist.FromSqlInterpolated($"exec sp_get_story_data @pageNumber = {pageNumber},@TotalCount = {output} out,@missionCount={output1} out").ToList();
            pagination.Stories = storydatalist;
            pagination.pageSize = 2;
            pagination.pageCount = long.Parse(output.Value.ToString());
            pagination.missionCount = long.Parse(output1.Value.ToString());
            pagination.activePage = pageNumber;
            return pagination;
        }
        List<MissionApplication> IStoryRepository.Getstorymission(long UserId)
        {
            List<MissionApplication> storymission= _ciPlatformDbContext.MissionApplications.Include(u => u.Mission).Where(u => u.UserId == UserId && u.ApprovalStatus == "approved").ToList();
            return storymission;
        }

       void IStoryRepository.Savestory(Story story)
        {
            //Story story = new Story();
            //story.Title = storymodel.Title;
            //story.Description = storymodel.Description;
            //story.PublishedAt = storymodel.PublishedAt;
            //story.UserId = storymodel.userid;
            //story.MissionId = (long)storymodel.MissionId;
            _ciPlatformDbContext.Add(story);
            _ciPlatformDbContext.SaveChanges();
        }

    }
}