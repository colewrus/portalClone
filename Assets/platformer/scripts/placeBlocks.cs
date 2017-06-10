using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class placeBlocks : MonoBehaviour {


    public float init_distance;
    public GameObject cube_331;
    public GameObject cube_512;
    public GameObject cube_214;
    public GameObject coinObj;

    public GameObject currentObj;
    public GameObject respawn;

    Vector3 mousePosition;
    Vector3 objectPos;
    Vector3 fineTune; //the player inputs from arrow keys to adjust the positioning of the object

    Ray ray;


    //UI systems
    public Text text_blockCount;
    public Text text_fallenCount;
    public Text text_coins;
    int blockCount;
    int fallenCount;
    int coins;


   // Use this for initialization
   void Start () {
        fineTune = Vector3.zero;

        blockCount = 0;
        fallenCount = 0;
        coins = 0;

        setUI();

	}

    void setUI()
    {

        text_blockCount.text = "Blocks Placed \n" + blockCount;
        text_fallenCount.text = "Times Fallen \n" + fallenCount;
        text_coins.text = "Coins \n" + coins;
    }
	
	// Update is called once per frame
	void Update () {
        mousePosition = Input.mousePosition;
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward) * init_distance;
        Ray ray = new Ray(transform.position + Vector3.up, rayDirection);
        Debug.DrawRay(transform.position, rayDirection, Color.green);
        ray = new Ray(transform.position + Vector3.up, rayDirection);
        

        if (Input.GetKey(KeyCode.Tab))
        {
            if(transform.position.y < -10)
            {
                fallenCount++;
                setUI();
            }
           
            this.transform.position = respawn.transform.position;
        }

        if(currentObj != null)
        {
            currentObj.transform.position = ray.origin + fineTune + ray.direction * init_distance;

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                fineTune.z += 1;
            }   
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                fineTune.z -= 1;
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                fineTune.x += 1;
            }
            if (Input.GetKeyDown(KeyCode.End))
            {
                fineTune.x -= 1;
            }
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                fineTune.y += 1;
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                fineTune.y -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {           
            objectPos = ray.origin + ray.direction * init_distance;         
            currentObj = GameObject.Instantiate(cube_331, objectPos, Quaternion.identity) as GameObject;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            objectPos = ray.origin + ray.direction * init_distance;
            currentObj = GameObject.Instantiate(cube_512, objectPos, Quaternion.identity) as GameObject;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            objectPos = ray.origin + ray.direction * init_distance;
            currentObj = GameObject.Instantiate(cube_214, objectPos, Quaternion.identity) as GameObject;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            objectPos = ray.origin + ray.direction * init_distance;
            currentObj = GameObject.Instantiate(coinObj, objectPos, Quaternion.identity) as GameObject;
        }

        if (Input.GetMouseButton(0))
        {
            if (currentObj != null)
            {
                blockCount++;
                setUI();
                currentObj = null;
            }
            
        }

        

	}

    void Repeater()
    {
        Debug.Log(mousePosition);
    }

    void OnTriggerEnter(Collider other)
    {
     
        if(other.gameObject.tag == "coin")
        {
            coins++;
            setUI();
            //play sound
            //run animation - this might just need the animation trigger at the end to destroy itself
            Destroy(other.gameObject);
        }
    }

}
