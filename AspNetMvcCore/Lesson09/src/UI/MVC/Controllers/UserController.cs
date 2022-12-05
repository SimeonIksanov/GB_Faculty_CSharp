using Domain.Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;

namespace MVC.Controllers;

public class UserController : Controller
{
    private readonly IRepository<User> _repository;
    private readonly ILogger<UserController> _logger;

    public UserController(IRepository<User> repository, ILogger<UserController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync(cancellationToken);
        return View(users);
    }
    public IActionResult Create() => View();
    [HttpPost]
    public async Task<IActionResult> Create(UserCreateViewModel userCreateViewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(userCreateViewModel);

        var newUser = new User
        {
            Name = userCreateViewModel.Name,
            Email = userCreateViewModel.Email
        };
        _ = await _repository.AddAsync(newUser, cancellationToken);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        if (user == null)
            return NotFound();

        return View(user);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var dbUser = await _repository.GetByIdAsync(id,cancellationToken);
        if (dbUser == null)
            return NotFound();
        var deletedUser = await _repository.DeleteAsync(dbUser, cancellationToken);
        return RedirectToAction("Index");
    }
}
