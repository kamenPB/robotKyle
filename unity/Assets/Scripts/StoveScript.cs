using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveScript : MonoBehaviour
{

    private HashSet<Food> foods_on_stove = new HashSet<Food>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foods_on_stove.Count > 0) { 
            foreach (var food_script in foods_on_stove) {
                food_script.IncrementCookingTime(Time.deltaTime);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var food_script = other.GetComponent<Food>();

        if (food_script != null)
        {
            //exists.
            foods_on_stove.Add(other.gameObject.GetComponent<Food>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        var food_script = other.GetComponent<Food>();

        if (food_script != null)
        {
            foods_on_stove.Remove(other.gameObject.GetComponent<Food>());
        }
    }



}
