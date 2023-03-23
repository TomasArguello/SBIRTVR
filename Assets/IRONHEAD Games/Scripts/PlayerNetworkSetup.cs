using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Crosstales.RTVoice;

/**
 This script will be executed as long as a new player is instantiated
 */
public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXRRigGameObject;
    public GameObject MainAvatarGameobject;
    public string VoiceName;

    // Start is called before the first frame update
    void Start()
    {
        //Setup the player
        // RE-ENABLE THIS IF YOU NEED IT FOR MULTIPLAYER
        /*
        //RemoteXRRigGameObject.SetActive(false);
        if (photonView.IsMine)
        {
            LocalXRRigGameObject.SetActive(true);
            gameObject.GetComponent<AvatarInputConverter>().enabled = true;
            //add audio listener 
            MainAvatarGameobject.AddComponent<AudioListener>();
        }
        else
        {
            LocalXRRigGameObject.SetActive(false);
            gameObject.GetComponent<AvatarInputConverter>().enabled = false;
        }
        */

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void callRpc()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Speak", RpcTarget.All);
    }

    [PunRPC]
    public void Speak()
    {
        Debug.Log("RPC is called");
        Speaker.Instance.Speak(
            "Hello World", null, Speaker.Instance.VoiceForName(VoiceName));
    }
}
