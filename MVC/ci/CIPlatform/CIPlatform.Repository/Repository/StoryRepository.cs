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

       void IStoryRepository.Savestory(Story storymodel,StoryMedium storymedia)
        {
            Story story = new Story();
            story.Title = storymodel.Title;
            story.Description = storymodel.Description;
            story.PublishedAt = storymodel.PublishedAt;
            story.UserId = storymodel.UserId;
            story.MissionId = storymodel.MissionId;
            StoryMedium storymedium = new StoryMedium();    
            storymedia.Path = storymedia.Path;
            bool hasAlreadystory = _ciPlatformDbContext.Stories.Any(u => u.UserId == storymodel.UserId && u.MissionId == storymodel.MissionId);
            if (hasAlreadystory) 
            {
                Story storyobj = _ciPlatformDbContext.Stories.Where(u => u.UserId == storymodel.UserId && u.MissionId == storymodel.MissionId).FirstOrDefault();
                StoryMedium storymedium1 = new StoryMedium();
                storymedium1.Path = storymedia.Path;
                storyobj.UpdatedAt = DateTime.Now;
                storyobj.Description = storymodel.Description;
                storyobj.Title = storymodel.Title;
                _ciPlatformDbContext.Stories.Update(storyobj);
                _ciPlatformDbContext.SaveChanges();
                storymedium1.StoryId = storyobj.StoryId;

                StoryMedium storymidea=_ciPlatformDbContext.StoryMedia.Where(u => u.StoryId == storymedium1.StoryId).FirstOrDefault();
                if (storymidea != null) 
                {
                storymidea.Path = storymedium1.Path;
                storymidea.Type = ".png";
                storymidea.StoryId = storymedium1.StoryId;
                _ciPlatformDbContext.StoryMedia.Update(storymidea);
                _ciPlatformDbContext.SaveChanges();
                }
                else
                {
                    StoryMedium media = new StoryMedium();
                    media.Path = storymedium1.Path;
                    media.Type = ".png";
                    media.StoryId = storymedium1.StoryId;
                    _ciPlatformDbContext.StoryMedia.Add(media);
                    _ciPlatformDbContext.SaveChanges();

                }
            }
            else
            {
                Story story1 = new Story();
                StoryMedium storymedium1 = new StoryMedium();
                storymedium1.Path = storymedia.Path;
                story1.Title = storymodel.Title;
                story1.Description = storymodel.Description;
                story1.PublishedAt = storymodel.PublishedAt;
                story1.UserId = storymodel.UserId;
                story1.MissionId = storymodel.MissionId;
                _ciPlatformDbContext.Stories.Add(story1);
                _ciPlatformDbContext.SaveChanges();
                storymedium1.StoryId = story1.StoryId;
                StoryMedium storymidea = new StoryMedium();
                if (storymidea != null)
                {
                    storymidea.Path = storymedium1.Path;
                    storymidea.Type = ".png";
                    storymidea.StoryId = storymedium1.StoryId;
                    _ciPlatformDbContext.StoryMedia.Update(storymidea); 
                    _ciPlatformDbContext.SaveChanges();
                }
                else
                {
                    StoryMedium storymedium2 = new StoryMedium();
                    storymedium2.Path = storymedium1.Path;
                    storymedium2.Type = ".png";
                    storymedium2.StoryId = storymedium1.StoryId;
                    _ciPlatformDbContext.StoryMedia.Add(storymedium2);
                    _ciPlatformDbContext.SaveChanges();

                }
            }
        }

        Story IStoryRepository.Getdetailstory(Story story,long storyid)
        {
           Story storydetail= _ciPlatformDbContext.Stories.Include(u => u.StoryMedia).Include(u =>u.User).Where(u => u.StoryId == storyid).FirstOrDefault();
            return storydetail;
        }

    }
}
