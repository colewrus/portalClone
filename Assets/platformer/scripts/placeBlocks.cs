using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;


public class placeBlocks : MonoBehaviour {


    public float init_distance;
    public GameObject cube_331;
    public GameObject cube_512;
    public GameObject cube_214;
    public GameObject coinObj;

    public GameObject currentObj;
    public GameObject respawn;
    public FirstPersonController fpsScript;

    Vector3 mousePosition;
    Vector3 objectPos;
    Vector3 fineTune; //the player inputs from arrow keys to adjust the positioning of the object
    Vector3 lastRotation; //holds the rotation the last block was placed at, should be a QoL feature for faster block placement

    Ray ray;


    //Game Manager Variables
    public List<Vector3> rotations = new List<Vector3>(); //preset list of rotations for objects
    int rotationPos; //the current rotation preset
    public float timer;
    public float timeLimit;
    public List<GameObject> blocksPlaced = new List<GameObject>();

    //UI systems
    public Text text_blockCount;
    public Text text_fallenCount;
    public Text text_coins;
    public Text text_Timer;
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
        rotationPos = 0;
        init_distance = 5;
        timer = 0; 

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


        timerUpdate();
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

            if (Input.GetKeyDown(KeyCode.R))
            {
                if(rotationPos < rotations.Count-1)
                {
                    rotationPos++;
                }else
                {
                    rotationPos = 0;
                }
               
                Vector3 changeRot = rotations[rotationPos];
                currentObj.transform.rotation = Quaternion.Euler(changeRot);
            }

          
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                init_distance += 0.8f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if(init_distance > 4.0f)
                {
                   init_distance -= 0.8f;
                }else
                {
                    init_distance = 4;
                }
                
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
                blocksPlaced.Add(currentObj);
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
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                fpsScript.m_MouseLook.lockCursor = false;
                
            }else
            {
                panel_Menu.SetActive(false);
                fpsScript.m_MouseLook.lockCursor = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

           
        }       

	}


    void timerUpdate()
    {
        if (timer < timeLimit)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
 

        float minutes = timer / 60;

        float seconds = timer % 60;

        text_Timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        //text_Timer.text = text_Timer.text + "/" + string.Format("{0}:{1:00}", (timeLimit / 60), (timeLimit % 60));

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

        if (Input.GetKeyDown(KeyCode.F2))
        {
           for(int i = 0; i < blocksPlaced.Count; i++)
            {
                Destroy(blocksPlaced[i]);
            }
            blocksPlaced.Clear();
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
        fpsScript.m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


}
