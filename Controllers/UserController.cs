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

        [HttpGet]
        [Route("")]
        // [Authorize(Roles="manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();
            
            return users;
        }


        //===============================POST - Criando user============================================
       [HttpPost]
       [Route("")]
       [AllowAnonymous]

       public async Task<ActionResult<User>> Post([FromServices] DataContext context, [FromBody] User model)
       {
           if(!ModelState.IsValid) return BadRequest(ModelState);
           
           try 
           {
               model.Role = "employee";
               context.Users.Add(model);
               await context.SaveChangesAsync();

               model.Password = "";
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
        public async Task<ActionResult<dynamic>> Authenticate(
                    [FromServices] DataContext context,
                    [FromBody]User model)
        {
            var user = await context.Users
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            // Esconde a senha
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }
        //===============================PUT============================================
         [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody]User model)
        {
            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se o ID informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Usuário não encontrada" });

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });

            }
        }
    }
}