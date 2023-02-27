using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositry.Repository.Interface
{
    public interface IUserRepository
    {
        public IEnumerable<User> getUsers();
    }
}
