using Desarrollo.Data;
using Desarollo.Models;
using Microsoft.AspNetCore.Mvc;


namespace Desarrollo.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ClienteController:ControllerBase
{

    private ClienteRepository repository;

    public ClienteController(IConfiguration configuration)
    {
        this.repository=new ClienteRepository(configuration);
    }


    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<ResponseMessage>> GetAllClientes()
    {
        try
        {
            var response=await repository.GetClienteList();
            return Ok(ResponseMessage.SuccessResponse(response));
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}", ex);
            return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            
        }
    }
}
