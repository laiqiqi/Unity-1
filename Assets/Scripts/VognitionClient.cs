using UnityEngine;
using SimpleJSON;

public class VognitionClient : MonoBehaviour
{
	string filePath;
	AudioSource aud;
	Vognition client = new Vognition();

	public void Start()
	{
		filePath = Application.dataPath + "/myfile.wav";
		aud = GetComponent<AudioSource>();
		foreach (string device in Microphone.devices) {
			print ("Name: " + device);
		}
		aud = GetComponent<AudioSource>();
	}

	void Update () {
		if(Input.GetKeyDown("r")){
			aud.clip = Microphone.Start(null, false, 4, 16000);
		}
		
		if(Input.GetKeyUp("r")){
			Microphone.End(null);
			SavWav.Save("myfile", aud.clip);
			VogSpeechTranslate();
		}
	}

	public void VogSpeechTranslate(){
		// Don't forget to visit Nuance.com to get your appId and app Key! 
		client.setNuanceAppID("");
		client.setNuanceAppKey("");
		string command = client.dictation(filePath);
		print (command);
		string transtext = client.transText(command);
		print(transtext);

		// Parse the response from transtext, extract the "ttsResponse" only if it is valid json
		string ttsResponse = "";
		var Json = JSON.Parse(transtext);
		if (Json != null) {
			ttsResponse = Json["ttsResponse"].Value;
			print(ttsResponse);
		}
		Debug.Log (command);
		switch(ttsResponse)
		{
			case "successfully turned off the light":
				GameObject.FindGameObjectWithTag ("IndoorLight").light.enabled = false;
				break;

			case "successfully turned on the light":
				GameObject.FindGameObjectWithTag("IndoorLight").light.enabled = true;
				break;

			default:
				print ("Unrecognized command");
				break;
		}
	}
}