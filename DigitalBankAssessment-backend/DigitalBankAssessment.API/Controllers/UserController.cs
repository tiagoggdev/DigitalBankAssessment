using DigitalBankAssessment.API.Responses;
using DigitalBankAssessment.UseCases.Commands.AddUserCommand;
using DigitalBankAssessment.UseCases.Commands.DeleteUserCommand;
using DigitalBankAssessment.UseCases.Commands.EditUserCommand;
using DigitalBankAssessment.UseCases.DTOs;
using DigitalBankAssessment.UseCases.Queries.GetAllUsersQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankAssessment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<IEnumerable<UsuarioResponseDto>>(false, result.Error ?? "Error al obtener los usuarios"));

            return Ok(new ApiResponse<IEnumerable<UsuarioResponseDto>>(
                true,
                "Usuarios obtenidos correctamente",
                result.Value
            ));

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AddUserCommand command)
        {
            if (command == null)
                return BadRequest(new ApiResponse<string>(false, "No se proporcionaron datos válidos."));

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>(false, result.Error ?? "Error al crear el usuario."));

            return Ok(new ApiResponse<string>(
                true,
                $"El usuario {command.Nombre} ha sido creado correctamente"
            ));

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] EditUserCommand command)
        {
            if (command == null)
                return BadRequest(new ApiResponse<string>(false, "No se proporcionaron datos para editar el usuario."));

            command.Id = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>(false, result.Error ?? "Error al editar el usuario."));

            return Ok(new ApiResponse<string>(
                true,
                $"El usuario {command.Nombre} ha sido editado correctamente"
            ));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>(false, result.Error ?? "Error al eliminar el usuario."));

            return Ok(new ApiResponse<bool>(
                true,
                "El usuario ha sido eliminado correctamente"
            ));
        }
    }
}
