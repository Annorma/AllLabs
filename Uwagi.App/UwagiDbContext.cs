using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Uwagi.App
{
    internal class UwagiDbContext : DbContext
    {
        public DbSet<Uwaga> Uwagi { get; set; }


        public UwagiDbContext() : base()
        {
            string parametryPołączenia = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Student\\source\\repos\\AllLabs\\Uwagi.App\\Database\\Database2.mdf\";Integrated Security=True";
            Database.Connection.ConnectionString = parametryPołączenia;
        }
    }
}
