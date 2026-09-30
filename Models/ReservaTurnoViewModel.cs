using System.ComponentModel.DataAnnotations;

namespace TallerServidio.Models
{
    public class ReservaTurnoViewModel
    {
        [Required(ErrorMessage = "Seleccioná una fecha.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha del turno")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "Ingresá el nombre.")]
        [StringLength(60)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresá el apellido.")]
        [StringLength(60)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresá un teléfono.")]
        [StringLength(30)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresá el modelo del auto.")]
        [StringLength(80)]
        [Display(Name = "Modelo del auto")]
        public string ModeloAuto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresá el año del auto.")]
        [Range(1900, 2100, ErrorMessage = "Ingresá un año válido.")]
        [Display(Name = "Año del auto")]
        public int AnioAuto { get; set; }

        [Required(ErrorMessage = "Ingresá la patente.")]
        [StringLength(10)]
        public string Patente { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contanos qué trabajo necesita el vehículo.")]
        [StringLength(500)]
        [Display(Name = "Trabajo solicitado")]
        public string TrabajoSolicitado { get; set; } = string.Empty;
    }
}
