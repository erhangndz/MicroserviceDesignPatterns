using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Features.Commands;

public class CreateProductCommand: IRequest
{

    public CreateProductDto CreateProductDto { get; set; }
}