//using System.Configuration;
//using Desarrollo.Data;

//using Desarrollo.Models;

using Microsoft.AspNetCore.Mvc;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;
using Desarrollo.Services;
using System.Collections;

namespace Desarrollo.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ClienteController:ControllerBase
{
    //private  Context context;
    private readonly ClienteServices service;

    public ClienteController(TestContext context)
    {
        this.service=new ClienteServices(context);

    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateNewCliente([FromBody]PostClienteDto2 body)
    {
        try
        {
            Cliente response=await service.CreateNewCliente(body);
            return Ok(ResponseMessage.SuccessResponse(response));
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}",ex);
            return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
        }
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult> GetAllCliente()
    {
        try
        {
            IEnumerable response=await service.GetAllClientes();
            return Ok(ResponseMessage.SuccessResponse(response));
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}",ex);
            return StatusCode(StatusCodes.Status500InternalServerError,ResponseMessage.ErrorResponse("Error Inesperado"));
            
        }
    }

    [HttpPost]
    [Route("get-by-email")]
    public async Task<ActionResult> GetClienteByEmail([FromBody]EmailBodyDTO body)
    {
        try
        {
            ClienteResponse response=await service.GetClienteByEmail(body.Email);
            return Ok(ResponseMessage.SuccessResponse(response));
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}",ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseMessage.ErrorResponse("Error inesperado"));
        }
    }
    /*[HttpGet]
    [Route("listar")]
    public dynamic ListarClientes()
    {
        
        List<Cliente> response=new List<Cliente>();

        Cliente c1=new Cliente{Id=1, Edad=35, Email="alan.chibilisco@gmail.com", Nombre="Alan"};
        Cliente c2=new Cliente{Id=2, Edad=33, Email="customer@gmail.com", Nombre="Customer"};
        response.Add(c1);
        response.Add(c2);

        return response;
    }

    [HttpPost]
    [Route("crear")]
    public dynamic CrearCliente([FromBody] PostClienteDTO body)
    {
        //desarrollar codigo

        Cliente c=new Cliente{Id=new Random().Next(10), Edad=int.Parse(body.edad), Email=body.email, Nombre=body.nombre};

        

        return new {
            success=true,
            message="Cliente Creado",
            data=c
        };
    }


    [HttpGet]
    [Route("cliente/{id}")]
    public dynamic GetClienteById([FromRoute] string id)
    {
        //desarrollor codigo
    System.Console.WriteLine("Id recibidio: {0}", id);
        return new {
            success=true,
            message="ok",
            data= new{
                resultado="ok"
            }
        };
    }


    [HttpGet]
    [Route("cliente/query")]
    public dynamic GetClienteQuery([FromQuery] string id)
    {
        return new {
            success=true,
            message="Ok",
            data=id
        };
    }


    [HttpGet]
    [Route("token")]
    public dynamic GetToken()
    {
        string token=Request.Headers.Where(element=>element.Key=="Authorization").FirstOrDefault().Value;
        return new{
            success=true,
            message="ok",
            data=token
        };
    }*/


}
