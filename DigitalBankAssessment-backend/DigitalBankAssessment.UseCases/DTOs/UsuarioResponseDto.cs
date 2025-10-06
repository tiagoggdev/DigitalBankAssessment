using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBankAssessment.UseCases.DTOs
{
    public record UsuarioResponseDto(int Id, string Nombre, DateTime FechaNacimiento, string Sexo);
}
