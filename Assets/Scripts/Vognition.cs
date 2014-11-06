using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
{
	public TrustAllCertificatePolicy() {}
	public bool CheckValidationResult(ServicePoint sp, 
	                                  X509Certificate cert,
	                                  WebRequest req, 
	                                  int problem)
	{
		return true;
	}
}

public class Vognition : MonoBehaviour
{
	string baseURL = "http://sample.whataremindsfor.com:46900/";
	string filePath;
	AudioSource aud;

	public void Start()
	{
		filePath = Application.dataPath + "/myfile.wav";
		//filePath = @"C:\Users\Richard\Desktop\lights.wav";

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
		string command = vogDictation(filePath);
		string transtext = vogTransText(command);
		print(transtext);

		if (transtext.Contains("successfully turned off the light")) {
			GameObject.FindGameObjectWithTag("IndoorLight").light.enabled = false;
		}

		if (transtext.Contains("successfully turned on the light")) {
			GameObject.FindGameObjectWithTag("IndoorLight").light.enabled = true;
		}
	}

	public string vogTransText(string sentence)
	{
		// Strip any special characters from the translated message
		sentence = Regex.Replace(sentence, @"[^0-9a-zA-Z ]+", "");
		
		NameValueCollection data = new NameValueCollection();
		//  **CAPSTONE KEYS**
		data["appkey"] = "59cddd076cf2a00b9f5555585c177276d8a49873";
		data["appsecret"] = "01d3cb9455722c2f6760e11f14a0571179649873";
		data["conkey"] = "09326eefff2cad2ae9f4c0fd5d4c7b71db249873";
		data["consecret"] = "7092379b31cd621c2c1b574e47ec956733f49873";
		
		//  **VOGNITION HACKATHON KEYS
		//data["appkey"] = "59c000476cf2a00b9f5bc1585c177276d8a4d75f";
		//data["appsecret"] = "01d3cb9455722c2f6760e11f14a05711796abc1a";
		//data["conkey"] = "09326ebc1e2cad2ae9f4c0fd5d4c7b71db2b0e48";
		//data["consecret"] = "7092379b31cd621c2c1b574e47ec956733fabc1a";
		
		data["sentence"] = sentence;
		data["ttsSpeakerType"] = "usenglishfemale";
		data["locale"] = "en-US";
		data["requestType"] = "ASR";
		data["HPDL_ID"] = "TEST_HPM_ID";

		string transTextURL = baseURL + "apiv1/transtext";
		string retVal = "_NULL";

		// Post the data to the transText URL
		using (var wb = new WebClient())
		{
			try
			{
				var response = wb.UploadValues(transTextURL, "POST", data);
				retVal = Encoding.UTF8.GetString(response, 0, response.Length);
			}
			catch (WebException e)
			{
				int statusCode = (int)((HttpWebResponse)e.Response).StatusCode;
				switch (statusCode)
				{
				case 400:
					retVal = "Response code: " + statusCode + "\tBad request";
					break;
					
				case 401:
					retVal = "Response code: " + statusCode + "\tUnauthorized authentication";
					break;
					
				case 403:
					retVal = "Response code: " + statusCode + "\tResource is not availible";
					break;
					
				case 409:
					retVal = "Response code: " + statusCode + "\tMissing Parameters";
					break;
					
				case 501:
					retVal = "Response code: " + statusCode + "\tFeature not yet implemented";
					break;
					
				case 503:
					retVal = "Response code: " + statusCode + "\tThere is an issue on Vognition";
					break;
					
				case 504:
					retVal = "Response code: " + statusCode + "\tComponent Time Out";
					break;
					
				default:
					return ((HttpWebResponse)e.Response).StatusDescription;
				}
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}
		return retVal;
	}

	public string vogDictation (string filePath)
	{
		string appId = "NMDPTRIAL_le_richard206520141105025111";
		string appKey = "db3253ad1bdd222d25517ef496be850cf7c8d74044b6d750c09b7f4963f7593e2aaceb458a0e72af5ff0a3f5ae2802a6f0d9e6348fbd854f580499bcfcf2fa26";
		string id = "0000";
		string nuanceURL = "https://dictation.nuancemobility.net:443/NMDPAsrCmdServlet/dictation?appId=" + appId + "&appKey=" + appKey + "&id=" + id;
		string translatedMessage = "null";
		//try
		//{
			System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
			HttpWebRequest nuanceRequest = (HttpWebRequest)WebRequest.Create(nuanceURL);           
			nuanceRequest.ProtocolVersion = HttpVersion.Version11;
			nuanceRequest.ContentType = "audio/x-wav;codec=pcm;bit=16;rate=16000";
			nuanceRequest.Accept = "text/plain";
			nuanceRequest.Method = WebRequestMethods.Http.Post;
			nuanceRequest.AuthenticationLevel = AuthenticationLevel.None;
			nuanceRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
			
			nuanceRequest.Headers["Accept-Language"] = "enus";
			nuanceRequest.Headers["Accept-Topic"] = "Dictation";
			nuanceRequest.Headers["X-Dictation-NBestListSize"] = "1";
			
			
			// Store the post data into the MemoryStream
			MemoryStream postDataStream = new MemoryStream();
			StreamWriter postDataWriter = new StreamWriter(postDataStream);
			
			// Read the file and write the bytes to the post stream
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			byte[] buffer = new byte[1024];
			int bytesRead = 0;
			while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
			{
				postDataStream.Write(buffer, 0, bytesRead);
			}
			fileStream.Close();
			postDataWriter.Flush();
			
			// Set the http request body content length
			nuanceRequest.ContentLength = postDataStream.Length;
			
			// Dump the post data from the memory stream to the request stream
			using (Stream s = nuanceRequest.GetRequestStream())
			{
				postDataStream.WriteTo(s);
			}
			postDataStream.Close();
			
			// Get the response from the server
			StreamReader responseReader = new StreamReader(nuanceRequest.GetResponse().GetResponseStream());
			translatedMessage = responseReader.ReadToEnd();
		//}
		/*catch (WebException e)
		{
			// Thrown if a 200 is not returned
			print("Response code: " + (int)((HttpWebResponse)e.Response).StatusCode +
			                  "\t" + ((HttpWebResponse)e.Response).StatusDescription);
			throw new WebException();

			Debug.Log(e.StackTrace);
		}
		catch (FileNotFoundException file)
		{
			Console.WriteLine(file.Message);
			throw new FileNotFoundException();
		}*/
		return translatedMessage;
	}
}