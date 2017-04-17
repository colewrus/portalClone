using UnityEngine;
using System.Collections;

public class portalScript : MonoBehaviour {
	public Camera playerCam;
	public GameObject portalIn;
	public GameObject portalOut;
	RaycastHit hit; 

	// Use this for initialization
	void Start () {
		portalIn.SetActive (false);
		portalOut.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 myVector = playerCam.transform.TransformDirection (Vector3.forward) * 100; 
		Debug.DrawRay (transform.position, myVector, Color.cyan);

		Ray myRay = new Ray (transform.position, myVector);
		/*
		if (Physics.Raycast (myRay, out hit)) {
			print (hit.transform.name);
			Debug.Log (hit.transform.position.x);
		}
		*/

		if (Input.GetMouseButtonDown (0)) {
			
			if (Physics.Raycast (myRay, out hit)) {
				portalIn.transform.position = hit.point + portalIn.transform.up;
				portalIn.SetActive (true);
				//comment
			}		


		}
		if (Input.GetMouseButtonDown (1)) {
			
			if (Physics.Raycast (myRay, out hit)) {
				portalOut.transform.position = hit.point + portalIn.transform.up;
				portalOut.SetActive (true);
			}	
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "portalIn" && portalOut.activeSelf) {
			transform.position = portalOut.transform.position;
		}
	}


}
