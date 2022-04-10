using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HomeWork9._4
{
    class AppContext : DbContext
    {
        public DbSet<File> FileList { get; set; }

        public AppContext() : base("DefaultConnection") { }
    }
}
