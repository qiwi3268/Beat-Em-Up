using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopper : MonoBehaviour
{
    public GameObject otherObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CameraMidPoint")
        {
            otherObject = other.transform.parent.gameObject;
            CameraController.isFollowing = false;
            gameObject.SetActive(false);
        }
        else
            return;

    }
}
