using DigitalBankAssessment.UseCases.Common;
using MediatR;

namespace DigitalBankAssessment.UseCases.Commands.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
    