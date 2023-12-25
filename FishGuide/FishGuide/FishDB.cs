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
       RecipeBookEntities entities = new RecipeBookEntities();
        public static User FindUserByLogin(string login)
        {
            using (var context = new RecipeBookEntities())
            {
                // Поиск пользователя по логину
                var user = context.User.FirstOrDefault(u => u.Login == login);

                return user;
            }
        }
    }
}

