using DigitalBankAssessment.UseCases.Common;
using MediatR;

namespace DigitalBankAssessment.UseCases.Commands.AddUserCommand
{
    public class AddUserCommand : IRequest<Result<bool>>
    {
        public required string Nombre {get; set;}
        public required DateTime FechaNacimiento { get; set; }
        public required char Sexo { get; set;}

    }
}
