using System.IO;

namespace Tech_Journal_ConsoleApp
{
    public class Settings
    {
        
        public bool AppSettingsExists(string path)
        {
            return File.Exists(path);
        }

        public void SaveSenderInfo(AppInfo appInfo, string path)
        {
            using var sw = File.AppendText(path);
            sw.WriteLine(appInfo.User);
            sw.WriteLine(appInfo.Password);

        }

        public AppInfo ReadSenderInfo(string path)
        {
            using var emailSettings = new StreamReader(path);
            var info = new AppInfo {User = emailSettings.ReadLine(), Password = emailSettings.ReadLine()};
            return info;
        }

        public void WriteDatabase(AppInfo appInfo, string path)
        {
            using var sw = File.AppendText(path);
            sw.WriteLine(appInfo.Datasource);
            sw.WriteLine(appInfo.Database);
            sw.WriteLine(appInfo.DatabaseUser);
            sw.WriteLine(appInfo.DatabasePassword);

        }

        public AppInfo ReadDatabase(string path)
        {
            using var emailSettings = new StreamReader(path);
            var info = new AppInfo();
            info.Datasource = emailSettings.ReadLine();
            info.Database = emailSettings.ReadLine();
            info.DatabaseUser = emailSettings.ReadLine();
            info.DatabasePassword = emailSettings.ReadLine();
            
            return info;
        }


    }
}