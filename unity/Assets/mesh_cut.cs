using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh_cut : MonoBehaviour
{
    public GameObject target;
    private bool cut = false;

    
    // Start is called before the first frame update
    void Start()
    {
        print("Started script");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float distance = Vector3.Distance(target.gameObject.transform.position, gameObject.transform.position);
        Vector3 scale = (target.gameObject.transform.localScale + gameObject.transform.localScale)/2;

        if ((distance <= scale.z) && cut == false) {
            print("COLLISION OKAY!");
            Vector3 rightPoint = target.gameObject.transform.position + Vector3.right * target.gameObject.transform.localScale.x/2;


            Material mat = target.gameObject.GetComponent<MeshRenderer>().material;
            GameObject rightSideObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            rightSideObj.transform.position = (rightPoint + gameObject.transform.position) /2;
            float rightWidth = Vector3.Distance(gameObject.transform.position,rightPoint);
            rightSideObj.transform.localScale = new Vector3( rightWidth ,target.gameObject.transform.localScale.y ,target.gameObject.transform.localScale.z );
            rightSideObj.AddComponent<Rigidbody>().mass = 100f;
            rightSideObj.GetComponent<MeshRenderer>().material = mat;
            cut = true;
        }

        */
    }


    private void SplitCube(GameObject cube, Vector3 hitPoint){
    
    float ratio = 0.4f; //Mathf.Abs(cube.transform.position.x - hitPoint.x) / cube.transform.localScale.x;  
    print("Ratio: " + ratio);
    Material mat = cube.GetComponent<MeshRenderer>().material;

    GameObject cube_l = GameObject.CreatePrimitive(PrimitiveType.Cube);
    GameObject cube_r = GameObject.CreatePrimitive(PrimitiveType.Cube);

    cube_l.AddComponent<Rigidbody>().mass = 1f;
    cube_l.GetComponent<MeshRenderer>().material = mat;

    cube_r.AddComponent<Rigidbody>().mass = 1f;
    cube_r.GetComponent<MeshRenderer>().material = mat;

    cube_l.transform.localScale = new Vector3(ratio * cube.transform.localScale.x, cube.transform.localScale.y, cube.transform.localScale.z);
    cube_l.transform.position = new Vector3((cube.transform.position.x + cube_l.transform.localScale.x/2), cube.transform.position.y, cube.transform.position.z);

    cube_r.transform.localScale = new Vector3((1-ratio) * cube.transform.localScale.x, cube.transform.localScale.y, cube.transform.localScale.z);
    cube_r.transform.position = new Vector3(cube.transform.position.x - (cube_r.transform.localScale.x/2), cube.transform.position.y, cube.transform.position.z);

    }

    private void OnTriggerEnter(Collider other) {
        Vector3 hitPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        if (cut == false && other.transform.localScale.x > 0.005){
            SplitCube(other.gameObject, hitPoint);
            Destroy(other.gameObject);
            cut = true;
        }       

    }

    private void OnTriggerExit(Collider other) {
        cut = false;
    }

    


}
