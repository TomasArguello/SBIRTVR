using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI occupancyNumber;

    public GameObject waitingObject;
    public SpeakerExcample SpeakerManager;
    public static GameObject Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
            // in Photon the Lobby is the place where all available rooms are listed
            // PhotonNetwork.JoinLobby();
            /*
            if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
            {
                waitingObject.SetActive(false);
            }
            else
            {
                waitingObject.SetActive(true);
            }
            */
            //waitingObject.SetActive(true);
        }
    }

    private void Start()
    {
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"])
        {
            waitingObject.SetActive(false);
        }
        else
        {
            waitingObject.SetActive(true);
        }
        Debug.Log("The customProperty hasAdmin is " + (bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"] + " and this was checked at Start()! The waitingObject should be " + waitingObject.activeSelf);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to Lobby");
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"])
        {
            waitingObject.SetActive(false);
        }
        else
        {
            waitingObject.SetActive(true);
        }
        Debug.Log("The customProperties have been updated. The hasAdmin bool Custom Property of this room is " + (bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"] + " and the waitingObject is set to " + waitingObject.activeSelf);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player joined. Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        
        Debug.Log("A new player has joined. The hasAdmin bool Custom Property of this room is " + (bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"]);
        //Checks if the room has an admin in order to turn off the waitingObject
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"])
        {
            waitingObject.SetActive(false);
        }
        else
        {
            waitingObject.SetActive(true);
        }
        
        /*
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            waitingObject.SetActive(false);
            //Instance = (GameObject)newPlayer.TagObject;
            //SpeakerExcample.GetAudioSource();
        }
        else
        {
            waitingObject.SetActive(true);
        }
        */
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("player leaves. Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("The hasAdmin bool is " + (bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"]);
        /*
        if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"])
        {
            waitingObject.SetActive(true);
        }
        else
        {
            waitingObject.SetActive(false);
        }
        */
        /*
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            waitingObject.SetActive(true);
        }
        else
        {
            waitingObject.SetActive(false);
        }
        */
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            //no room at all
            occupancyNumber.text = 0 + "person";
            Debug.Log("Player count is 0");
        }

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            occupancyNumber.text = room.PlayerCount + "person";
            Debug.Log("Player count is " + room.PlayerCount);
        }
    }
}