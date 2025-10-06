using DigitalBankAssessment.UseCases.Common;
using MediatR;

namespace DigitalBankAssessment.UseCases.Commands.EditUserCommand
{
    public class EditUserCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public char? Sexo { get; set; }
    }
}
