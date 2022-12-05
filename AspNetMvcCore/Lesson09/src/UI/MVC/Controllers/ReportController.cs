using Domain.Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using Services;

namespace MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRepository<Report> _reportRepository;

        public ReportController(IRepository<Report> reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var reports = await _reportRepository.GetAllAsync(cancellationToken);
            return View(reports);
        }

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(ReportCreateViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newReport = new Report
            {
                Body = model.Body,
                Title = model.Title,
                SendAt = model.SendAt ?? DateTime.Now,
                CreatedAt = DateTime.Now,
            };
            _ = await _reportRepository.AddAsync(newReport, cancellationToken);

            return RedirectToAction("Index");
        }
    }
}
