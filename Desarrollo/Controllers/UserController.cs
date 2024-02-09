using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Desarrollo.ContextDB;
using Desarrollo.Modelos;
using Desarrollo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service=service;
        }

        [HttpPost]
        [Route("exist")]
        public async Task<ActionResult> UserExist([FromBody]EmailBodyDTO body)
        {
            try
            {
                bool exist=await _service.UserExists(body.Email);
                var response=new{
                    exist=exist
                };
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }

        [HttpPost]
        [Route("get-by-email")]
        public async Task<ActionResult> GetUserByEmail([FromBody]EmailBodyDTO body)
        {
            try
            {
                User? response=await _service.GetUserByEmail(body.Email);
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }
    }
}