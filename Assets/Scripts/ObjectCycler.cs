using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCycler : MonoBehaviour
{
    public GameObject[] OrderedCycle = new GameObject[0];
    public bool ResetOnEnable = true;

    private void Awake()
    {
        Reset();
    }

    private void OnEnable()
    {
        if (ResetOnEnable)
            Reset();
    }

    public void Reset()
    {
        foreach (GameObject o in OrderedCycle)
            o.SetActive(false);
        
        if (OrderedCycle.Length > 0)
            OrderedCycle[0].SetActive(true);
    }

    public void Next()
    {
        int next = -1;
        for (int i = 0; i < OrderedCycle.Length; i++)
        {
            if (OrderedCycle[i].activeSelf)
                next = (i+1) % OrderedCycle.Length;
            OrderedCycle[i].SetActive(false);
        }

        if (next != -1)
            OrderedCycle[next].SetActive(true);
    }
}
