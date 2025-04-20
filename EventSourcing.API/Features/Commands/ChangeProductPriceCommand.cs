using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Features.Commands;

public class ChangeProductPriceCommand: IRequest
{

    public ChangeProductPriceDto ChangeProductPriceDto { get; set; }
}