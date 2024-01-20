using Do_Kolokwium_02.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Do_Kolokwium_02
{
    internal class FilmsDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public FilmsDbContext() : base()
        {
            Database.Connection.ConnectionString = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(appDirectory).Parent.Parent.FullName;
            string relativePath = Path.Combine("Database", "Films.mdf");
            string absolutePath = Path.Combine(projectDirectory, relativePath);
            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{absolutePath}\";Integrated Security=True";
            //return "Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = "C:\Users\delid\source\repos\AllLabs\Do_Kolokwium_02\bin\Debug\Database\Films.mdf"; Integrated Security = True";
        }

    }
}
