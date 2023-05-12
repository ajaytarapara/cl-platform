﻿using CIPlatform.Entities.DataModels;
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
    public class HomeRepository : IHomeRepository
    {
        private readonly CIPlatformDbContext _ciPlatformDbContext;

        public HomeRepository(CIPlatformDbContext cIPlatformDbContext)
        {
            _ciPlatformDbContext = cIPlatformDbContext;
        }

        IEnumerable<City> IHomeRepository.getCities()
        {
            return _ciPlatformDbContext.Cities;
        }

        IEnumerable<Country> IHomeRepository.getCountries()
        {
            return _ciPlatformDbContext.Countries;

        }

        IEnumerable<Skill> IHomeRepository.getSkills()
        {
            return _ciPlatformDbContext.Skills;
        }

        IEnumerable<MissionTheme> IHomeRepository.getThemes()
        {
            return _ciPlatformDbContext.MissionThemes;
        }

        IEnumerable<Mission> IHomeRepository.GetMissions()
        {

            return _ciPlatformDbContext.Missions.Include(mission => mission.City).Include(city => city.Country).Include(theme=>theme.Theme).Include(media=>media.MissionMedia);

        }
        IEnumerable<string> IHomeRepository.Getimgmissionurl()
        {
            return _ciPlatformDbContext.MissionDocuments.Select(x => x.DocumentPath);
        }
        IEnumerable<string> IHomeRepository.GetMissioncity()
        {
            return (IEnumerable<string>)_ciPlatformDbContext.Missions.Select(x => x.City);
        }


        IEnumerable<string> IHomeRepository.GetMissionThemestitle()
        {
            return _ciPlatformDbContext.MissionThemes.Select(x =>x.Title).ToList();
        }

        IEnumerable<string> IHomeRepository.GetMissionDiscription()
        {
            return _ciPlatformDbContext.Missions.Select(x =>x.ShortDescription).ToList();
        }

        IEnumerable<string> IHomeRepository.GetMissionstartdate()
        {
            return (IEnumerable<string>)_ciPlatformDbContext.Missions.Select(x =>x.StartDate).ToList();
        }

        IEnumerable<string>IHomeRepository.GetMissionenddate()
        {
            return (IEnumerable<string>)_ciPlatformDbContext.Missions.Select(x =>x.EndDate).ToList();
        }
        public PaginationMission gridSP(string country, string city, string theme, string skill, string searchText, string sorting, int pageNumber,int uid,string explore)
        {
            // make explicit SQL Parameter
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output1 = new SqlParameter("@missionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            PaginationMission pagination = new PaginationMission();
            List<GridModel> test = _ciPlatformDbContext.GridModel.FromSqlInterpolated($"exec sp_get_gridview_data  @countryNames={country},@cityNames={city},@themeNames={theme},@skillNames={skill},@searchtext={searchText},@sorting={sorting}, @pageNumber = {pageNumber}, @TotalCount = {output} out,@missionCount={output1} out,@UserId = {uid},@Exploreby={explore}").ToList();
            pagination.missions = test;
            pagination.pageSize = 6;
            pagination.pageCount = long.Parse(output.Value.ToString());
            pagination.missionCount= long.Parse(output1.Value.ToString());
            pagination.activePage = pageNumber;
            return pagination;
        }
        public User getuser(string email)
        {
            var user = _ciPlatformDbContext.Users.Where(x => x.Email == email).FirstOrDefault();
            return user;
        }
        public void addToFavourites(long missionid, long userid, int fav)
        {
            if (fav == 0)
            {
                FavouriteMission favouriteMission = new FavouriteMission();
                favouriteMission.MissionId = missionid;
                favouriteMission.UserId = userid;
                favouriteMission.CreatedAt = DateTime.Now;
                _ciPlatformDbContext.FavouriteMissions.Add(favouriteMission);
                _ciPlatformDbContext.SaveChanges();

            }
            else
            {
                FavouriteMission favouriteMission = _ciPlatformDbContext.FavouriteMissions.FirstOrDefault(x => x.MissionId == missionid && x.UserId == userid);
                _ciPlatformDbContext.FavouriteMissions.Remove(favouriteMission);
                _ciPlatformDbContext.SaveChanges();
            }
        }
        List<City> IHomeRepository.GetCityFromCountry(string countryname)
        {
            List<City> citylist = _ciPlatformDbContext.Cities.Include(x => x.Country).Where(x => x.Country.Name == countryname).ToList();
            return citylist;
        }
        List<City> IHomeRepository.GetCityFromCountry()
        {
            List<City> citylist = _ciPlatformDbContext.Cities.Include(x => x.Country).ToList();
            return citylist;
        }

        //===============================================================================================================
        //Notification 
        //================================================================================================================

        List<Notification> IHomeRepository.GetNotificationforUser(long userid, string[] nSetting)
        {
            return _ciPlatformDbContext.Notifications.Where(noti=>noti.ToUserId == userid  && nSetting.Contains(noti.NotificationType)).ToList();
        }
        void IHomeRepository.ClearNotification(long userid)
        {
            List<Notification> usernotificationlist=_ciPlatformDbContext.Notifications.Where(x=>x.ToUserId==userid).ToList();
            foreach (var notification in usernotificationlist)
            {
                _ciPlatformDbContext.Remove(notification);
                _ciPlatformDbContext.SaveChanges();
            }
        }
        void IHomeRepository.UpdateNotificationStatus(long notificationid)
        {
            Notification notification=_ciPlatformDbContext.Notifications.Where(x => x.NotificationId == notificationid).FirstOrDefault();
            notification.Status = "seen";
            _ciPlatformDbContext.Update(notification);
            _ciPlatformDbContext.SaveChanges();
        }


        //==============================================================================
        //notification setting 
        //==============================================================================
        void IHomeRepository.AddNotificationSetting(long userid, NotificationSetting notificationSetting)
        {
            if(notificationSetting == null)
            {
            notificationSetting.UserId = (int)userid;
            notificationSetting.ApplicationApproval = true;
            notificationSetting.StoryApproval = true;
            notificationSetting.RecommandedFromStory= true;
            notificationSetting.RecommandedFromMission= true;
            notificationSetting.NewMissionAdded= true;
            notificationSetting.Receiveemailnotification= true;
            _ciPlatformDbContext.Add(notificationSetting);
            _ciPlatformDbContext.SaveChanges();
            }
            else
            {
                _ciPlatformDbContext.Update(notificationSetting);
                _ciPlatformDbContext.SaveChanges();
            }
        }
        NotificationSetting IHomeRepository.GetNotificationSetting(long userid)
        {

            return _ciPlatformDbContext.NotificationSettings.Where(x=>x.UserId == userid).FirstOrDefault();
        }
    }
}
