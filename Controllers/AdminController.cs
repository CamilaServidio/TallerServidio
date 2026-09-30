using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerServidio.Data;

namespace TallerServidio.Controllers
{
    public class AdminController : Controller
    {
        private readonly TallerServidioContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(
            TallerServidioContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private bool EstaAutenticado()
        {
            return HttpContext.Session.GetString("AdminAutenticado") == "Si";
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (EstaAutenticado())
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string password)
        {
            var passwordCorrecta =
                _configuration["Admin:Password"];

            if (!string.IsNullOrWhiteSpace(passwordCorrecta) &&
                password == passwordCorrecta)
            {
                HttpContext.Session.SetString("AdminAutenticado", "Si");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "La contraseña es incorrecta.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string? buscar,
            DateOnly? fecha,
            string? estado)
        {
            if (!EstaAutenticado())
            {
                return RedirectToAction(nameof(Login));
            }

            var consulta = _context.Turnos.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.Trim();

                consulta = consulta.Where(t =>
                    t.Nombre.Contains(buscar) ||
                    t.Apellido.Contains(buscar) ||
                    t.Patente.Contains(buscar) ||
                    t.Telefono.Contains(buscar));
            }

            if (fecha.HasValue)
            {
                consulta = consulta.Where(t => t.Fecha == fecha.Value);
            }

            if (!string.IsNullOrWhiteSpace(estado))
            {
                consulta = consulta.Where(t => t.Estado == estado);
            }

            var turnos = await consulta
                .OrderBy(t => t.Fecha)
                .ThenBy(t => t.Apellido)
                .ToListAsync();

            ViewBag.Buscar = buscar;
            ViewBag.Fecha = fecha;
            ViewBag.Estado = estado;

            return View(turnos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarAtendido(int id)
        {
            if (!EstaAutenticado())
            {
                return RedirectToAction(nameof(Login));
            }

            var turno = await _context.Turnos.FindAsync(id);

            if (turno != null)
            {
                turno.Estado = "Atendido";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            if (!EstaAutenticado())
            {
                return RedirectToAction(nameof(Login));
            }

            var turno = await _context.Turnos.FindAsync(id);

            if (turno != null)
            {
                turno.Estado = "Cancelado";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
