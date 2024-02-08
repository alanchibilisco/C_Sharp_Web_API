//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Desarrollo.Data;
using Desarrollo.ContextDB;
//using Desarrollo.Models;
using Desarrollo.Modelos;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using Desarrollo.Services;

namespace Desarrollo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly Context _context;
        private readonly CargoServices service;
        public CargoController(Context context)
        {
            _context=context;
            service= new CargoServices(context);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNewCargo([FromBody] PostCargoDto body)        
        {
            try
            {
                /*Cargo c=new Cargo{Nombre_Cargo=body.Nombre_Cargo};
                _context.Cargo.Add(c);
                var response=await _context.SaveChangesAsync();*/
                Cargo response=await service.CreateNewCargo(body);
                return Ok(ResponseMessage.SuccessResponse(body));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }

        [HttpGet]
        [Route("cargos")]
        public async Task<ActionResult> GetAllCargos()
        {
            try
            {
                //List<Cargo> cargos=await _context.Cargo.ToListAsync();
                var cargos=await service.GetCargosTodos();
                //throw new Exception("Excepcion");
                //return Ok(new{success=true, message="SUCCESS", data=cargos});
                return Ok(ResponseMessage.SuccessResponse(cargos));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                //return BadRequest(new{success=false, message="ERROR"});
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }
    }
}