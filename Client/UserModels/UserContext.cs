using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Suppressors.Models
{
    public class UserContext: DbContext
    {
        public DbSet<Users> Users { get; set; }
        //public UserContext(): base("name=UserContext")
        //{

        //}
    }

}
