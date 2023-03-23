using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButt : MonoBehaviour
{

    public string roomName;

    public Sprite standardButt;
    public Sprite selectedButt;
    public Sprite fullButt;

    private void Start()
    {
        roomName = gameObject.GetComponentInChildren<Text>().text;
        RoomSelectEvent.current.onButtonClick += onClick;
    }

    //This will be called after the user selects a room and the Action onButtonClick is called, which will activate this function on every button 
    public void onClick()
    {
        if(RoomSelectEvent.current.selectedRoom != this)
        {
            gameObject.GetComponent<Image>().sprite = standardButt;
        }
    }

    //This will be called when the user selects this room specifically
    public void roomSelected()
    {
        RoomSelectEvent.current.selectedRoom = this;
        gameObject.GetComponent<Image>().sprite = selectedButt;

        RoomSelectEvent.current.ButtonClick();
    }
}
