using System.Collections.Generic;
using System.Threading.Tasks;
using DataDriven.Data;
using DataDriven.Models;
using Microsoft.AspNetCore.Mvc;

//https://localhost:5001

namespace DataDriven.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        //---------------GET---------------
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            return new Category();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Category>>> GetById()
        {
            return new List<Category>();
        }
        //---------------POST---------------
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Post(
                [FromBody]Category model,
                [FromServices] DataContext context
                )
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);

            
        }

        //---------------PUT---------------
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Put(int id, [FromBody]Category model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            //se o id informado é o msm do modelo
            if(model.Id == id) return NotFound(new {message = "Categoria não encontrada"});;

            return null;
        }

        //---------------DELETE---------------
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Delete()
        {
            return Ok();
        }
    }
}