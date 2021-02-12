using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoginScreen : MonoBehaviour
{
    public TMP_InputField user_inputField;
    public TMP_InputField Pass_inputField;

    public void sendData()
    {
        StartCoroutine(Upload());
    }

    public void receiveData()
    {
        StartCoroutine(GetRequest(""));
    }
    
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_inputField.text);
        form.AddField("Password", Pass_inputField.text);


        using (UnityWebRequest www = UnityWebRequest.Post("https://aqugzmhltj.execute-api.us-east-1.amazonaws.com/default/Log_In", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
