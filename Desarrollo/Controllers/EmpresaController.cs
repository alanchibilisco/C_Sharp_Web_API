//using Desarrollo.Data;

//using Desarrollo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Expressions;
using Desarrollo.Modelos;
using Desarrollo.ContextDB;

namespace Desarrollo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        #region fields
        private readonly TestContext _context;
        #endregion

        #region Constructor
        public EmpresaController(TestContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [Route("empresas")]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            try
            {
                var empresas = await _context.Empresa.ToListAsync();

                return Ok(ResponseMessage.SuccessResponse(empresas));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Empresa>> CreateEmpresa([FromBody] PostEmpresaDto body)
        {
            try
            {

                Empresa e = new Empresa { Nombre = body.nombre };
                _context.Empresa.Add(e);
                var result = await _context.SaveChangesAsync();
                return Ok(ResponseMessage.SuccessResponse(result));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresaById([FromRoute] int id)
        {
            try
            {
                Empresa e = await _context.Empresa.FindAsync(id);
                if (e == null)
                {
                    return NotFound(ResponseMessage.ErrorResponse($"Empresa con id: {id}, NOT FOUND"));
                }
                return Ok(ResponseMessage.SuccessResponse(e));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));

            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateEmpresaById([FromBody] Empresa empresa)
        {
            try
            {
                if (!EmpresaExist(empresa.Id))
                {
                    return NotFound(ResponseMessage.ErrorResponse($"Empresa con id: {empresa.Id}, NOT FOUND"));
                }

                _context.Empresa.Entry(empresa).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                return Ok(ResponseMessage.SuccessResponse(result));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }

        [HttpGet]
        [Route("empresa-con-empleados/{id}")]
        public async Task<ActionResult> GetEmpresaConEmpleadosById([FromRoute] int id)
        {
            try
            {
                /*var result=await _context.Empresa.Where(empresa=>empresa.Id==id).Join(_context.Empleado,empresa=>empresa.Id,empleado=>empleado.EmpresaId,(empresa, empleado)=>new{
                     Empresa=empresa,
                     Empleados=empleado
                 }).ToListAsync();*/

                var result = await _context.Empresa.Where(e => e.Id == id).Select(empresa => new
                {
                    Empresa = empresa,
                    Empleados = _context.Empleado.Where(empleado => empleado.EmpresaId == empresa.Id).ToList()
                }).FirstOrDefaultAsync();

                return Ok(ResponseMessage.SuccessResponse(result));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }

        [HttpGet]
        [Route("empresas-complete")]
        public async Task<ActionResult> GetEmpresasComplete()
        {
            try
            {
                /*var response=_context.Empresa.Select(empresa=>new{
                    Empresa=empresa,
                    Empleados=_context.Empleado.Where(empleado=>empleado.EmpresaId==empresa.Id).ToList()
                });*/
                var response = _context.Empresa.Select(empresa => new
                {
                    Empresa = empresa,
                    Empleados = _context.Empleado.Where(empleado => empleado.EmpresaId == empresa.Id).Select(empleado => new
                    {
                        //Empleado = empleado,
                        nombre=empleado.Nombre,
                        apellido=empleado.Apellido,
                        cargo=_context.CargoEmpleado
                        .Where(cargoempleado => cargoempleado.EmpleadoId == empleado.Id)
                        .Select(cargoempleado => _context.Cargo
                            .Where(cargo => cargo.Id == cargoempleado.CargoId)
                            .Select(cargo => cargo.Nombre_Cargo).First()
                        ).First()
                    }).ToList()
                }).ToList();
                if (response == null)
                {
                    return NotFound(ResponseMessage.ErrorResponse("Empresas NOT FOUND"));
                }
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest(ResponseMessage.ErrorResponse("Error inesperado"));
                throw;
            }
        }
        #endregion

        #region Private Methods
        private bool EmpresaExist(int id)
        {
            bool result = _context.Empresa.Any(e => e.Id == id);
            return result;
        }
        #endregion
    }
}