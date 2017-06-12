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
    Vector3 lastRotation; //holds the rotation the last block was placed at, should be a QoL feature for faster block placement

    Ray ray;



    //UI systems
    public Text text_blockCount;
    public Text text_fallenCount;
    public Text text_coins;
    int blockCount;
    int fallenCount;
    int coins;
    public GameObject panel_Menu;
    public GameObject image_infoBubble;
    public GameObject panel_inventory;
	public GameObject panel_Instructions;

   // Use this for initialization
   void Start () {
        fineTune = Vector3.zero;

        panel_Menu.SetActive(false);
        image_infoBubble.SetActive(false);
		panel_Instructions.SetActive (false);

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
        Vector3 rayDirection = Camera.main.transform.TransformDirection(Vector3.forward) * init_distance;
        Ray ray = new Ray(Camera.main.transform.position, rayDirection);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward)*init_distance, Color.green);
       // ray = new Ray(transform.position + Vector3.up, rayDirection);
        

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
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Vector3 changeRot = currentObj.transform.rotation.eulerAngles;
                changeRot.z += 5;
                currentObj.transform.rotation = Quaternion.Euler(changeRot);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Vector3 changeRot = currentObj.transform.rotation.eulerAngles;
                changeRot.z -= 5;
                currentObj.transform.rotation = Quaternion.Euler(changeRot);
            }
        }



        number_Inputs();


        if (Input.GetMouseButton(0))
        {
            if (currentObj != null)
            {
                if(currentObj.tag == "coin")
                {
                  
                    platformer_GM.instance.obj_coinList.Add(currentObj);
                    
                }
                lastRotation = currentObj.transform.eulerAngles;
              
                blockCount++;
                setUI();
                currentObj = null;

            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel_Menu.active)
            {               
                panel_Menu.SetActive(true);
            }else
            {
                panel_Menu.SetActive(false);
            }

           
        }       

	}



       void number_Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            image_infoBubble.SetActive(true);
            image_infoBubble.transform.position = panel_inventory.transform.GetChild(0).transform.position + new Vector3(0, 60, 0);
            //move the bubble that tells you what you selected
            image_infoBubble.transform.GetChild(0).GetComponent<Text>().text = "3x3x1 Platform"; //and set the text

            if (currentObj != null)
                Destroy(currentObj);

            objectPos = ray.origin + ray.direction * init_distance;         //updating position of the object to the end of the ray;
            currentObj = GameObject.Instantiate(cube_331, objectPos, Quaternion.Euler(lastRotation)) as GameObject;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            image_infoBubble.SetActive(true);
            image_infoBubble.transform.position = panel_inventory.transform.GetChild(1).transform.position + new Vector3(0, 60, 0);
            //move the bubble that tells you what you selected
            image_infoBubble.transform.GetChild(0).GetComponent<Text>().text = "5x1x2 Platform"; //and set the text

            if (currentObj != null)
                Destroy(currentObj);


            objectPos = ray.origin + ray.direction * init_distance;
            currentObj = GameObject.Instantiate(cube_512, objectPos, Quaternion.Euler(lastRotation)) as GameObject;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            image_infoBubble.SetActive(true);
            image_infoBubble.transform.position = panel_inventory.transform.GetChild(2).transform.position + new Vector3(0, 60, 0);
            //move the bubble that tells you what you selected
            image_infoBubble.transform.GetChild(0).GetComponent<Text>().text = "2x1x4 Platform";

            if (currentObj != null)
                Destroy(currentObj);

            objectPos = ray.origin + ray.direction * init_distance;
            currentObj = GameObject.Instantiate(cube_214, objectPos, Quaternion.Euler(lastRotation)) as GameObject;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            image_infoBubble.SetActive(true);
            image_infoBubble.transform.position = panel_inventory.transform.GetChild(3).transform.position + new Vector3(0, 60, 0);
            //move the bubble that tells you what you selected
            image_infoBubble.transform.GetChild(0).GetComponent<Text>().text = "Gold Coin";

            if (currentObj != null)
                Destroy(currentObj);

            objectPos = ray.origin + ray.direction * init_distance;
            currentObj = GameObject.Instantiate(coinObj, objectPos, Quaternion.identity) as GameObject;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            image_infoBubble.SetActive(true);
            image_infoBubble.transform.position = panel_inventory.transform.GetChild(4).transform.position + new Vector3(0, 60, 0);
            //move the bubble that tells you what you selected
            image_infoBubble.transform.GetChild(0).GetComponent<Text>().text = "Respawn Coins";

            for (int i = 0; i < platformer_GM.instance.obj_coinList.Count; i++)
            {
                if (!platformer_GM.instance.obj_coinList[i].active)
                    platformer_GM.instance.obj_coinList[i].SetActive(true);
            }
        }

		if (Input.GetKeyDown (KeyCode.F1)) {
			if (panel_Instructions.activeSelf) {
				panel_Instructions.SetActive (false);
			} else {
				panel_Instructions.SetActive (true);
			}
		}
    }


    void OnTriggerEnter(Collider other)
    {
     
        if(other.gameObject.tag == "coin")
        {
            coins++;
            setUI();
            //play sound
            //run animation - this might just need the animation trigger at the end to destroy itself
            other.gameObject.SetActive(false);
        }
    }

    //GUI Button Functions
    public void exitGame()
    {
        Application.Quit();
    }

    public void resumeGame()
    {
        Debug.Log("resume");
        panel_Menu.SetActive(false);
    }


}
