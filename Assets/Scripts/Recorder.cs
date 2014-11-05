using UnityEngine;
using System.Collections;

public class Recorder : MonoBehaviour {

	AudioSource aud;
	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
		foreach (string device in Microphone.devices) {
			print ("Name: " + device);
		}
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("r")){
			aud.clip = Microphone.Start(null, false, 4, 16000);
		}

		if(Input.GetKeyUp("r")){
			Microphone.End(null);
			SavWav.Save("myfile", aud.clip);
		}

		if(Input.GetKeyDown("p")){
			aud.Play();
		}
	}
}