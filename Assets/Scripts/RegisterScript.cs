using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterScript : MonoBehaviour
{
    public TextMeshProUGUI TMPuser_id;
    public TMP_InputField user_inputField;
    public TMP_InputField DOB_inputField;
    public TMP_InputField Pass_inputField;
    public void sendData()
    {
        //TMPuser_id.text = user_inputField.text;
        StartCoroutine(Upload());
    }
    public void setBirth()
    {

    }
    public void setPass()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("https://aqugzmhltj.execute-api.us-east-1.amazonaws.com/default/Register/?user_id=" + user_inputField.text + "&DOB=" + DOB_inputField.text + "&Password=" + Pass_inputField.text));

        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));

        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_inputField.text);
        form.AddField("password", Pass_inputField.text);
        form.AddField("DOB", DOB_inputField.text);
        

        using (UnityWebRequest www = UnityWebRequest.Post("https://aqugzmhltj.execute-api.us-east-1.amazonaws.com/default/Register", form))
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

}
