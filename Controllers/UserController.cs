using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataDriven.Data;
using DataDriven.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using DataDriven.Services;

namespace DataDriven.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        //===============================POST - Criando user============================================
       [HttpPost]
       [Route("")]
       [AllowAnonymous]

       public async Task<ActionResult<User>> Post([FromServices] DataContext context, [FromBody] User model)
       {
           if(!ModelState.IsValid) return BadRequest(ModelState);
           
           try 
           {
               context.Users.Add(model);
               await context.SaveChangesAsync();
               return model;
           } 
           
           catch (Exception) 
           {
               return BadRequest(new { message = "Não foi possível criar usuário" });
           }
       }

        //===============================POST - Login============================================

       [HttpPost]
       [Route("login")]
       [AllowAnonymous]

       public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] User model)
       {
           var user = await context.Users
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();
            
            //se os usuários foram encontrados no banco
            if(user == null) return NotFound(new { message = "Usuário ou senha inválidos"});
        
            var token = TokenService.GenerateToken(user);

            return new{
                user = user,
                token = token
            };       
       }

        //===============================GET-Anonimo============================================
        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonimo() => "Anonimo";

        [HttpGet]
        [Route("autenticado")]
        [AllowAnonymous]
        public string Autenticado() => "Autenticado";
        
        [HttpGet]
        [Route("funcionario")]
        [Authorize(Roles="employee")]
        public string Funcionario() => "funcionario";

        [HttpGet]
        [Route("gerente")]
        [Authorize(Roles="manager")]
        public string Gerente() => "Gerente";
    }
}