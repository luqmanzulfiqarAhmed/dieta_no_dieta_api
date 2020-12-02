using EF_DietaNoDietaApi.Model;
using EF_DietaNoDietaApi.MySql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Repositry
{
    public class UserRepositry: IRepositry
    {

        private MySqlDbContext dbContext=null;
        public UserRepositry(MySqlDbContext context) {

            dbContext = context;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return dbContext.Users.ToList();
        }
    }
}
