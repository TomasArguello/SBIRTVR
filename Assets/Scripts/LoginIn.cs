using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class LoginIn : MonoBehaviourPunCallbacks
{
    private Dictionary<string, RoomInfo> cachedRoomInfo = new Dictionary<string, RoomInfo>();

    public int baseRooms;

    public GameObject[] roomButtList = new GameObject[15];
    //public GameObject roomButton;

    [HideInInspector]
    GameObject startButton;
    GameObject background;
    //GameObject roomsPanel;

    private void Awake()
    {
        startButton = GameObject.Find("StartButton");
        background = GameObject.Find("Background");
        //roomsPanel = GameObject.Find("RoomsPanelParent");
        //roomsPanel.SetActive(false);
    }

    private void Start()
    {
        ConnectToPhotonServer();
    }

    public void ConnectToPhotonServer()
    {
        PhotonNetwork.NickName = "VRuser";
        PhotonNetwork.ConnectUsingSettings();
    }

    #region  Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. The server is available.");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the Master Server with player name: " + PhotonNetwork.NickName);
        //PhotonNetwork.AutomaticallySyncScene = true;
        //JoinRandomRoom(); Turned this off for now
        //PhotonNetwork.LoadLevel("HomeSceneCharSetup");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        //CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);
        //Load the Main Scene
        Debug.Log("The hasAdmin bool Custom Property is " + (bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"]);
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["hasAdmin"]){
            PhotonNetwork.NickName = "VR_User_" + (PhotonNetwork.CurrentRoom.PlayerCount - 1);
        }
        else
        {
            PhotonNetwork.NickName = "VR_User_" + PhotonNetwork.CurrentRoom.PlayerCount;
        }
        Debug.Log("The new name of the user is " + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("MainSceneCharSetup");

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to Lobby");
        //startButton.SetActive(false);
        //background.SetActive(false);
        //roomsPanel.SetActive(true);
        cachedRoomInfo.Clear();
        //GameObject.Find("Background").GetComponent<RoomSelectEvent>().startGame();
        //initializeBaseRooms();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);

        Debug.Log("OnRoomListUpdate has been called! The following rooms are: ");
        bool roomIsInDict = false;

        for(int i=0; i < background.transform.childCount; i++)
        {
            string currentRoomText = background.transform.GetChild(i).GetComponentInChildren<Text>().text;
            roomIsInDict = cachedRoomInfo.ContainsKey(currentRoomText);

            if (roomIsInDict && (bool)cachedRoomInfo[currentRoomText].CustomProperties["hasAdmin"])
            {
                Debug.Log("Is there an Admin in Room " + currentRoomText + "? The answer is " + (bool)cachedRoomInfo[currentRoomText].CustomProperties["hasAdmin"]);
            }
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room..trying again");
        //CreateAndJoinRoom();
    }

    #endregion

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion

    #region Unique Functions
    /*
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    */

    public void CreateOrJoinRoom(string selectedRoomName)
    {
        string roomName = selectedRoomName;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5; //Dr Seo wants 1 admin, and like 3 VR users at a time

        if (cachedRoomInfo.ContainsKey(selectedRoomName))
        {
            PhotonNetwork.JoinRoom(selectedRoomName);
        }
        else
        {
            bool hasAdmin = false;
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
            {
                {"hasAdmin", hasAdmin }
            };
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "hasAdmin" };
            roomOptions.BroadcastPropsChangeToAll = true;
            Debug.Log("The customRoomProperties of this room will be hasAdmin and it is " + (bool)roomOptions.CustomRoomProperties["hasAdmin"]);
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
        }
    }

    //Updates the list of rooms, or creates one if there isn't one
    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomInfo.Remove(info.Name);
            }
            else
            {
                cachedRoomInfo[info.Name] = info;
            }
        }

        Debug.Log("The avaiable rooms are: ");
        foreach (KeyValuePair<string, RoomInfo> entry in cachedRoomInfo)
        {
            Debug.Log("Room: " + entry.Value.Name);
        }

        Debug.Log("The number of rooms are " + cachedRoomInfo.Count);

        //initializeRoomOrder();
        //GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().sizeDelta = new Vector2(100, 75 + (((baseRooms + cachedRoomInfo.Count) - 5) * 15));
        //GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().anchorMin = new Vector2(.5f, 1f);
        //GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().anchorMax = new Vector2(.5f, 1f);
        //Debug.Log("The final height of the RoomPanelSpawn is " + GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().sizeDelta.y);
    }

    /*
    //Initializes the order of rooms to be shown
    public void initializeRoomOrder()
    {
        int i = 0;
        Transform lastBaseRoom = GameObject.Find("RoomsPanelSpawn").transform.GetChild(GameObject.Find("RoomsPanelSpawn").transform.childCount - 1);
        Debug.Log("The lastBaseRoom is " + lastBaseRoom.GetComponent<Text>().text);
        foreach (KeyValuePair<string, RoomInfo> entry in cachedRoomInfo)
        {
            roomButtList[i] = Instantiate(roomButton, lastBaseRoom.position, lastBaseRoom.rotation, GameObject.Find("RoomsPanelSpawn").transform);
            roomButtList[i].gameObject.transform.localPosition = new Vector2(0, -8 - 15 * (baseRooms + i));
            //roomButtList[i].GetComponent<RoomButtons>().roomOrder = i;
            roomButtList[i].GetComponent<Text>().text = entry.Value.Name;
            roomButtList[i].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
            roomButtList[i].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
            i++;
        }

        //Debug.Log("The height of the RoomPanelSpawn is now " + GameObject.Find("RoomPanelSpawn").GetComponent<RectTransform>().sizeDelta.y);
    }
    */

    /*
    public void initializeBaseRooms()
    {
        GameObject currentButton;

        for (int i = 0; i < baseRooms; i++)
        {
            currentButton = Instantiate(roomButton, GameObject.Find("RoomsPanelSpawn").transform);
            currentButton.gameObject.transform.localPosition = new Vector2(0, -8 - 15 * i);
            currentButton.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
            currentButton.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
            //currentButton.GetComponent<RoomButton>().roomOrder = i;
            currentButton.GetComponent<Text>().text = "Room " + (i + 1);
        }

        GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().sizeDelta = new Vector2(100, 75 + ((baseRooms - 5) * 15));
        Debug.Log("The height of the RoomsPanelSpawn is " + GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().sizeDelta.y);
        GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().anchorMin = new Vector2(.5f, 1f);
        GameObject.Find("RoomsPanelSpawn").GetComponent<RectTransform>().anchorMax = new Vector2(.5f, 1f);
    }
    */

    #endregion
}
