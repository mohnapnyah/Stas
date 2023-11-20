using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGuide
{
    public class FishDB
    {
       FishGuideEntities entities = new FishGuideEntities();
        public static User FindUserByLogin(string login)
        {
            using (var context = new FishGuideEntities())
            {
                // Поиск пользователя по логину
                var user = context.User.FirstOrDefault(u => u.Login == login);

                return user;
            }
        }
    }
}

