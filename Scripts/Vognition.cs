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
    
	public void TransText()
	{
		string response = vogTransText("Raise the degrees", "usenglishmale", "en-US");
		Debug.Log (response);
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
            //data["sentence"] = "C:\\Users\\Richard\\Desktop\\sample.wma";
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
                int statusCode = (int) ((HttpWebResponse)e.Response).StatusCode;
                switch (statusCode)
                {
                    case 400:
                        return "Response code: " + statusCode + "\tBad request";

                    case 401:
                        return "Response code: " + statusCode + "\tUnauthorized authentication";

                    case 403:
                        return "Response code: " + statusCode + "\tResource is not availible";

                    case 409:
                        return "Response code: " + statusCode + "\tMissing Parameters";

                    case 501:
                        return "Response code: " + statusCode + "\tFeature not yet implemented";
                        
                    case 503:
                        return "Response code: " + statusCode + "\tThere is an issue on Vognition";

                    case 504:
                        return "Response code: " + statusCode + "\tComponent Time Out";

                    default:
                        return ((HttpWebResponse)e.Response).StatusDescription;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }    
        }
        return retVal;
    }
}


