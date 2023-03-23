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
         Story Storydata()
        {
           List <Story> storydata= _ciPlatformDbContext.Stories.FromSqlInterpolated($"exec sp_get_gridview_data").ToList();
            return (Storydata);
        }
    }
}