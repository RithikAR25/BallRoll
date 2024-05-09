using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterCameraPass : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GameManager.GM.MainCamera.transform.position.z > (50 + this.transform.position.z))
        {
            Destroy(gameObject);
        }
    }
}