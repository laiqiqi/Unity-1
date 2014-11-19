using System;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Client  class to access Vognitions voice recognition services
/// </summary>
public class Vognition
{
    // Private global variables with default values
    private string vogHostname = "sample.whataremindsfor.com";
    private int vogPortnumber = 46900;
    private string vogAppkey = "59cddd076cf2a00b9f5555585c177276d8a49873";
    private string vogAppsecret = "01d3cb9455722c2f6760e11f14a0571179649873";
    private string vogConkey = "09326eefff2cad2ae9f4c0fd5d4c7b71db249873";
    private string vogConsecret = "7092379b31cd621c2c1b574e47ec956733f49873";
    private string ttsSpeakerType = "usenglishfemale";
    private string locale = "en-US";
    private string requestType = "ASR";
    private string HPDL_ID = "TEST_HPM_ID";

    /// <summary>
    /// This function calls the initial sets functions for all the global private variables
    /// </summary>    
    public void vogDoConfigure(string hostname, int portnumber, string appkey, string appsecret, string conkey, string consecret)
    {
        vogSetHostname(hostname);
        vogSetPortnumber(portnumber);
        vogSetAppKey(appkey);
        vogSetAppSecret(appsecret);
        vogSetConsumerKey(conkey);
        vogSetConsumerSecret(consecret);
    }
					
    /// <summary>
    /// This function is used to assign the vogHostname variable
    /// </summary>
    /// <param name="hostname"></param>
    public void vogSetHostname(string hostname) 
    {
        this.vogHostname = hostname;
    }

    /// <summary>
    /// This function is used to assign the vogPortnumber variable
    /// </summary>
    /// <param name="portnumber"></param>
    public void vogSetPortnumber(int portnumber)
    {
        this.vogPortnumber = portnumber;
    }

    /// <summary>
    /// This function is used to assign the vogAppkey variable
    /// </summary>
    /// <param name="appkey"></param>
    public void vogSetAppKey(string appkey)
    {
        this.vogAppkey = appkey;
    }

    /// <summary>
    /// This function is used to assign the vogAppsecret variable
    /// </summary>
    /// <param name="appsecret"></param>
    public void vogSetAppSecret(string appsecret)
    {
        this.vogAppsecret = appsecret;
    }

    /// <summary>
    /// This function is used to assign the vogConkey variable
    /// </summary>
    /// <param name="conkey"></param>
    public void vogSetConsumerKey(string conkey) 
    {
        this.vogConkey = conkey;
    }

    /// <summary>
    /// This function is used to assign the vogConsecret variable
    /// </summary>
    /// <param name="consecret"></param>
    public void vogSetConsumerSecret(string consecret)
    {
        this.vogConsecret = consecret;
    }

    /// <summary>
    /// This function is used to get the vogAppkey variable
    /// </summary>
    /// <returns></returns>
    public string vogGetAppKey()
    {
        return this.vogAppkey;
    }

    /// <summary>
    /// This function is used to get the vogAppsecret variable
    /// </summary>
    /// <returns></returns>
    public string vogGetAppSecret()
    {
        return this.vogAppsecret;
    }

    /// <summary>
    /// This function is used to get the vogConkey variable
    /// </summary>
    /// <returns></returns>
    public string vogGetConsumerKey()
    {
        return this.vogConkey;
    }

    /// <summary>
    /// This function is used to get the vogConsecret variable
    /// </summary>
    /// <returns></returns>
    public string vogGetConsumerSecret()
    {
        return this.vogConsecret;
    }

    /// <summary>
    /// This function is used to get the vogHostname variable
    /// </summary>
    /// <returns></returns>
    public string vogGetHostname()
    {
        return this.vogHostname;
    }

    /// <summary>
    /// This function is used to get the vogPortnumber variable
    /// </summary>
    /// <returns></returns>
    public int vogGetPortnumber()
    {
        return this.vogPortnumber;
    }

    /// <summary>
    /// This API submits a textual sentence to be translated to a machine actionable command 
    /// assembled in Vognition. It is submitted to the API via an HTTP POST
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    public string vogTransText(string sentence)
    {
        // Strip any non-alphanumeric characters from the translated message
        sentence = Regex.Replace(sentence, @"[^0-9a-zA-Z ]+", "");

        NameValueCollection data = new NameValueCollection();
        // populate the NameValueCollection with Vognition parameters
        data["appkey"] = vogGetAppKey();
        data["appsecret"] = vogGetAppSecret();
        data["conkey"] = vogGetConsumerKey();
        data["consecret"] = vogGetConsumerSecret();
        data["sentence"] = sentence;
        data["ttsSpeakerType"] = ttsSpeakerType;
        data["locale"] = locale;
        data["requestType"] = requestType;
        data["HPDL_ID"] = HPDL_ID;

        string transTextURL = "http://" + vogHostname + ":" + vogPortnumber + "/apiv1/transtext";
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
   
    /// <summary>
    /// This API submits a voice file to be analyzed by the Vognition service using the iSpeech ASR. It
    /// is submitted to the API via an HTTP POST
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public string vogDictation(string filePath)
    {
        string appId = "NMDPTRIAL_le_richard206520141105025111";
        string appKey = "db3253ad1bdd222d25517ef496be850cf7c8d74044b6d750c09b7f4963f7593e2aaceb458a0e72af5ff0a3f5ae2802a6f0d9e6348fbd854f580499bcfcf2fa26";
        string id = "0000";
        string nuanceURL = "https://dictation.nuancemobility.net:443/NMDPAsrCmdServlet/dictation?appId=" + appId + "&appKey=" + appKey + "&id=" + id;
        string translatedMessage = "null";
        try
        {
            // handles certification
            SSLValidator.OverrideValidation();
            HttpWebRequest nuanceRequest = (HttpWebRequest)WebRequest.Create(nuanceURL);
            nuanceRequest.ProtocolVersion = HttpVersion.Version11;
            nuanceRequest.ContentType = "audio/x-wav;codec=pcm;bit=16;rate=16000";
            nuanceRequest.Accept = "text/plain";
            nuanceRequest.Method = WebRequestMethods.Http.Post;
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
        }
        catch (WebException e)
        {
            // Thrown if a 200 is not returned from server
            Console.WriteLine("Response code: " + (int)((HttpWebResponse)e.Response).StatusCode +
                          "\t" + ((HttpWebResponse)e.Response).StatusDescription);
            throw new WebException();
        }
        catch (FileNotFoundException file)
        {
            Console.WriteLine(file.Message);
            throw new FileNotFoundException();
        }
        return translatedMessage;
    }

    /// <summary>
    /// Registers a user associated to the application ID
    /// </summary>
    /// <returns></returns>
    public string vogRegisterUser ()
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = vogGetAppKey();
        data["appsecret"] = vogGetAppSecret();

        string transTextURL = vogHostname + ":" + vogPortnumber + "/apiv1/user";
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
                return statusCode + "\t" + ((HttpWebResponse)e.Response).StatusDescription;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

    /// <summary>
    /// Creates a home profile ID
    /// </summary>
    /// <returns></returns>
    public string vogCreateHomeProfileID(string regConKey)
    {
        NameValueCollection data = new NameValueCollection();
        data["appkey"] = vogGetAppKey();
        data["appsecret"] = vogGetAppSecret();
        data["conkey"] = vogGetConsumerKey();
        data["consecret"] = vogGetConsumerSecret();
        data["HPDL_ID"] = HPDL_ID;
        data["roleeKey"] = regConKey;

        string transTextURL = vogHostname + ":" + vogPortnumber + "/apiv1/homeprofile";
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
                return statusCode + "\t" + ((HttpWebResponse)e.Response).StatusDescription;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        return retVal;
    }

	/// <summary>
	/// Creates a role to associate a different user to the homeprofile of the owner
	/// </summary>
	/// <returns></returns>
	public string vogCreateRole (string regConKey)
	{
		NameValueCollection data = new NameValueCollection();
		data["appkey"] = vogGetAppKey();
		data["appsecret"] = vogGetAppSecret();
		data["conkey"] = vogGetConsumerKey();
		data["consecret"] = vogGetConsumerSecret();
		data["HPDL_ID"] = HPDL_ID;
		data["roleeKey"] = regConKey;
		//data["role"] =

		string transTextURL = vogHostname + ":" + vogPortnumber + "/apiv1/role";
		string retVal = "_NULL";

		// Post the data to the role URL
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
				return statusCode + "\t" + ((HttpWebResponse)e.Response).StatusDescription;
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}
		return retVal;
	}
	
	/// <summary>
	/// Owner of the home profile can change the privileges of the user
	/// </summary>
	/// <returns></returns>
	public string vogChangeRole ()
	{
		NameValueCollection data = new NameValueCollection();
		data["appkey"] = vogGetAppKey();
		data["appsecret"] = vogGetAppSecret();
		data["conkey"] = vogGetConsumerKey();
		data["consecret"] = vogGetConsumerSecret();
		data["HPDL_ID"] = HPDL_ID;
		//data["accept"] =

		string transTextURL = vogHostname + ":" + vogPortnumber + "/apiv1/role";
		string retVal = "_NULL";

		// Put the data to the role URL
		using (var wb = new WebClient())
		{
			try
			{
				var response = wb.UploadValues(transTextURL, "PUT", data);
				retVal = Encoding.UTF8.GetString(response, 0, response.Length);
			}
			catch (WebException e)
			{
				int statusCode = (int)((HttpWebResponse)e.Response).StatusCode;
				return statusCode + "\t" + ((HttpWebResponse)e.Response).StatusDescription;
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}
		return retVal;
	}
	
	/// <summary>
	/// Removes a user from the associated home profile
	/// </summary>
	/// <returns></returns>
	public string vogDeleteRole (string regConKey)
	{
		NameValueCollection data = new NameValueCollection();
		data["appkey"] = vogGetAppKey();
		data["appsecret"] = vogGetAppSecret();
		data["conkey"] = vogGetConsumerKey();
		data["consecret"] = vogGetConsumerSecret();
		data["HPDL_ID"] = HPDL_ID;
		data["roleeKey"] = regConKey;

		string transTextURL = vogHostname + ":" + vogPortnumber + "/apiv1/role";
		string retVal = "_NULL";

		// Delete the data from the role URL
		using (var wb = new WebClient())
		{
			try
			{
				var response = wb.UploadValues(transTextURL, "DELETE", data);
				retVal = Encoding.UTF8.GetString(response, 0, response.Length);
			}
			catch (WebException e)
			{
				int statusCode = (int)((HttpWebResponse)e.Response).StatusCode;
				return statusCode + "\t" + ((HttpWebResponse)e.Response).StatusDescription;
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}
		return retVal;
	}
	
	/// <summary>
	/// User accepts a role access to another home profile
	/// </summary>
	/// <returns></returns>
	public string vogAcceptRole ()
	{
		NameValueCollection data = new NameValueCollection();
		data["appkey"] = vogGetAppKey();
		data["appsecret"] = vogGetAppSecret();
		data["conkey"] = vogGetConsumerKey();
		data["consecret"] = vogGetConsumerSecret();
		data["HPDL_ID"] = HPDL_ID;
		//data["accept"] = 

		string transTextURL = vogHostname + ":" + vogPortnumber + "/apiv1/assignedrolee";
		string retVal = "_NULL";

		// Post the data to the assignedRolee URL
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
				return statusCode + "\t" + ((HttpWebResponse)e.Response).StatusDescription;
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}
		return retVal;
	}
	
	/// <summary>
	/// User removes themselves from a role access to another home profile
	/// </summary>
	/// <returns></returns>
	public string vogRemoveRole ()
	{
		NameValueCollection data = new NameValueCollection();
		data["appkey"] = vogGetAppKey();
		data["appsecret"] = vogGetAppSecret();
		data["conkey"] = vogGetConsumerKey();
		data["consecret"] = vogGetConsumerSecret();
		data["HPDL_ID"] = HPDL_ID;

		string transTextURL = vogHostname + ":" + vogPortnumber + "/apiv1/assignedrolee";
		string retVal = "_NULL";

		// Delete the data from the assignedRolee URL
		using (var wb = new WebClient())
		{
			try
			{
				var response = wb.UploadValues(transTextURL, "DELETE", data);
				retVal = Encoding.UTF8.GetString(response, 0, response.Length);
			}
			catch (WebException e)
			{
				int statusCode = (int)((HttpWebResponse)e.Response).StatusCode;
				return statusCode + "\t" + ((HttpWebResponse)e.Response).StatusDescription;
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}
		return retVal;
	}
	
	
	
	/// <summary>
	/// Inner class called to handle HttpWebRequest certification
	/// </summary>
	public static class SSLValidator
    {
        private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public static void OverrideValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = OnValidateCertificate;
            ServicePointManager.Expect100Continue = true;
        }
    }


}