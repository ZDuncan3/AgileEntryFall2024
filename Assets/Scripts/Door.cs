using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Range(-359f, 359f)]public float degreesToRotate = 90f;

    public bool isOpen = false;

    public void OpenDoor()
    {
        transform.Rotate(Vector3.up, degreesToRotate);
    }
}
