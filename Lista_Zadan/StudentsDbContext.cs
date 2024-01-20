using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_11.App
{
    internal class StudentsDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }


        public StudentsDbContext() : base()
        {
            string parametryPołączenia = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Student\\source\\repos\\AllLabs\\Lab_11.App\\Database\\Database1.mdf\";Integrated Security=True";
            Database.Connection.ConnectionString = parametryPołączenia;
        }
    }
}
