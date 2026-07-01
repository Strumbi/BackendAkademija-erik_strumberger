using BackendAkademija.Application.Products.Queries;
using BackendAkademija.Application.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAkademija.api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var resutl = await mediator.Send(new GetProductsListQuery(), cancellationToken);
        return Ok(resutl);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        
        if(result is null) return NotFound(new { Message = $"Product wtih ID {id} is not efund." });
        
        return Ok(result);
    }
    
    //TODO: Dovristi FilterProducts SearchProducts
}