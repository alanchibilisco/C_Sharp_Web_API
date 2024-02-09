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
    public class UserController : ControllerBase
    {
        private IUserService service;

        public UserController(IUserService service)
        {
            this.service=service;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<User> response=await service.GetAll();
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseMessage.ErrorResponse("Error inesperado"));
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNewUser([FromBody]PostUserDto body)
        {
            try
            {
                User response=await service.CreateNewUser(body);
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseMessage.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] PostLoginDto body)
        {
            try
            {
                LoginResponseDto? response= await service.Login(body.Email, body.Password);
                return Ok(ResponseMessage.SuccessResponse(response));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error--> {0}",ex);
                return StatusCode(StatusCodes.Status500InternalServerError,ResponseMessage.ErrorResponse(ex.Message));
            }
        }
    }
}