using UnityEngine;
using SimpleJSON;

public class VognitionClient : MonoBehaviour
{
	string filePath;
	AudioSource aud;
	public KeyCode recordingKey = KeyCode.R;
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
		if(Input.GetKeyDown(recordingKey)){
			aud.clip = Microphone.Start(null, false, 4, 16000);
		}
		
		if(Input.GetKeyUp(recordingKey)){
			Microphone.End(null);
			SavWav.Save("myfile", aud.clip);
			VogSpeechTranslate();
		}
	}

	public void VogSpeechTranslate(){
		client.setNuanceAppID("Your app id here");
		client.setNuanceAppKey("Your app key here");
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