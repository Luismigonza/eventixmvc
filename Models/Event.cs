using System.ComponentModel.DataAnnotations;

namespace EventixMVC.Models;

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string Description { get; set; }

    [Required(ErrorMessage = "La ubicación es obligatoria")]
    public string Location { get; set; }

    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    public string Category { get; set; }

    public string Status { get; set; } = "Activo";

    [Range(1, 100000, ErrorMessage = "La capacidad debe ser mayor a 0")]
    public int Capacity { get; set; }

    [Range(0, 10000000, ErrorMessage = "El precio no puede ser negativo")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "El nombre del organizador es obligatorio")]
    public string OrganizerName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}