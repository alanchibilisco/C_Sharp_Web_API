using System;
using System.Collections;
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
    public class EmpleadoController : ControllerBase
    {
        #region Fields
        private EmpleadoServices service;
        #endregion
        #region Constructor
        public EmpleadoController(IConfiguration configuration)
        {
            this.service=new EmpleadoServices(configuration);
        }
        #endregion
        #region Public Methods
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetEmpleados()
        {
            try
            {
                IEnumerable response=await service.GetEmpleados();
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
                
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNewEmpleado([FromBody]PostEmpleadoTDto body)
        {
            try
            {
                EmpleadosDTO response=await service.CreateEmpleado(body);
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult> GetEmpleadoById([FromRoute] int id)
        {
            try
            {
                EmpleadosDTO response=await service.GetEmpleadoById(id);
                return Ok(ResponseMessage.SuccessResponse(response));

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);                
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }
        #endregion
    }
}