﻿using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        IEnumerable<GridModel>IHomeRepository.Getgridview(string country, string city, string theme, string skill,string searchText,string sorting)
        {
            return _ciPlatformDbContext.GridModel.FromSqlInterpolated($"exec sp_get_gridview_data  @countryNames={country},@cityNames={city},@themeNames={theme},@skillNames={skill},@searchtext={searchText},@sorting={sorting}");


        }

    }
}
