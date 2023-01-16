using Microsoft.AspNetCore.Mvc;
using Recpies_ServerSide_ori.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recpies_ServerSide_ori.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        //--------------------------------------------------------------------------------------------------
        // # GET ALL INGREDIENTS                                
        //--------------------------------------------------------------------------------------------------
        // GET: api/<FlatController>
        [HttpGet]
        public List<Recipe> Get()
        {
            return Recipe.Read();
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT RECPIE                              
        //--------------------------------------------------------------------------------------------------

        // POST api/<FlatController>
        [HttpPost]
        public int Post(Recipe recipe)
        {
            int temp = Recipe.InsertRecipe(recipe);
            return temp;

        }

        //--------------------------------------------------------------------------------------------------
        // # GET INGREDIENTS FROM RECIPE                                
        //--------------------------------------------------------------------------------------------------
        // GET: 
        [HttpGet("resId/{resId}")]
        public List<string> GetIngredientsFromRecipe(int resId)
        {

            return Recipe.GetIngredientsList(resId);
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT INGREDIENTS TO RECIPE                              
        //--------------------------------------------------------------------------------------------------

        // POST 
        [HttpPost("ingId/{ingId}/resId/{resId}")]
        public bool InsertToRecipe(int ingId, int resId)
        {

            int temp = Recipe.InsertIngredientsToRecipe(ingId, resId);
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
