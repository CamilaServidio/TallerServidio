using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TallerServidio.Models;

[Index("Fecha", "Estado", Name = "IX_Turnos_Fecha_Estado")]
public partial class Turno
{
    [Key]
    public int Id { get; set; }

    public DateOnly Fecha { get; set; }

    [StringLength(60)]
    public string Nombre { get; set; } = null!;

    [StringLength(60)]
    public string Apellido { get; set; } = null!;

    [StringLength(30)]
    public string Telefono { get; set; } = null!;

    [StringLength(80)]
    public string ModeloAuto { get; set; } = null!;

    public int AnioAuto { get; set; }

    [StringLength(10)]
    public string Patente { get; set; } = null!;

    [StringLength(500)]
    public string? TrabajoSolicitado { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }
}
