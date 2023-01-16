using Microsoft.AspNetCore.Mvc;
using Recpies_ServerSide_ori.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recpies_ServerSide_ori.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        // GET: api/<IngredientController>
        [HttpGet]
        public List<Ingredient> Get()
        {
            return Ingredient.Read();
        }

        // POST api/<IngredientController>
        [HttpPost]
        public bool Post([FromBody] Ingredient ingredient)
        {
            int temp = Ingredient.InsertIngredient(ingredient);
            if (temp > 0)
            {
                return true;
            }
            else

            {
                return false;
            }

        }
    }
  
    
}
