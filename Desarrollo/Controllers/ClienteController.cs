using Desarrollo.Dto;
using Desarrollo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Desarrollo.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ClienteController:ControllerBase
{
    [HttpGet]
    [Route("listar")]
    public dynamic ListarClientes()
    {
        //desarrollar codigo;
        //respuesta
        /*return new {
            nombre="Test",
            edad=35
        };*/
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

        /*return new {
            nombre="Cliente",
            edad=32
        };*/

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
    }


}
