using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository.Interface
{
    public interface IStoryRepository
    {
        public IEnumerable<Country> getCountries();
        public IEnumerable<City> getCities();
        public IEnumerable<MissionTheme> getThemes();
        public IEnumerable<Skill> getSkills();
        public PaginationMission Storydata(int pageNumber);

        public List<MissionApplication> Getstorymission(long UserId);
        public int Savestory(Story story,StoryMedium storymedia);

        public Story  Getdetailstory(Story story, long storyid);

        public List<User> Getusersemail();

        public void AddInvitedUser(StoryInvite invite);

        public long GetInvitedUserid(string cow_email);

        public void AddStoryViews(Story story);

    }
}
