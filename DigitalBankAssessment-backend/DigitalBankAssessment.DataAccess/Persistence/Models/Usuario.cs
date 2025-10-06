using System;
using System.Collections.Generic;

namespace DigitalBankAssessment.DataAccess.Persistence.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public bool Activo { get; set; }
}
