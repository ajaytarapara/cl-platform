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
            pagination.pageSize = 3;
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

        Story IStoryRepository.Getdetailstory(Story story,long storyid)
        {
           Story storydetail= _ciPlatformDbContext.Stories.Include(u => u.StoryMedia).Include(u =>u.User).Where(u => u.StoryId == storyid).FirstOrDefault();
            return storydetail;
        }

        List<User> IStoryRepository.Getusersemail()
        {
           List<User> emails=_ciPlatformDbContext.Users.ToList();
            return emails;
        }

        void IStoryRepository.AddInvitedUser(StoryInvite invite)
        {
            _ciPlatformDbContext.Add(invite);
            _ciPlatformDbContext.SaveChanges();
        }
        long IStoryRepository.GetInvitedUserid(string cow_email,long fromuserid)
        {
            User user= _ciPlatformDbContext.Users.Where(x=>x.Email== cow_email).FirstOrDefault();  
            Notification notification= new Notification();
            notification.CreatedAt= DateTime.Now;
            notification.NotificationText = user.FirstName+user.LastName+"\n"+"Recommanded co-worker from story";
            notification.ToUserId= (int?)user.UserId;
            notification.FromId= (int?)fromuserid;
            notification.NotificationType= "Recommanded co-worker from story";
            _ciPlatformDbContext.Add(notification);
            _ciPlatformDbContext.SaveChanges();
            return user.UserId;
        }

        void IStoryRepository.AddStoryViews(Story story)
        {
            _ciPlatformDbContext.Update(story);
            _ciPlatformDbContext.SaveChanges();
        }
        int IStoryRepository.AddStory(Story story)
        {
            _ciPlatformDbContext.Add(story);
            _ciPlatformDbContext.SaveChanges();
            return (int)story.StoryId;
        }
        void IStoryRepository.AddStoryMedia(StoryMedium storyMedium)
        {
            _ciPlatformDbContext.Add(storyMedium);
            _ciPlatformDbContext.SaveChanges();
        }
    }
}
