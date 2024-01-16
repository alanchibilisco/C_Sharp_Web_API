using Desarrollo.Data;
using Desarrollo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desarrollo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly Context _context;

        public EmpresaController(Context context)
        {
            _context = context;
        }

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
    }
}