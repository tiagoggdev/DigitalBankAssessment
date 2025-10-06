using DigitalBankAssessment.DataAccess.Persistence;
using DigitalBankAssessment.UseCases.Common;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankAssessment.UseCases.Commands.AddUserCommand
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Result<bool>>
    {
        private readonly AppDbContext _context;

        public AddUserHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sexo = char.ToUpper(request.Sexo);

                if (sexo != 'F' && sexo != 'M')
                {
                    return Result<bool>.Fail("El campo 'Sexo' solo puede ser 'F' (Femenino) o 'M' (Masculino).");
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_CreateUsuario @Nombre, @FechaNacimiento, @Sexo",
                        new SqlParameter("@Nombre", request.Nombre),
                        new SqlParameter("@FechaNacimiento", request.FechaNacimiento),
                        new SqlParameter("@Sexo", request.Sexo));

                return Result<bool>.Ok(true);
            } catch (Exception ex)
            {
                return Result<bool>.Fail($"Error al crear el usuario: {ex.Message}");
            }
        }
    }
}
