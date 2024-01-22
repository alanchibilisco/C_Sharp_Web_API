using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desarollo.Models;
using Desarrollo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desarrollo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private EmpresaServices service;

        public EmpresaController(IConfiguration configuration)
        {
            this.service=new EmpresaServices(configuration);
        }

        [HttpGet]
        [Route("get-all-with-customer")]
        public async Task<ActionResult> GetEmpresaConClientes()
        {
            try
            {
                var response = await service.GetEmpresasConCLientes();
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }

        [HttpGet]
        [Route("get-all-with-empleados")]
        public async Task<ActionResult> GetEmpresaConEmpleados()
        {
            try
            {
                List</*EmpresaEmpleadosDTO*/EmpresaEmpleadoListResponse> response=await service.GetEmpresaConEmpleados();
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }
    }
}