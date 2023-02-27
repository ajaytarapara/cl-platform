using CI_Platform.Entities.DataModels;
using CI_Platform.Repositry.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositry.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly CiDbContext _userContext;
        public UserRepository(CiDbContext userContext)
        {
            _userContext= userContext;
        }

        IEnumerable<User> IUserRepository.getUsers()
        {
            return _userContext.Users;
        }
    }
}
