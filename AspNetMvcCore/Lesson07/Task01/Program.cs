using System;
using System.IO;
using RazorTemplate;

namespace Task01
{
    class Program
    {
        static void Main()
        {
            DataForReport dataForReport = new();

            var report = new RazorReport();

            CreateReport(report, dataForReport, "RazorTemplate.txt");

            Console.WriteLine("I'm done");
        }

        static FileInfo CreateReport(IReport report, DataForReport data, string templateFileName)
        {
            report.Data = data;
            var retVal = report.Create(templateFileName);
            return retVal;
        }
    }
}
