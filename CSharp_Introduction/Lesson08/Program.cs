using System;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Lesson08
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string greeting = appConfig.AppSettings.Settings["Greeting"].Value;

            Configuration roaming = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = roaming.FilePath };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (configuration.HasFile)
            {
                sb.Append(greeting);

                sb.AppendJoin(' ', "\nYour name is ", configuration.AppSettings.Settings["Name"].Value,
                                    "\nYour age is ", configuration.AppSettings.Settings["Age"].Value,
                                    "\nYour job is ", configuration.AppSettings.Settings["Job"].Value);
                Console.WriteLine(sb.ToString());
            }
            else
            {
                Console.WriteLine(greeting);

                Console.Write("what is your name?: ");
                string name = Console.ReadLine();

                Console.Write("what is your age?: ");
                string age = Console.ReadLine();

                Console.Write("what is your job?: ");
                string jobName = Console.ReadLine();

                configuration.AppSettings.Settings.Add("Name", name);
                configuration.AppSettings.Settings.Add("Age", age);
                configuration.AppSettings.Settings.Add("Job", jobName);
                configuration.Save();
            }
        }
    }
}
