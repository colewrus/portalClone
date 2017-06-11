using UnityEngine;
using System.Collections;

public class animationEvents : MonoBehaviour {

    public int cycleCount_info;

	// Use this for initialization
	void Start () {
        cycleCount_info = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void infoBubble_idle()
    {
        this.gameObject.SetActive(false);
        
    }
}
