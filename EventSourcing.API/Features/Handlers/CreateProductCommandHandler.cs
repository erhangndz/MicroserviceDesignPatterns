using EventSourcing.API.EventStores;
using EventSourcing.API.Features.Commands;
using MediatR;

namespace EventSourcing.API.Features.Handlers
{
    public class CreateProductCommandHandler(ProductStream productStream): IRequestHandler<CreateProductCommand>
    {
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
           productStream.Created(request.CreateProductDto);

           await productStream.SaveAsync();
        }
    }
}
