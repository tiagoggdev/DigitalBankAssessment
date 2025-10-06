using DigitalBankAssessment.DataAccess.Persistence;
using DigitalBankAssessment.UseCases.Common;
using DigitalBankAssessment.UseCases.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankAssessment.UseCases.Queries.GetAllUsersQuery
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<ICollection<UsuarioResponseDto>>>
    {
        private readonly AppDbContext _context;

        public GetAllUsersHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ICollection<UsuarioResponseDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarios = await _context.Usuarios
                    .FromSqlRaw("EXEC sp_GetAllUsuarios")
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var usuariosDto = usuarios
                    .Select(u => new UsuarioResponseDto(u.Id, u.Nombre, u.FechaNacimiento, u.Sexo))
                    .ToList();

                return Result<ICollection<UsuarioResponseDto>>.Ok(usuariosDto);
            }
            catch (Exception ex)
            {
                return Result<ICollection<UsuarioResponseDto>>.Fail($"Error al obtener los usuarios: {ex.Message}");
            }
        }
    }
}
