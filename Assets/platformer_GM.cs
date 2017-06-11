using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class platformer_GM : MonoBehaviour {

    public static platformer_GM instance = null;

   
    public List<GameObject> obj_coinList = new List<GameObject>();


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
       
        obj_coinList.Clear();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
