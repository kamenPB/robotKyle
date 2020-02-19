using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Recipe : MonoBehaviour{

    private List<RecipeItem> recipeItemsList;

    /* Constructors */
    public Recipe(params RecipeItem[] recipes) {
       
        this.recipeItemsList = new List<RecipeItem>();

        foreach (var recipeItem in recipes) 
            this.recipeItemsList.Insert(0, recipeItem);
        
    }
    public Recipe(List<RecipeItem> items) {
        this.recipeItemsList = items;
    }


    /* Private Get Methods */
    private int GetFoodRecipeItemIndex(FoodType.TYPE food) {
        for (int i=0; i< recipeItemsList.Count; i++)
            if (recipeItemsList[i].IsFood(food))
                return i;
        
        return -1;
    }


    /* Public Set Methods */
    public void AddRecipeItem(RecipeItem item){
        recipeItemsList.Insert(0, item);
    }
    public bool RemoveRecipeItem(RecipeItem item){
        return recipeItemsList.Remove(item);
    }

    /* Public Get Methods */
    public RecipeItem GetRecipeItemFirst() {
        if (recipeItemsList.Count > 0)
            return recipeItemsList[0];
        else 
            throw new NoDataException();
    }

    /* Public Query Methods */
    public bool IsRecipeEmpty() {
        if (recipeItemsList.Count == 0) 
            return true;
        else 
            return false;     
    }

    public bool IsFoodInRecipe(FoodType.TYPE food) {
        foreach (var recipeItem in recipeItemsList) 
            if (recipeItem.IsFood(food)) return true; 
 
        return false;
    }


    public void ReduceRecipe(FoodType.TYPE food_type, float food_quantity_value){
        print("2.0 Reduce Recipe");
        if (IsFoodInRecipe(food_type)){
            print("2.1");
            int recipeItemIndex = GetFoodRecipeItemIndex(food_type);
            print("2.2");
            recipeItemsList[recipeItemIndex].ReduceFoodQuantity(food_quantity_value);
            
             if (recipeItemsList[recipeItemIndex].IsQuantityZero()) {
                print("2.3 - I'm 0.");
                recipeItemsList.RemoveAt(recipeItemIndex);
            }
        }

    }

}

