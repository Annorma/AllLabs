using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_10.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var dataDir = $@"{di.Parent?.Parent?.Parent?.FullName}\Database";
            var connString = ConfigurationManager.ConnectionStrings["FileDbConnectionStr"].ConnectionString;
            connString = connString.Replace($"|DataDirectory|", dataDir);
            Current.Resources["connString"] = connString;
            base.OnStartup(e);
        }

    }
}
