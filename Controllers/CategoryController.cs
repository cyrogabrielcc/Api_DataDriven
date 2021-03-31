using Microsoft.AspNetCore.Mvc;

//https://localhost:5001

namespace DataDriven.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [Route("")]
        public string Function()
        {
            return "Olá mundo";
        }
    }
}