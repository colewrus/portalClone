using UnityEngine;
using System.Collections;

public class portalScript : MonoBehaviour {

	public Camera rayCamera;
	public GameObject portalIn;
	public GameObject portalOut;
	RaycastHit hit; 

	// Use this for initialization
	void Start () {
		portalIn.SetActive (false);
		portalOut.SetActive (false);			

	}
	
	// Update is called once per frame
	// I'm a comment

	void Update () {

		
		
		if (Input.GetMouseButtonDown (0)) {
			
			Vector3 shootRay = rayCamera.transform.TransformDirection (Vector3.forward) * 100;
			Debug.DrawRay (transform.position, shootRay, Color.cyan);

			Ray ray = new Ray (transform.position, shootRay); //player Ray

            print(ray.origin + ray.direction * 100);
            
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "portal") {
					portalIn.transform.position = hit.point + portalIn.transform.up;
					//portalIn.transform.rotation = hit.transform.rotation;
					portalIn.SetActive (true);
				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			
			Vector3 shootRay = rayCamera.transform.TransformDirection (Vector3.forward) * 100;
			Debug.DrawRay (transform.position, shootRay, Color.cyan);

			Ray ray = new Ray (transform.position, shootRay);

			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "portal") {
					portalOut.transform.position = hit.point + portalOut.transform.up;
					//portalOut.transform.rotation = hit.transform.rotation;
					portalOut.SetActive (true);
				}
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "portalIn" && portalOut.activeSelf) {
			transform.position = portalOut.transform.position + portalOut.transform.forward * 5;
		}
	}
}
