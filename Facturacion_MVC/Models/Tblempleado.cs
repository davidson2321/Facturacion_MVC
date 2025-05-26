using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facturacion_MVC.Models;

public partial class Tblempleado
{
    [Key]
    public int IdEmpleado { get; set; }
    [Display(Name = "Nombre")]
    public string StrNombre { get; set; } = null!;
    [Display(Name = "Número de Documento")]

    public long NumDocumento { get; set; }

    [Display(Name = "Dirección")]
    public string? StrDireccion { get; set; }
    [Display(Name = "Telefono")]

    public string? StrTelefono { get; set; }
    [Display(Name = "Email")]

    public string? StrEmail { get; set; }

    [Display(Name = "Rol Empleado")]
    public int? IdRolEmpleado { get; set; }
    [Display(Name = "Fecha de Ingreso")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmIngreso { get; set; }
    [Display(Name = "Fecha de Retiro")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmRetiro { get; set; }
    [Display(Name = "Datos Adicionales")]
    public string? StrDatosAdicionales { get; set; }

    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifico { get; set; }

    public virtual Tblrole? IdRolEmpleadoNavigation { get; set; }

    public virtual ICollection<Tblfactura> Tblfacturas { get; set; } = new List<Tblfactura>();

    public virtual ICollection<Tblseguridad> Tblseguridads { get; set; } = new List<Tblseguridad>();
}
