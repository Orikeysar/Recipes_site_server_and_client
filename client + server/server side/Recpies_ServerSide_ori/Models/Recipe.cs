namespace Recpies_ServerSide_ori.Models
{
    using Recpies_ServerSide_ori.Models.Dal;
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string cookingMethod { get; set; }
        public float time { get; set; }

        private static List<Recipe> RecipesList = new List<Recipe>();

        //--------------------------------------------------------------------------------------------------
        // # GET ALL INGREDIENTS                           
        //--------------------------------------------------------------------------------------------------
        public static List<Recipe> Read()
        {
            DBservices dbs = new DBservices();
            RecipesList = dbs.getRecipesFromDB();
            return RecipesList;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT NEW Recpie                            
        //--------------------------------------------------------------------------------------------------
        public static int InsertRecipe(Recipe recipe)
        {

            DBservices dbs = new DBservices();
            return dbs.InsertRecipeToDB(recipe);
        }

        //--------------------------------------------------------------------------------------------------
        // # GET  INGREDIENTS FROM RECIPE                        
        //--------------------------------------------------------------------------------------------------
        public static List<string> GetIngredientsList(int resId)
        {
            DBservices dbs = new DBservices();

            List<string> ingredientList = dbs.GetIngredientsListDB(resId);
            return ingredientList;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT INGREDIENTS TO RECIPE                            
        //--------------------------------------------------------------------------------------------------
        public static int InsertIngredientsToRecipe(int ingId, int resId)
        {

            DBservices dbs = new DBservices();
            return dbs.InsertIngredientsToRecipeDB(ingId, resId);
        }
    }
}
