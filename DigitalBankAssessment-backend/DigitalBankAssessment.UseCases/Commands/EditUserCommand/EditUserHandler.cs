using DigitalBankAssessment.DataAccess.Persistence;
using DigitalBankAssessment.UseCases.Common;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankAssessment.UseCases.Commands.EditUserCommand
{
    public class EditUserHandler : IRequestHandler<EditUserCommand, Result<bool>>
    {
        private readonly AppDbContext _context;

        public EditUserHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Usuarios.FindAsync(request.Id);
                if (user == null) return Result<bool>.Fail("Usuario no encontrado");

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_EditUsuario @Id, @Nombre, @FechaNacimiento, @Sexo",
                    new SqlParameter("@Id", request.Id),
                    new SqlParameter("@Nombre", (object?)request.Nombre ?? DBNull.Value),
                    new SqlParameter("@FechaNacimiento", (object?)request.FechaNacimiento ?? DBNull.Value),
                    new SqlParameter("@Sexo", (object?)request.Sexo ?? DBNull.Value)
                );

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error al editar el usuario: {ex.Message}");
            }
        }
    }
}
