using Desarrollo.Data;
using Desarrollo.Dto;
using Desarrollo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;

namespace Desarrollo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        #region fields
        private readonly Context _context;
        #endregion

        #region Constructor
        public EmpresaController(Context context)
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

                return Ok(empresas);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return Ok(new { id = 0, nombre = "not-found" });
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
                return Ok(new {success=true, message="Created", data=body});
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return BadRequest($"Error al crear la empresa:--> {ex.Message}");
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresaById([FromRoute] int id)
        {
            try
            {
                Empresa e=await _context.Empresa.FindAsync(id);
                if (e==null)
                {
                    return NotFound();
                }
                return Ok(new {success=true, message="SUCCESS", data=e});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new{success=false, message="ERROR", data=ex});
                
            }
        }

        [HttpPut] 
        [Route("update")]       
        public async Task<ActionResult> UpdateEmpresaById([FromBody] Empresa empresa)
        {
            try
            {
                if(!EmpresaExist(empresa.Id)){
                    return NotFound(new {success=false, message=$"Empresa con id: {empresa.Id}, NOT FOUND"});
                }
                 
                _context.Empresa.Entry(empresa).State=EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                return Ok(new{success=true, message="SUCCESS", data=result});
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(new {success=false, message="ERROR", data=ex});
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

                var result=await _context.Empresa.Where(e=>e.Id==id).Select(empresa=>new{
                    Empresa=empresa,
                    Empleados=_context.Empleado.Where(empleado=>empleado.EmpresaId==empresa.Id).ToList()
                }).FirstOrDefaultAsync();

                return Ok(new{success=true, message="SUCCESS", data=result});
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(new{success=false, message="ERROR", data=ex});
            }
        }
        #endregion
        #region Private Methods
        private bool EmpresaExist(int id)
        {
            bool result=_context.Empresa.Any(e=>e.Id==id);
            return result;
        }
        #endregion
    }
}