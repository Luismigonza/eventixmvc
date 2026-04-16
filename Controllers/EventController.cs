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

    // Panel admin - lista todos los eventos
    public IActionResult Index()
    {
        var events = _context.Events.ToList();
        return View(events);
    }

    // Galería pública con filtro por categoría
    public IActionResult Gallery(string category = null)
    {
        var events = _context.Events.AsQueryable();

        if (!string.IsNullOrEmpty(category))
            events = events.Where(e => e.Category == category);

        return View(events.ToList());
    }

    // Mostrar formulario de creación
    public IActionResult Create()
    {
        return View();
    }

    // Guardar nuevo evento
    [HttpPost]
    public IActionResult Store(Event evento)
    {
        _context.Events.Add(evento);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // Mostrar formulario de edición
    public IActionResult Edit(int id)
    {
        var evento = _context.Events.Find(id);
        if (evento == null) return NotFound();
        return View(evento);
    }

    // Guardar cambios del evento editado
    [HttpPost]
    public IActionResult Update(Event evento)
    {
        _context.Events.Update(evento);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // Eliminar evento
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var evento = _context.Events.Find(id);
        if (evento == null) return NotFound();
        _context.Events.Remove(evento);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}