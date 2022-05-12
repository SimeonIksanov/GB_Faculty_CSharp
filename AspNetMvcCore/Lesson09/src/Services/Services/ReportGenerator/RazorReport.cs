using Domain.Entities;
using Interfaces;
using Microsoft.Extensions.Options;
using RazorEngine.Templating;

namespace Services.ReportGenerator
{
    public class RazorReport : IReport
    {
        private RazorReportSettings _settings;
        public RazorReport(IOptions<RazorReportSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Create(Report reportData)
        {
            string template = File.ReadAllText(_settings.TemplateFilePath);
            return RazorEngine.Engine.Razor.RunCompile(template, "aaaa", null, reportData);
        }
    }
}
