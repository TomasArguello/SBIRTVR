using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class WebComms : MonoBehaviour
{
    public string dataReceived;

    private void Start()
    {
        dataReceived = "";
    }

    public void SendData(string data)
    {
        StartCoroutine(PostData(data));
    }


    
    IEnumerator PostData(string data)
    {
        string message = data;
        byte[] rawData = Encoding.UTF8.GetBytes($"{{\"message\":\"{message}\"}}");
        UnityWebRequest request = UnityWebRequest.Post("http://c3f6-165-91-13-189.ngrok.io/predict", "");
        request.uploadHandler = new UploadHandlerRaw(rawData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
            dataReceived = "Didn't get shit!";
        }
        else
        {
            dataReceived = request.downloadHandler.text;
            Debug.Log("The patient has responded!! He said " + dataReceived);
        }
    }
}
