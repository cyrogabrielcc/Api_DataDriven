using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataDriven.Data;
using DataDriven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataDriven.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {

//=============================================================GET=========================================================================//
       [HttpGet]
       [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            // var products = produtos + categoria-> Include, está incluindo a categoria
            var products = await context
                .Products
                .Include(x=> x.Category)
                .AsNoTracking()
                .ToListAsync();
            
            //retornar os produtos
            return products;    
        }

//============================================================GetById=====================================================================//
       [HttpGet]
       [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetById([FromServices] DataContext context, int id)
        {
            // seleciona o ID do produto 
            // busca o id do produto, de acordo com a categoria
            var products = await context
                .Products
                .Include(x => x.Category) 
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .ToListAsync();
            
            return products;                     
        }

//============================================================POST=====================================================================//
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Post([FromBody] Product model, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {  
                return BadRequest(new {message = "Não foi possível criar a categoria"});
            }     
        }
//============================================================PUT=====================================================================//
//============================================================DELETE=====================================================================//
    
    
    
    
    
    }
}