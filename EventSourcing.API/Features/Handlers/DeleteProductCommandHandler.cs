using EventSourcing.API.EventStores;
using EventSourcing.API.Features.Commands;
using MediatR;

namespace EventSourcing.API.Features.Handlers
{
    public class DeleteProductCommandHandler(ProductStream productStream) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            productStream.Deleted(request.Id);
            await productStream.SaveAsync();
        }
    }
}
