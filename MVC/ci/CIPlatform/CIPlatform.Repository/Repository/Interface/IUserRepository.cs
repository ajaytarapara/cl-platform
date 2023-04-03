using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Repository.Repository.Interface
{
    public interface IUserRepository
    {
        public IEnumerable<User> getUsers();
        public Boolean validateEmail(string email);
        public Boolean validateUser(string email, string password);
        public User findUser(string email);
        public User findUser(int? id);

        public PasswordReset findUserByToken(string token);
        public void updatePassword(User user);

        public void addUser(User user);
        public void addResetPasswordToken(PasswordReset obj);
        public void RemoveResetPasswordToken(PasswordReset obj);
        void removeResetPasswordToken(PasswordReset obj);
        public void edituserprofile(User userObj);
    }
}