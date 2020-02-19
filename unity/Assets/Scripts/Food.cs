using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IFood
{
    //time until this food is fully cooked.
    public float time_until_cooked = 3f;

    //how long it's actually been cooked.
    private float cooking_time = 0f;


    //private properties. [ only once ] 

    public FoodType.TYPE Type = FoodType.TYPE.EGG;
    private FoodStatus.State State = FoodStatus.State.UNCOOKED;
    private FoodQuantity.TYPE Quantity = FoodQuantity.TYPE.QUANTITY;
    private float QuantityValue = 1f;


    public void IncrementCookingTime(float time) {
        cooking_time += time;
        //Check if within region

        float diff = cooking_time / time_until_cooked;
        if (diff > 1.2)
        {
            //burnt
            GetComponent<Renderer>().material.color = Color.black;
            State = FoodStatus.State.BURNT;
        } else if (diff > 1.1)
        {
            //overcooked
            GetComponent<Renderer>().material.color = Color.grey;
            State = FoodStatus.State.BURNT;
        } else if (diff > 0.9)
        {
            //perfect
            GetComponent<Renderer>().material.color = Color.green;
            State = FoodStatus.State.PERFECT;

        } else if (diff > 0.8)
        {
            //slightly undercooked
            GetComponent<Renderer>().material.color = Color.blue;
            State = FoodStatus.State.GOOD;
        } else
        {
            //uncooked.
        }
    }

    public FoodStatus.State GetFoodStatus() {
        return State;
    }

    public FoodType.TYPE GetFoodType() {
        return Type;
    }
    public FoodQuantity.TYPE GetFoodQuantity() {
        return Quantity;
    }

    public float GetFoodQuantityValue() {
        return QuantityValue;
    }

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
    }
}
