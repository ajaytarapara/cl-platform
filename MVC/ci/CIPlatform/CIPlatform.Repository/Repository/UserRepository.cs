using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CIPlatformDbContext _ciPlatformDbContext;
        public UserRepository(CIPlatformDbContext cIPlatformDbContext)
        {
            _ciPlatformDbContext = cIPlatformDbContext;
        }
        public IEnumerable<User> getUsers()
        {
            var Users = _ciPlatformDbContext.Users;
            return Users;
        }


        void IUserRepository.addResetPasswordToken(PasswordReset passwordResetObj)
        {
            bool isAlreadyGenerated = _ciPlatformDbContext.PasswordResets.Any(u => u.Email.Equals(passwordResetObj.Email));
            if (isAlreadyGenerated)
            {
                _ciPlatformDbContext.Update(passwordResetObj);

            }
            else
            {
                _ciPlatformDbContext.Add(passwordResetObj);
            }
            _ciPlatformDbContext.SaveChanges();
        }

        void IUserRepository.addUser(User user)
        {
            _ciPlatformDbContext.Users.Add(user);
            _ciPlatformDbContext.SaveChanges();
        }

        User IUserRepository.findUser(string email)
        {
            User user = _ciPlatformDbContext.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }
        User IUserRepository.findUser(int? id)
        {
            return _ciPlatformDbContext.Users.Where(u => u.UserId == id).First();
        }

        PasswordReset IUserRepository.findUserByToken(string token)
        {
            return _ciPlatformDbContext.PasswordResets.Where(u => u.Token == token).First();
        }



        void IUserRepository.RemoveResetPasswordToken(PasswordReset obj)
        {
            _ciPlatformDbContext.Remove(obj);
            _ciPlatformDbContext.SaveChanges();
        }



        void IUserRepository.updatePassword(User user)
        {
            _ciPlatformDbContext.Update(user);
            _ciPlatformDbContext.SaveChanges();
        }

        bool IUserRepository.validateEmail(string email)
        {
            return _ciPlatformDbContext.Users.Any(u => u.Email == email);
        }

        User IUserRepository.validateUser(string emailId, string password)
        {
            return _ciPlatformDbContext.Users.FirstOrDefault(u => u.Password == password && u.Email == emailId);
        }

        void IUserRepository.removeResetPasswordToken(PasswordReset obj)
        {
            _ciPlatformDbContext.Remove(obj);
            _ciPlatformDbContext.SaveChanges();
        }
        void IUserRepository.edituserprofile(User userObj, List<UserSkill> userSkill)
        {
            _ciPlatformDbContext.Users.Update(userObj);
            _ciPlatformDbContext.SaveChanges();
            foreach (var skill in userSkill)
            {
                _ciPlatformDbContext.UserSkills.Update(skill);
                _ciPlatformDbContext.SaveChanges();

            }
        }

        int IUserRepository.getskillid(string skill)
        {
            int skillid = _ciPlatformDbContext.Skills.Where(u => u.SkillName == skill).Select(u => u.SkillId).FirstOrDefault();
            return skillid;
        }

        IEnumerable<City> IUserRepository.getCities(long countryid)
        {
            if (countryid == 0)
            {
                return _ciPlatformDbContext.Cities;
            }
            else
            {
                return _ciPlatformDbContext.Cities.Where(x => x.CountryId == countryid);
            }
        }
        IEnumerable<City> IUserRepository.getCities()
        {

            return _ciPlatformDbContext.Cities;
        }



        IEnumerable<Country> IUserRepository.getCountries()
        {
            return _ciPlatformDbContext.Countries;

        }
        IEnumerable<Skill> IUserRepository.getSkill()
        {
            return _ciPlatformDbContext.Skills;
        }
        void IUserRepository.editPassword(User userObj)
        {
            _ciPlatformDbContext.Update(userObj);
            _ciPlatformDbContext.SaveChanges();
        }

        IEnumerable<Timesheet> IUserRepository.getTimesheets(long UserId)
        {
            IEnumerable<Timesheet> timesheet = _ciPlatformDbContext.Timesheets.Include(x => x.Mission).Where(u => u.UserId == UserId && u.Status == "approved");
            return timesheet;
        }
        Timesheet IUserRepository.GetTimesheet(long timesheetId)
        {
            return _ciPlatformDbContext.Timesheets.Where(x => x.TimesheetId == timesheetId).FirstOrDefault();
        }
        IEnumerable<Mission> IUserRepository.getmissiontitle(long UserId)
        {
            IEnumerable<Mission> titlesmission = _ciPlatformDbContext.MissionApplications.Where(u => u.UserId == UserId && u.ApprovalStatus == "approved").Select(u => u.Mission);
            return titlesmission;
        }
        void IUserRepository.addtimesheet(Timesheet timesheet)
        {
            _ciPlatformDbContext.Add(timesheet);
            _ciPlatformDbContext.SaveChanges();
        }
        void IUserRepository.deletetimesheet(Timesheet timesheet)
        {
            _ciPlatformDbContext.Remove(timesheet);
            _ciPlatformDbContext.SaveChanges();
        }
        void IUserRepository.edittimesheet(Timesheet timesheet)
        {
            _ciPlatformDbContext.Update(timesheet);
            _ciPlatformDbContext.SaveChanges();
        }

        void IUserRepository.edittimesheetgoal(Timesheet timesheet)
        {
            _ciPlatformDbContext.Update(timesheet);
            _ciPlatformDbContext.SaveChanges();

        }
        List<Banner> IUserRepository.getbanner()
        {
            return _ciPlatformDbContext.Banners.OrderBy(x => x.SortOrder).ToList();
        }
        Mission IUserRepository.GetMission(long MissionId)
        {
            return _ciPlatformDbContext.Missions.Where(x => x.MissionId == MissionId).FirstOrDefault();
        }
        List<UserSkill> IUserRepository.getUserSkill(long userid)
        {
            return _ciPlatformDbContext.UserSkills.Include(x => x.Skill).Where(x => x.UserId == userid).ToList();
        }
        void IUserRepository.RemoveUserSkill(long userid)
        {
            List<UserSkill> userSkills = _ciPlatformDbContext.UserSkills.Where(x => x.UserId == userid).ToList();
            foreach (UserSkill skill in userSkills)
            {
                _ciPlatformDbContext.UserSkills.Remove(skill);
                _ciPlatformDbContext.SaveChanges();
            }
        }

    }
}