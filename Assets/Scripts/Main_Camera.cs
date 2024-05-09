using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    public Transform target; // Reference to the object you want the camera to follow
    public Vector3 offset;

    private void Update()
    {
        if (GameManager.GM.instantiatedObject != null)
        {
            target = GameManager.GM.instantiatedObject.transform;
            transform.position = target.position + offset;
        }
    }
}