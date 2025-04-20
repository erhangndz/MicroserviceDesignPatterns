using MediatR;

namespace EventSourcing.API.Features.Commands;

public class DeleteProductCommand: IRequest
{
    public Guid Id { get; set; }
}