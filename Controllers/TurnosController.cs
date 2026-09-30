using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerServidio.Data;
using TallerServidio.Models;

namespace TallerServidio.Controllers
{
    public class TurnosController : Controller
    {
        private readonly TallerServidioContext _context;

        public TurnosController(TallerServidioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new ReservaTurnoViewModel
            {
                Fecha = DateOnly.FromDateTime(DateTime.Today)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ReservaTurnoViewModel modelo)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);

            if (modelo.Fecha < hoy)
            {
                ModelState.AddModelError(
                    nameof(modelo.Fecha),
                    "No se puede reservar una fecha pasada."
                );
            }

            if (modelo.Fecha.DayOfWeek == DayOfWeek.Saturday ||
                modelo.Fecha.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError(
                    nameof(modelo.Fecha),
                    "El taller no toma turnos los sábados ni domingos."
                );
            }

            var cantidadDeTurnos = await _context.Turnos.CountAsync(t =>
                t.Fecha == modelo.Fecha &&
                t.Estado != "Cancelado"
            );

            if (cantidadDeTurnos >= 2)
            {
                ModelState.AddModelError(
                    nameof(modelo.Fecha),
                    "Ese día ya tiene los dos turnos ocupados."
                );
            }

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var turno = new Turno
            {
                Fecha = modelo.Fecha,
                Nombre = modelo.Nombre.Trim(),
                Apellido = modelo.Apellido.Trim(),
                Telefono = modelo.Telefono.Trim(),
                ModeloAuto = modelo.ModeloAuto.Trim(),
                AnioAuto = modelo.AnioAuto,
                Patente = modelo.Patente.Trim().ToUpper(),
                TrabajoSolicitado = modelo.TrabajoSolicitado.Trim(),
                Estado = "Pendiente",
                FechaCreacion = DateTime.Now
            };

            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Confirmacion));
        }

        [HttpGet]
        public IActionResult Confirmacion()
        {
            return View();
        }
    }
}