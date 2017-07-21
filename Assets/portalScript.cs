using UnityEngine;
using System.Collections;


// !!!---- I'm a comment, any text followed by two // do not affect code

//A class is a set of instructions that can be attached to an object
public class portalScript : MonoBehaviour {

    //Below the class we can declare variables, declare variables in C# like this - visibility - variable type - variable name
    //if we declare visibility "public" then we can see the variable and assign it in the Unity editor 
	public Camera rayCamera;     //This is the player's camera, we'll shoot a ray out of it based on where the player is looking

    public GameObject portalIn;     //the object that we will use for our "IN" portal, we will create/move it when player left clicks
	public GameObject portalOut;    //the object that we will use for our "IN" portal, we will create/move it when player left clicks
    
    //Variables are not visible in the Unity editor unless they are specifically declared "Public"
    RaycastHit hit;     //If a ray hits an object a RaycastHit can give us information about that object

	// Use this for initialization
	void Start () {
		portalIn.SetActive (false);     //we set this variable (a GameObject) to hide using .SetActive(false), we can show it again by using .SetActive(true)
		portalOut.SetActive (false);			
	}
	
	// Update is called once per frame
	void Update ()   { // computers typically run between 20-60+ frames per second. Everything in the update happens A LOT! It's useful for "listening" to commands


        if (Input.GetMouseButtonDown (0)) {     //This listens for us to press the left mouse button
			
			Vector3 shootRay = rayCamera.transform.TransformDirection (Vector3.forward) * 100;      //A vector is direction and magnitude! we use this to show movement in space. Basically we are drawing a line between our camera and a spot 100 units in front of the camera
			Debug.DrawRay (transform.position, shootRay, Color.cyan);   //Let's spice it up, this will draw a line that is cyan

			Ray ray = new Ray (transform.position, shootRay); //player Ray, because we drew a ray up above but it's just a drawing, this is the actual ray that will give us data about what is in front of us

           // print(ray.origin + " " + ray.direction * 100);    //This will display a message in the console, we give it two variables to show where our ray starts and where our ray ends
            
			if (Physics.Raycast (ray, out hit)) {       //Now we listen to see what kind of data we get back from the ray we just drew

                if (hit.transform.tag == "portal") {    //Now that we have the attention of Physics let's see if our "hit" collides with a "transform" (the data about an object's position, rotation, size) that has a "tag" called "portal"
                    portalIn.transform.position = hit.point;  //Hey we hit something that can hold a portal, let's put our portal at that spot we hit, 

                                 
                    
                    // Below deals with some complex math for rotation, Unity gives us tools to solve the math for us. It's hard to describe what is happening in a comment so I'll just say what it does
                    portalIn.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);  //Let's align the object with the face of object we are hitting
           
                    portalIn.transform.position += (portalIn.transform.forward / 10);  //Personal space is important, we're going to move just a litte bit away form the object the portalIn spawns on

                    portalIn.SetActive (true);  //and we make the portalIn active
				}
			}
		}

        //This is a copy of the previous code, just substituting a few new values and variables
		if (Input.GetMouseButtonDown (1)) {     //Right click on the mouse is indicated by the number 1 
			
			Vector3 shootRay = rayCamera.transform.TransformDirection (Vector3.forward) * 100;
			Debug.DrawRay (transform.position, shootRay, Color.cyan);

			Ray ray = new Ray (transform.position, shootRay);

			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "portal") {
              

					portalOut.transform.position = hit.point; //we are setting the position of the portalOut here

                    // Below deals with some complex math for rotation, Unity gives us tools to solve the math for us. It's hard to describe what is happening in a comment so I'll just say what it does. See above comments
                    portalOut.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);             
                    portalOut.transform.position += (portalOut.transform.forward/10);
                    portalOut.SetActive (true);
				}
			}
		}
	}


    //WHOA! New function! Unity has built in functions that can help us out. This one listens for the object to collide with a Trigger
    //We use the Collider col variable to store the data of the Trigger the we assigned in the Unity Editor
	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "portalIn" && portalOut.activeSelf) {    //if the object we collided with is a gameObject with the name "portalIn" and the out portal is active then we do something below
			transform.position = portalOut.transform.position + portalOut.transform.forward*2;    //we are going to make our position (stored in the transform, remember?) the out portal's position and a little bit forward to simulate our momentum walking through the portal            
		}
	}
}
