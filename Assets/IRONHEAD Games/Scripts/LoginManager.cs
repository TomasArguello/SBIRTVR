using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks 
{
    public TMP_InputField PlayerName_InputField;
    public string loadableRoom;
    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    public void ConnectToPhotonServer()
    {
        //if (PlayerName_InputField != null) 
        //{
        //    PhotonNetwork.NickName = PlayerName_InputField.text;
            PhotonNetwork.ConnectUsingSettings();
       // }
    }
    #region  Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. The server is available.");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the Master Server with player name: " + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("HomeScene");
    }
    #endregion
}
