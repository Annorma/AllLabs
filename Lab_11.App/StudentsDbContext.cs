﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            Database.Connection.ConnectionString = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(appDirectory).Parent.Parent.FullName;
            string relativePath = Path.Combine("Database", "Database1.mdf");
            string absolutePath = Path.Combine(projectDirectory, relativePath);

            // Zwróć pełną ścieżkę połączenia
            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{absolutePath}\";Integrated Security=True";
        }

        //public StudentsDbContext() : base()
        //{
        //    string parametryPołączenia = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"..\\..\\Database\\Database1.mdf\";Integrated Security=True";
        //    Database.Connection.ConnectionString = parametryPołączenia;
        //}
    }
}
