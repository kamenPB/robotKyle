using UnityEngine;

public class RecipeItem : MonoBehaviour {

    private FoodType.TYPE food_type;
    private FoodQuantity.TYPE food_quantity;
    private float food_quantity_value;
    
    /* Constructor */
    public RecipeItem(FoodType.TYPE food_type, FoodQuantity.TYPE food_quantity, float food_quantity_value){
        this.food_type = food_type;
        this.food_quantity = food_quantity;
        this.food_quantity_value = food_quantity_value;
    }


    /* Public Methods */
    public void ReduceFoodQuantity(float quantity_val) {
        print("3.0 Reduce Food Quantity.");
        if (quantity_val > 0) {
            print("3.1");
            if (quantity_val <= food_quantity_value)
            {
                print("3.1.1");
                food_quantity_value -= quantity_val;
            }
                
            else //TODO: Provided quantity is too high!.
                food_quantity_value = 0f;
        }
    }
    /* Public Query Methods */
    public bool IsFood(FoodType.TYPE food) {
        if (food == food_type)
            return true;
        else 
            return false;
    }
    public bool IsQuantityZero() {
        if (food_quantity_value == 0f)
            return true;
        else 
            return false;
    }
}
