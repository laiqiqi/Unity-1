using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("o")){
			if (this.light.enabled == false){
				this.light.enabled = true;
			}
			else{
				this.light.enabled = false;
			}
		}
	}
}
