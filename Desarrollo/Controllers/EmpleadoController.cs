using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
//using Desarrollo.Data;
using Desarrollo.Dto;
//using Desarrollo.Models;
using Microsoft.AspNetCore.Mvc;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;

namespace Desarrollo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        #region Fields
        private readonly Context _context;
        #endregion

        #region Constructor
        public EmpleadoController(Context context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        [HttpPost]
        [Route("create-simple")]
        public async Task<ActionResult> CreateNewEmpleado([FromBody] PostEmpleadoDto body)
        {
            try
            {
                //TODO: crear registro en CargoEmpleadoTambien
                _context.Empleado.Add(new Modelos.Empleado { Nombre = body.Nombre, Apellido = body.Apellido, EmpresaId = body.EmpresaId });
                var result = await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Success", data = result });
            }
            catch (System.Exception ex)
            {

                return BadRequest(new { success = false, message = "Error", data = ex });
            }
        }

        [HttpPost]
        [Route("create-transaction")]
        public async Task<ActionResult> CreateEmpleadoTransaction([FromBody] PostEmpleadoTDto body)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Empleado e=new Empleado{Nombre=body.Nombre, Apellido=body.Apellido, EmpresaId=body.EmpresaId};
                    _context.Empleado.Add(e);
                    var resEmp=await _context.SaveChangesAsync();

                    int newEmpId=e.Id;

                    CargoEmpleado cargo=new CargoEmpleado{EmpleadoId=newEmpId, CargoId=body.CargoId};
                    _context.CargoEmpleado.Add(cargo);
                    var resCargo=await _context.SaveChangesAsync();


                    scope.Complete();

                    var response=new {
                        resultEmpleado=resEmp,
                        resultCargo=resCargo
                    };
                    return Ok(ResponseMessage.SuccessResponse(response));
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("Error--> {0}",ex);
                    return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
                }
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleadoById([FromRoute] int id)
        {
            try
            {
                if (!EmpleadoExists(id))
                {
                    return NotFound(ResponseMessage.ErrorResponse($"Empleado con id: {id}, NOT FOUND"));
                }

                Empleado response = await _context.Empleado.FindAsync(id);
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }
        #endregion

        #region Private Methods
        private bool EmpleadoExists(int id)
        {
            bool result = _context.Empleado.Any(e => e.Id == id);
            return result;
        }
        #endregion
    }
}