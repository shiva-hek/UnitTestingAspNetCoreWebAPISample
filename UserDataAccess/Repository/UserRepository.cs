using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserDomain;
using UserDomain.Repository;

namespace UserDataAccess.Repository
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(UserDbContext context):base(context) 
        {
        }
    }
}
