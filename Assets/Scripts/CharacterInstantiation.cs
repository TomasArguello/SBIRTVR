using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.RTVoice;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using CrazyMinnow.SALSA;

public class CharacterInstantiation : MonoBehaviour, IPunInstantiateMagicCallback
{
    private SpeakerExcample SpeakerManager;
    private string patientOneVoiceName;
    private string patientTwoVoiceName;
    private string patientThreeVoiceName;

    public void SetPatientVoiceNames(string voiceName1, string voiceName2, string voiceName3)
    {
        patientOneVoiceName = voiceName1;
        patientTwoVoiceName = voiceName2;
        patientThreeVoiceName = voiceName3;
    }

    private void Start()
    {
        try
        {
            SpeakerManager = GameObject.Find("[MANAGERS]/SpeakerManager").GetComponent<SpeakerExcample>();
            Debug.Log("Speaker Manager:" + SpeakerManager);
            SetPatientVoiceNames(SpeakerManager.Patient1VoiceName, SpeakerManager.Patient2VoiceName, SpeakerManager.Patient3VoiceName);
        }
        catch
        {
            Debug.LogError("Not able to find Speaker Manager! Make sure to place it under path [MANAGERS]/SpeakerManager!");
        }

        GetAudioSource(this.gameObject);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // Get reference to SpeakerManager to set voice name
        try
        {
            SpeakerManager = GameObject.Find("[MANAGERS]/SpeakerManager").GetComponent<SpeakerExcample>();
            Debug.Log("Speaker Manager:" + SpeakerManager);
            SetPatientVoiceNames(SpeakerManager.Patient1VoiceName, SpeakerManager.Patient2VoiceName, SpeakerManager.Patient3VoiceName);
        }
        catch
        {
            Debug.LogError("Not able to find Speaker Manager! Make sure to place it under path [MANAGERS]/SpeakerManager!");
        }

        Debug.Log("New player joined. Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            info.Sender.TagObject = this.gameObject;
            Debug.Log(info.Sender.TagObject);
            GetAudioSource(info.Sender);
        }
    }

    // this method gets a reference to the admin player's speaker for RT-Voice to use
    public void GetAudioSource(Player newPlayer)
    {
        Debug.Log("Firing off GetAudioSource");
        GameObject admin = (GameObject)newPlayer.TagObject;
        string objectName = admin.name;
        GameObject T = admin.transform.Find("RT-VoiceSpeaker").gameObject;
        Eyes eyesRef = admin.GetComponent<Eyes>();

        GameObject player = SpawnManager.LocalPlayerInstance;

        if (T != null)
        {
            AudioSource Source = T.GetComponent<AudioSource>();
            Debug.Log("RT-Voice compiled.");

            if (objectName == "PatientOnePrefab" + "(Clone)")
            {
                Debug.Log("Object name is: " + objectName);
                string tempVoice = SpeakerManager.SetVoiceName(patientOneVoiceName);
                Debug.Log("Selected voice kind is: " + tempVoice);
            }

            else if (objectName == "PatientTwoPrefab" + "(Clone)")
            {
                Debug.Log("Object name is: " + objectName);
                string tempVoice = SpeakerManager.SetVoiceName(patientTwoVoiceName);
                Debug.Log("Selected voice kind is: " + tempVoice);
            }

            else if (objectName == "PatientThreePrefab" + "(Clone)")
            {
                Debug.Log("Object name is: " + objectName);
                string tempVoice = SpeakerManager.SetVoiceName(patientThreeVoiceName);
                Debug.Log("Selected voice kind is: " + tempVoice);
            }

            else
            {
                Debug.LogWarning("Object name that was instantiated was not valid, it is actually: " + objectName);
            }

            SpeakerManager.GetComponent<SpeakerExcample>().Source = Source;

            GameObject cameraObject = player.transform.Find("XR-Rig/Camera-Offset/Main-Camera").gameObject;
            eyesRef.lookTarget = cameraObject.transform;
        }
        else
        {
            Debug.LogError("Unable to find RT-Voice AudioSource");
        }
    }

    public void GetAudioSource(GameObject newPlayer)
    {
        Debug.Log("Firing off GetAudioSource");
        GameObject admin = newPlayer;
        string objectName = admin.name;
        GameObject T = admin.transform.Find("RT-VoiceSpeaker").gameObject;
        Eyes eyesRef = admin.GetComponent<Eyes>();

        GameObject player = SpawnManager.LocalPlayerInstance;
        if (T != null)
        {
            AudioSource Source = T.GetComponent<AudioSource>();
            Debug.Log("RT-Voice compiled.");

            if (objectName == "PatientOnePrefab" + "(Clone)")
            {
                Debug.Log("Object name is: " + objectName);
                string tempVoice = SpeakerManager.SetVoiceName(patientOneVoiceName);
                Debug.Log("Selected voice kind is: " + tempVoice);
            }

            else if (objectName == "PatientTwoPrefab" + "(Clone)")
            {
                Debug.Log("Object name is: " + objectName);
                string tempVoice = SpeakerManager.SetVoiceName(patientTwoVoiceName);
                Debug.Log("Selected voice kind is: " + tempVoice);
            }

            else if (objectName == "PatientThreePrefab" + "(Clone)")
            {
                Debug.Log("Object name is: " + objectName);
                string tempVoice = SpeakerManager.SetVoiceName(patientThreeVoiceName);
                Debug.Log("Selected voice kind is: " + tempVoice);
            }

            else
            {
                Debug.LogWarning("Object name that was instantiated was not valid, it is actually: " + objectName);
            }

            SpeakerManager.GetComponent<SpeakerExcample>().Source = Source;

            GameObject cameraObject = player.transform.Find("XR-Rig/Camera-Offset/Main-Camera").gameObject;
            eyesRef.lookTarget = cameraObject.transform;
        }
        else
        {
            Debug.LogError("Unable to find RT-Voice AudioSource");
        }
    }

}