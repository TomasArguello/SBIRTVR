using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectEvent : MonoBehaviour
{
    public static RoomSelectEvent current;

    public RoomButt selectedRoom;

    private void Awake()
    {
        current = this;
    }

    public event Action onButtonClick;

    public void ButtonClick()
    {
        if(onButtonClick != null)
        {
            onButtonClick();
        }
    }

    public void startGame()
    {
        if (selectedRoom == null)
        {
            Debug.Log("A room has not been selected yet!");
            return;
        }
        GameObject.Find("Login Manager").GetComponent<LoginIn>().CreateOrJoinRoom(selectedRoom.gameObject.GetComponentInChildren<Text>().text);
    }
}
