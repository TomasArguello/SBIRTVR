using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreVRUser : MonoBehaviour
{

    [SerializeField]
    GameObject GenericVRPlayerPrefab;

    private void Awake()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), GenericVRPlayerPrefab.GetComponent<Collider>(), true);
    }

}
