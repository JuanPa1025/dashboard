using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Glamping_Addventure.Models;

public partial class Paquete
{
    public int Idpaquete { get; set; }

    [Display(Name = "Nombre del Paquete")]
    public string? NombrePaquete { get; set; }

    [Display(Name = "Imagen")]
    public byte[]? ImagenPaquete { get; set; }

    public string? Descripcion { get; set; }

    [Display(Name = "Habitacion")]
    public int? Idhabitacion { get; set; }

    [Display(Name = "Servicio")]
    public int? Idservicio { get; set; }

    public double? Precio { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; } = new List<DetalleReservaPaquete>();

    [Display(Name = "Habitacion")]
    public virtual Habitacion? IdhabitacionNavigation { get; set; }

    [Display(Name = "Servicio")]
    public virtual Servicio? IdservicioNavigation { get; set; }

    [NotMapped] // No se mapea a la base de datos
    public string? ImagenDataURL { get; set; }
}
