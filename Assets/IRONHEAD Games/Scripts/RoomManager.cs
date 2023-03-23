using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI occupancy;
    [Tooltip("Room to be loaded into.")] public string loadableRoom;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        //if (PhotonNetwork.IsConnectedAndReady)
        //{
         //   Debug.Log("Connected");
         //   // in Photon the Lobby is the place where all available rooms are listed
        //    PhotonNetwork.JoinLobby();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion

    #region Photo Callback methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);
        //Load the School Scene
       PhotonNetwork.LoadLevel(loadableRoom);

    }

    //when new player comes in, this method is called
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player joined to Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    { 
        if (roomList.Count == 0)
        {
            //no room at all
            occupancy.text = 0 + "person";
            Debug.Log("Player count is 0");
        }

        foreach (RoomInfo room in roomList) 
        {
            Debug.Log(room.Name);
            occupancy.text = room.PlayerCount + "person";
            Debug.Log("Player count is " + room.PlayerCount);
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to Lobby");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room..trying again");
        CreateAndJoinRoom();
    }

    #endregion

    void CreateAndJoinRoom() 
    {
        string randomRoomName = "Room_" + Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
}
