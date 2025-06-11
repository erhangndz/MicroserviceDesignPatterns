using EventSourcing.API.DTOs;
using EventSourcing.API.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {
      

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            var command = new CreateProductCommand{CreateProductDto = createProductDto};
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeName(ChangeProductNameDto changeProductNameDto)
        {
            var command = new ChangeProductNameCommand { ChangeProductNameDto = changeProductNameDto };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ChangePrice(ChangeProductPriceDto changeProductPriceDto)
        {
            var command = new ChangeProductPriceCommand { ChangeProductPriceDto = changeProductPriceDto };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
