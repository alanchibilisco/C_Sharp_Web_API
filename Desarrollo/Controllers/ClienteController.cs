﻿using Desarrollo.Data;
using Desarollo.Models;
using Microsoft.AspNetCore.Mvc;
using Desarrollo.Services;


namespace Desarrollo.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ClienteController:ControllerBase
{

    private ClienteServices service;

    public ClienteController(IConfiguration configuration)
    {
        this.service=new ClienteServices(configuration);
    }


    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<ResponseMessage>> GetAllClientes()
    {
        try
        {
            var response=await service.GetClienteList();
            if (response==null)
            {
                return NotFound(ResponseMessage.ErrorResponse("Clientes NOT FOUND"));
            }
            return Ok(ResponseMessage.SuccessResponse(response));
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}", ex);
            return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            
        }
    }

    [HttpPost]
    [Route("new")]
    public async Task<ActionResult> NewCliente([FromBody]PostClienteDto2 body)
    {
        try
        {
            Cliente response=await service.NewCliente(body);
            return Ok(ResponseMessage.SuccessResponse(response));
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}", ex);
            return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
        }
    }

    [HttpPost]
    [Route("get-cliente")]
    public async Task<ActionResult> GetClienteByEmail([FromBody]GetClienteByEmail body)
    {
        try
        {
            Cliente response=await service.GetClienteByEmail(body.Email);
            return Ok(ResponseMessage.SuccessResponse(response));
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error--> {0}",ex);
            return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
        }
    }
}
