using DigitalBankAssessment.UseCases.Common;
using DigitalBankAssessment.UseCases.DTOs;
using MediatR;

namespace DigitalBankAssessment.UseCases.Queries.GetAllUsersQuery
{
    public class GetAllUsersQuery : IRequest<Result<ICollection<UsuarioResponseDto>>>
    {
    }
}
