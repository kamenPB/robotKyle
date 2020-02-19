using UnityEngine;

public class BinScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider other) {
        var food_script = other.GetComponent<Food>();

        if (food_script != null)
            Destroy(other.gameObject);

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
