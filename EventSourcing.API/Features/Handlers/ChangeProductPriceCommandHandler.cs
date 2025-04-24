using EventSourcing.API.EventStores;
using EventSourcing.API.Features.Commands;
using MediatR;

namespace EventSourcing.API.Features.Handlers
{
    public class ChangeProductPriceCommandHandler(ProductStream productStream): IRequestHandler<ChangeProductPriceCommand>
    {
        public async Task Handle(ChangeProductPriceCommand request, CancellationToken cancellationToken)
        {
           productStream.PriceChanged(request.ChangeProductPriceDto);
           await productStream.SaveAsync();
        }
    }
}
