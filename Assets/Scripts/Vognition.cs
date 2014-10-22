using System;
using System.Collections;
using UnityEngine;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;

public class Vognition : MonoBehaviour
{

    string postScoreURL = "http://sample.whataremindsfor.com:46900/apiv1/transtext";
	string postScoreURL2 = "http://sample.whataremindsfor.com:46900/apiv1/recognize";
	
	public void Recognize()
	{
		string response = vogSpeechRecognition(Path.GetFileName("Audio/RaiseTheDegressBy5.wav"), "usenglishmale", "en-US");
		Debug.Log(response);
	}

    public void TransText()
    {
        string response = vogTransText("Raise the degrees by 5", "usenglishmale", "en-US");
        Debug.Log(response);
    }

	public string vogSpeechRecognition(String wav, String ttsSpeakerType, String locale)
	{
		string retVal = "_NULL";
		using (var wb = new WebClient())
		{
			var data = new NameValueCollection();
			//  **CAPSTONE KEYS**
			data["appkey"] = "59cddd076cf2a00b9f5555585c177276d8a49873";
			data["appsecret"] = "01d3cb9455722c2f6760e11f14a0571179649873";
			data["conkey"] = "09326eefff2cad2ae9f4c0fd5d4c7b71db249873";
			data["consecret"] = "7092379b31cd621c2c1b574e47ec956733f49873";
			data["wav"] = wav;
			data["ttsSpeakerType"] = ttsSpeakerType;
			data["locale"] = locale;
			data["requestType"] = "ASR";
			data["HPDL_ID"] = "TEST_HPM_ID";
			try
			{
				var response = wb.UploadValues(postScoreURL2, "POST", data);
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

    public string vogTransText(String sentence, String ttsSpeakerType, String locale)
    {
        string retVal = "_NULL";
        using (var wb = new WebClient())
        {
            var data = new NameValueCollection();
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
            data["ttsSpeakerType"] = ttsSpeakerType;
            data["locale"] = locale;
            data["requestType"] = "ASR";
            data["HPDL_ID"] = "TEST_HPM_ID";
            try
            {
                var response = wb.UploadValues(postScoreURL, "POST", data);
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
}