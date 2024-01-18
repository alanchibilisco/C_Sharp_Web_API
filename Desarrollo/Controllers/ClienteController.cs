using Desarrollo.Data;
using Desarrollo.Dto;
using Desarrollo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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
    public async Task<ActionResult> GetAllClientes()
    {
        try
        {
            var response=await repository.GetClienteList();
            return Ok(response);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}", ex);
            return BadRequest("Error inesperado");
            
        }
    }
}
