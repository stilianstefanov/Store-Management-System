﻿namespace DelayedPaymentService.Controllers
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
        public async Task<IActionResult> GetAllClients([FromQuery] ClientsAllQueryModel queryModel)
        {
            var result = await _clientService.GetAllClientsAsync(queryModel, User.GetId()!);

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
            var result = await _clientService.CreateClientAsync(model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return CreatedAtAction(nameof(GetClientById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(string id, [FromBody]ClientUpdateModel model)
        {
            var result = await _clientService.UpdateClientAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateClient(string id, [FromBody] ClientPartialUpdateModel model)
        {
            var result = await _clientService.PartialUpdateClientAsync(id, model, User.GetId()!);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return Ok(result.Data);
        }

        [HttpPatch("{id}/decrease-credit")]
        public async Task<IActionResult> DecreaseClientCredit(string id, [FromBody] ClientDecreaseCreditModel model)
        {
            var result = await _clientService.DecreaseClientCreditAsync(id, model.Amount, User.GetId()!);

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
