using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRecipeList : MonoBehaviour {

    [SerializeField]
    private int noOfRecipes = 0;

    public GameObject Player1PlateSet;
    public GameObject Player2PlateSet;

    private bool RecipeListEmpty = true;

    /*Debug Only */

    [SerializeField]
    private int RecipeCount = -1;
    [SerializeField]
    private int PlateCount = -1;

    private List<Recipe> recipeList = new List<Recipe>();
    private HashSet<PlateScript> plateList = new HashSet<PlateScript>();

    private List<Recipe> completedRecipes = new List<Recipe>();

    [SerializeField]
    private bool HasPhotonNetworkIDLoaded = false;

    [SerializeField]
    private int PhotonPlayerID = -1;
    
    // Start is called before the first frame update
    void Start() {

        Recipe recipe_1 = new Recipe(new RecipeItem(FoodType.TYPE.EGG, FoodQuantity.TYPE.QUANTITY, 3f));
        Recipe recipe_2 = new Recipe(new RecipeItem(FoodType.TYPE.EGG, FoodQuantity.TYPE.QUANTITY, 1f));

        recipeList.Add(recipe_1);
        recipeList.Add(recipe_2);

        RecipeListEmpty = false;

        print("RECIPE COUNT: " + recipeList.Count);
        print("PLATE COUNT: " + plateList.Count);
    }

    // Update is called once per frame
    void Update() {
        RecipeCount = recipeList.Count;
        PlateCount = plateList.Count;
        PhotonPlayerID = PhotonNetwork.player.ID;
        if ((!HasPhotonNetworkIDLoaded) && (PhotonNetwork.player.ID != -1)) {
            HasPhotonNetworkIDLoaded = true;
            
            if (PhotonNetwork.player.ID == 1)
                foreach (var plate in Player1PlateSet.GetComponentsInChildren<PlateScript>())
                    plateList.Add(plate);
            else if (PhotonNetwork.player.ID == 2)
                foreach (var plate in Player2PlateSet.GetComponentsInChildren<PlateScript>())
                    plateList.Add(plate);
        }

        if (!RecipeListEmpty){
            //TODO: ADD LATER FOR EFFICIENCY!! --->  if (!PlatesFull)   
            foreach (var plate in plateList) {
                if (plate.IsPlateFree() && (!RecipeListEmpty)){
                    Recipe recipe = GetRecipe();
                    GiveRecipeToPlate(recipe, plate);
                }
            }
        } else {
            //No more recipes. -> Check every plate is empty too.
      
            foreach (var plate in plateList) {
                if (!plate.IsPlateFree()) 
                    return;           
            }
            
            Debug.Log("Beautiful ->>> Winner!");
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }
    }

    private void GiveRecipeToPlate(Recipe recipe, PlateScript plate) {
        print("CALLED: GiveRecipeToPlate");

        //Set Recipe to Plate.
        plate.SetRecipe(recipe);

        recipeList.Remove(recipe);
        completedRecipes.Add(recipe);

        if (recipeList.Count == 0) RecipeListEmpty = true; //Efficiency check.

    }

    public Recipe GetRecipe() {
        if (recipeList.Count > 0) {
            return recipeList[0];
        }
        else throw new NoDataException();
    }

}
