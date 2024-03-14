namespace DelayedPaymentService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Utilities.Extensions;
    using Data.ViewModels.Client;
    using Services.Contracts;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _clientService.GetAllClientsAsync(User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpGet("{id}", Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(string id)
        {
           var result = await _clientService.GetClientByIdAsync(id, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _clientService.CreateClientAsync(model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return CreatedAtAction(nameof(GetClientById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(string id, [FromBody]ClientUpdateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _clientService.UpdateClientAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            var result = await _clientService.DeleteClientAsync(id, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return NoContent();
        }
    }
}
