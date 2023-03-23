using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AudioLogController : MonoBehaviour
{
    private PhotonView PV;

    private void Start()
    {
        //PV = GetComponent<PhotonView>();
        //PV.RPC("DebugAttempt", RpcTarget.AllBuffered);
    }

    /// <summary>
    /// Sends the voiceLine to the Admin side and records it in the conversionalLog
    /// </summary>
    /// <param name="voiceLine">The voiceLine being sent to the Admin side</param>
    public void SendVoiceLine(string voiceLine)
    {
        /*
        if (PV.IsMine)
        {
            PV.RPC("RPC_TransferVoiceLine", RpcTarget.MasterClient, voiceLine);
        }
        */
        PV.RPC("RPC_TransferVoiceLine", RpcTarget.MasterClient, voiceLine, PhotonNetwork.NickName);
    }

    [PunRPC]
    void DebugAttempt()
    {
        Debug.Log("HELPPPPP!");
    }
}
