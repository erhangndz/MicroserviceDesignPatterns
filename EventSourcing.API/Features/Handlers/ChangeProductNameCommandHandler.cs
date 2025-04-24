using EventSourcing.API.EventStores;
using EventSourcing.API.Features.Commands;
using MediatR;

namespace EventSourcing.API.Features.Handlers
{
    public class ChangeProductNameCommandHandler(ProductStream productStream): IRequestHandler<ChangeProductNameCommand>
    {
        public async Task Handle(ChangeProductNameCommand request, CancellationToken cancellationToken)
        {
           productStream.NameChanged(request.ChangeProductNameDto);
           await productStream.SaveAsync();
        }
    }
}
