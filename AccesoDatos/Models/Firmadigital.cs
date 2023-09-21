using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoDatos.Models;

public partial class Firmadigital
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int IdFirma { get; set; }

    public char TipoFirma { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string RepresentanteLegal { get; set; } = null!;

    public string EmpresaAcreditadora { get; set; } = null!;

    public DateTime FechaEmision { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public string? RutaRubrica { get; set; }

    public string? CertificadoDigital { get; set; }
}
