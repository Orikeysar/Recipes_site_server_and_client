using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Recpies_ServerSide_ori.Models.Dal
{
    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;


        public DBservices()
        {

        }


        //--------------------------------------------------------------------------------------------------
        //         **********CONNECTION AND SQL COMMAND ( FEET TO ALL, NOT NEED TO CHANGE  )*************
        //--------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        //                                 **********Ingredients*************
        //--------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------
        // # GET ALL INGREDIENTS                               
        //--------------------------------------------------------------------------------------------------
        public List<Ingredient> getIngredientsFromDB()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            List<Ingredient> list_ingredient = new List<Ingredient>();
            cmd = CreateCommandWithStoredProcedureGetAllIngredients("spGetAllIngredients", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Ingredient ing = new Ingredient();
                    ing.Id = Convert.ToInt32(dataReader["id"]);
                    ing.Name = dataReader["name"].ToString();
                    ing.Image = dataReader["image"].ToString();
                    ing.Calories = (float)Convert.ToDouble(dataReader["calories"]);
                    list_ingredient.Add(ing);
                }



            }
            catch (Exception ex) { throw (ex); }
            finally { con.Close(); }
            return list_ingredient;
        }

        // Create the SqlCommand using a stored procedure to Get All Flats
        private SqlCommand CreateCommandWithStoredProcedureGetAllIngredients(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure




            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT INGREDIENT                               
        //--------------------------------------------------------------------------------------------------

        // This method inserts a Flat to the Flats table 
        public int InsertIngredientToDB(Ingredient ingredient)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

            cmd = CreateCommandWithStoredProcedureInsertIngredient("spInsertIngredient", con, ingredient);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        // Create the SqlCommand using a stored procedure to Update Flat
        private SqlCommand CreateCommandWithStoredProcedureInsertIngredient(String spName, SqlConnection con, Ingredient ingredient)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure



            cmd.Parameters.AddWithValue("@name", ingredient.Name);

            cmd.Parameters.AddWithValue("@image", ingredient.Image);

            cmd.Parameters.AddWithValue("@calories", ingredient.Calories);




            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        //                                 **********Recipes*************
        //--------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------
        // # GET ALL RECIPES                               
        //--------------------------------------------------------------------------------------------------
        public List<Recipe> getRecipesFromDB()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            List<Recipe> list_recipe = new List<Recipe>();
            cmd = CreateCommandWithStoredProcedureGetAllRecipes("spGetAllRecipes", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Recipe rec = new Recipe();
                    rec.Id = Convert.ToInt32(dataReader["id"]);
                    rec.Name = dataReader["name"].ToString();
                    rec.Image = dataReader["image"].ToString();
                    rec.cookingMethod = dataReader["cookingMethod"].ToString();
                    rec.time = (float)Convert.ToDouble(dataReader["time"]);
                    list_recipe.Add(rec);
                }



            }
            catch (Exception ex) { throw (ex); }
            finally { con.Close(); }
            return list_recipe;
        }

        // Create the SqlCommand using a stored procedure to Get All Flats
        private SqlCommand CreateCommandWithStoredProcedureGetAllRecipes(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure




            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT RECIPE                               
        //--------------------------------------------------------------------------------------------------

        // This method inserts a Flat to the Flats table 
        public int InsertRecipeToDB(Recipe recipe)
        {

            SqlConnection con;
            SqlCommand cmd;
            int Id = 0;
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

            cmd = CreateCommandWithStoredProcedureInsertRecipe("spInsertRecipe", con, recipe);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                     Id = Convert.ToInt32(dataReader["SCOPE_IDENTITY"]);
                     
                }
               
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
            return Id;

        }
        // Create the SqlCommand using a stored procedure to Update Flat
        private SqlCommand CreateCommandWithStoredProcedureInsertRecipe(String spName, SqlConnection con, Recipe recipe)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure



            cmd.Parameters.AddWithValue("@name", recipe.Name);

            cmd.Parameters.AddWithValue("@image", recipe.Image);

            cmd.Parameters.AddWithValue("@cookingMethod", recipe.cookingMethod);

            cmd.Parameters.AddWithValue("@time", recipe.time);




            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // # GET INGREDIENTS FROM RECIPE                              
        //--------------------------------------------------------------------------------------------------
        public List<string> GetIngredientsListDB(int resId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            List<string> ingredientsList = new List<string>();


            cmd = CreateCommandWithStoredProcedureGetIngredientsList("spGetIngredientsInRecipe", con, resId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {

                    string id = dataReader["ingId"].ToString();

                    ingredientsList.Add(id);
                }



            }
            catch (Exception ex) { throw (ex); }
            finally { con.Close(); }
            return ingredientsList;
        }

        // Create the SqlCommand using a stored procedure to Get All Flats
        private SqlCommand CreateCommandWithStoredProcedureGetIngredientsList(String spName, SqlConnection con, int resId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@resId", resId);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT INGREDIENTS TO RECIPE                            
        //--------------------------------------------------------------------------------------------------

        // This method inserts a Flat to the Flats table 
        public int InsertIngredientsToRecipeDB(int ingId, int resId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

            cmd = CreateCommandWithStoredProcedureInsertIngredientsToRecipe("spInsertIngredientsToRecipe", con, ingId, resId);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        // Create the SqlCommand using a stored procedure to Update Flat
        private SqlCommand CreateCommandWithStoredProcedureInsertIngredientsToRecipe(String spName, SqlConnection con, int ingId, int resId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure



            cmd.Parameters.AddWithValue("@ingId", ingId);

            cmd.Parameters.AddWithValue("@resId", resId);






            return cmd;
        }


    }
}
