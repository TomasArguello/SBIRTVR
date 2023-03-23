using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject GenericVRPlayerPrefab;
    public static GameObject LocalPlayerInstance;
    public GameObject patientPrefab;
    public static GameObject LocalPatientInstance;


    public Vector3 spawnPosition;

    private void Awake()
    {
        LocalPlayerInstance = Instantiate<GameObject>(GenericVRPlayerPrefab, spawnPosition, gameObject.transform.rotation);
        Quaternion patientRotation = Quaternion.Euler(new Vector3(2.85f, 147.796f, 0.345f));
        LocalPatientInstance = Instantiate<GameObject>(patientPrefab, new Vector3(-1.273f, 0.372f, 0.517f), patientRotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        Debug.Log("Spawn VR player");
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Spawn V");
            LocalPlayerInstance = PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
        }
        
        else
        {
            Debug.Log("Failed to connect to Photon");
        }
        */

        
    }
}
