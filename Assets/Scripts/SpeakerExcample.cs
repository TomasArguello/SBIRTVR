using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Crosstales.RTVoice;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class SpeakerExcample : MonoBehaviour, IOnEventCallback  
{
    private string VoiceName;
    public AudioSource Source;
    public string Patient1VoiceName;
    public string Patient2VoiceName;
    public string Patient3VoiceName;
    public const byte PlaySentenceWithRTVoice = 1;

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == PlaySentenceWithRTVoice)
        {
            object[] data = (object[])photonEvent.CustomData;
            string sentence = (string)data[0];
            Debug.Log(sentence);
            Speak(sentence);
        }
    }

    public string SetVoiceName(string VoiceName)
    {
        this.VoiceName = VoiceName;
        return VoiceName;
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void Speak(string sentence)
    {
        Debug.Log("Event is received");

        Speaker.Instance.Speak(
            sentence, Source, Speaker.Instance.VoiceForName(VoiceName));
    }
}
