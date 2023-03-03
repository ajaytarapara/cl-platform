using CIPlatform.Entities.DataModels;
using CIPlatform.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class HomeRepository:IHomeRepository
    {
        private readonly CIPlatformDbContext _ciPlatformDbContext;

        public HomeRepository(CIPlatformDbContext cIPlatformDbContext)
        {
            _ciPlatformDbContext = cIPlatformDbContext; 
        }
     
         public IEnumerable<City>getcity()
        {
            var City = _ciPlatformDbContext.Cities;
            return City;
        }

    }
}
