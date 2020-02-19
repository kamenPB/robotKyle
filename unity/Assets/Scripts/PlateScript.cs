using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField]
    private bool HasRecipe = false;
    private int wtf = -1;

    public Recipe plateRecipe; //This is the only recipe this plate takes!!

    GameObject player;

    public bool IsPlateFree() {
        return !HasRecipe;
    }

    void Start() {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRecipe(Recipe recipe){
        plateRecipe = recipe;
        HasRecipe = true;
    }

    private void IncreaseScore(int pts) {
        player.GetComponent<PlayerScore>().IncreaseScore(pts);
        DatabaseHandler.Test_IncreaseScore(pts);
    }
    private void DecreaseScore(int pts) {
        player.GetComponent<PlayerScore>().IncreaseScore(-pts);
    }

    private void OnTriggerEnter(Collider other) {
        var food_script = other.GetComponent<Food>();

        if (food_script != null) {
            if (plateRecipe.IsFoodInRecipe(food_script.GetFoodType())) {
                plateRecipe.ReduceRecipe(food_script.GetFoodType(), food_script.GetFoodQuantityValue());
                //Check Food Type -> see if it matches what's on the recipe.
                switch (food_script.GetFoodStatus()) {
                        case FoodStatus.State.PERFECT:
                            IncreaseScore(25);
                            break;
                        case FoodStatus.State.GOOD:
                            IncreaseScore(10);
                            break;
                        case FoodStatus.State.BURNT:
                            DecreaseScore(5);
                            //TODO: Display X
                            print("FOOD IS BURNT!");
                            return;
                        default:
                            //TODO: FOOD IS UNCOOKED. -> Do nothing???
                            return;
                }
                
                
                //Process Recipe Reduction.


                //Destroy food.
                Destroy(other.gameObject);
            


                if (plateRecipe.IsRecipeEmpty()) 
                    HasRecipe = false;
                    
    

            }
        }
    }
}
