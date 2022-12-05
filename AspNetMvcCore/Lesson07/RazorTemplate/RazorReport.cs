using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorEngine;
using RazorEngine.Templating;

namespace RazorTemplate
{
    public class RazorReport : IReport
    {
        public DataForReport Data { get; set; }

        public FileInfo Create(string templateFileName)
        {
            string template = File.ReadAllText(templateFileName);
            string report = Engine.Razor.RunCompile(template, "templateKey", null, Data);
            string resultFilename = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss") + ".html";
            File.WriteAllText(resultFilename, report);

            return new FileInfo(resultFilename);
        }
    }
}
