using EF_DietaNoDietaApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Repositry
{
    public interface IRepositry
    {
        IEnumerable<UserModel> GetUsers();
    }
}
