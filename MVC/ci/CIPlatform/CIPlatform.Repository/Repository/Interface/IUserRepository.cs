using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Repository.Repository.Interface
{
    public interface IUserRepository
    {
        public IEnumerable<User> getUsers();
        public Boolean validateEmail(string email);
        public User validateUser(string email, string password);
        public User findUser(string email);
        public User findUser(int? id);

        public PasswordReset findUserByToken(string token);
        public void updatePassword(User user);

        public void addUser(User user);
        public void addResetPasswordToken(PasswordReset obj);
        public void RemoveResetPasswordToken(PasswordReset obj);
        void removeResetPasswordToken(PasswordReset obj);
        public void edituserprofile(User userObj,List<UserSkill> userSkill);

        public IEnumerable<City> getCities(long countryid);
        public IEnumerable<City> getCities();
        public IEnumerable<Country> getCountries();

        public IEnumerable<Skill> getSkill();

        public void editPassword(User userObj);
        public int getskillid(string skill);

        public IEnumerable<Timesheet> getTimesheets(long UserId);

        public IEnumerable<Mission>getmissiontitle(long UserId);

        public void addtimesheet(Timesheet timesheet);

        public void deletetimesheet(Timesheet timesheet);

        public void edittimesheet(Timesheet timesheet);

        public void edittimesheetgoal(Timesheet timesheet);

        public List<Banner> getbanner();
        public Mission GetMission(long MissionId);
        public Timesheet GetTimesheet(long timesheetId);
        public List<UserSkill> getUserSkill(long userid);
    }
}