using DigitalBankAssessment.DataAccess.Persistence;
using DigitalBankAssessment.UseCases.Common;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankAssessment.UseCases.Commands.DeleteUserCommand
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly AppDbContext _context;

        public DeleteUserHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Usuarios.FindAsync(request.Id);
                if (user == null) return Result<bool>.Fail("Usuario no encontrado");
                if (!user.Activo) return Result<bool>.Fail("El usuario ya se encuentra deshabilitado");

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_DeleteUsuario @Id",
                        new SqlParameter("@Id", request.Id));

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error al borrar el usuario: {ex.Message}");
            }
        }
    }
}
