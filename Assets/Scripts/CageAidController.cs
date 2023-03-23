using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CageAidController : MonoBehaviour
{

    Image cageAidImage;

    MeshRenderer cageAidMeshRend;

    bool isOn;

    InputDevice rightHand;

    bool triggerValue;
    bool hasBeenTriggered;

    // Start is called before the first frame update
    void Start()
    {
        //This is supposed to be on the canvas that is a child of the main camera of every Generic VR Player
        if(gameObject.name == "CageAidQuestionsPlane")
        {
            cageAidMeshRend = gameObject.GetComponent<MeshRenderer>();
            if (cageAidMeshRend.enabled)
            {
                isOn = true;
            }
            else
            {
                isOn = false;
            }
        }
        else
        {
            //For whenever the user wants to use the version of questions that is in the canvas of the user XR Rig camera
            cageAidImage = transform.GetChild(0).GetComponent<Image>();
            if (cageAidImage.color.a == 1f)
            {
                isOn = true;
            }
            else
            {
                isOn = false;
            }

        }

        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        triggerValue = true;
        hasBeenTriggered = false;
    }

    private void Update()
    {
        if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue && !hasBeenTriggered)
        {
            isOn = !isOn;
            hasBeenTriggered = true;
            if (!isOn)
            {
                if(cageAidImage != null)
                {
                    Color temp = cageAidImage.color;
                    temp.a = 0f;
                    cageAidImage.color = temp;
                }
                else
                {
                    cageAidMeshRend.enabled = false;
                }
            }
            else
            {
                if(cageAidImage != null)
                {
                    Color temp = cageAidImage.color;
                    temp.a = 1f;
                    cageAidImage.color = temp;
                }
                else
                {
                    cageAidMeshRend.enabled = true;
                }
            }
            StartCoroutine("activatingPanel");
        }
    }

    IEnumerator activatingPanel()
    {
        yield return new WaitForSeconds(0.25f);
        hasBeenTriggered = false;
    }

}
