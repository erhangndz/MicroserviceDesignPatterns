using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Features.Commands;

public class ChangeProductNameCommand: IRequest
{
    public ChangeProductNameDto ChangeProductNameDto { get; set; }

}