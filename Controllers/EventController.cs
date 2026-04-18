using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventixMVC.Data;
using EventixMVC.Models;

namespace EventixMVC.Controllers;

public class EventController : Controller
{
    private readonly AppDbContext _context;

    public EventController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var events = _context.Events
            .Where(e => e.Status != "Eliminado")
            .ToList();
        return View(events);
    }

    public IActionResult Gallery(string category = null)
    {
        var events = _context.Events
            .Where(e => e.Status == "Activo")
            .AsQueryable();

        if (!string.IsNullOrEmpty(category))
            events = events.Where(e => e.Category == category);

        return View(events.ToList());
    }

    public IActionResult Detail(int id)
    {
        var evento = _context.Events.Find(id);
        if (evento == null) return NotFound();
        return View(evento);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Store(Event evento)
    {
        if (!ModelState.IsValid)
            return View("Create", evento);

        evento.CreatedAt = DateTime.Now;
        _context.Events.Add(evento);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var evento = _context.Events.Find(id);
        if (evento == null) return NotFound();
        return View(evento);
    }

    [HttpPost]
    public IActionResult Update(Event evento)
    {
        if (!ModelState.IsValid)
            return View("Edit", evento);

        _context.Events.Update(evento);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var evento = _context.Events.Find(id);
        if (evento == null) return NotFound();
        evento.Status = "Eliminado";
        _context.Events.Update(evento);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}