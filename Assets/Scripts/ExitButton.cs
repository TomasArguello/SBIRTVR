using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{

    //GameObject VRUser;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("disconnectPlayer");
        Debug.Log("The coroutine disconnectPlayer has been called!");
    }

    IEnumerator disconnectPlayer()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("Player " + PhotonNetwork.NickName + " has disconnected from the room");
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        SceneManager.LoadScene("LoginScene");
    }
}
