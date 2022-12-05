using System.Collections.Generic;
using System.IO;

namespace RazorTemplate
{
    public interface IReport
    {
        DataForReport Data { get; set; }
        FileInfo Create(string templateFileName);
    }
}