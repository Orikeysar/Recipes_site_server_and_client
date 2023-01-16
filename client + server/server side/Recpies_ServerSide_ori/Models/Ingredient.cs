

using Recpies_ServerSide_ori.Models.Dal;

namespace Recpies_ServerSide_ori.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Calories { get; set; }

        private static List<Ingredient> IngredientsList = new List<Ingredient>();

        //--------------------------------------------------------------------------------------------------
        // # GET ALL INGREDIENTS                           
        //--------------------------------------------------------------------------------------------------
        public static List<Ingredient> Read()
        {
            DBservices dbs = new DBservices();
            IngredientsList = dbs.getIngredientsFromDB();
            return IngredientsList;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT NEW INGREDIENT                            
        //--------------------------------------------------------------------------------------------------
        public static int InsertIngredient(Ingredient ingredient)
        {

            DBservices dbs = new DBservices();
            return dbs.InsertIngredientToDB(ingredient);
        }
    }
}
