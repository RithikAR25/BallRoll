using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public GameObject PlanePrefab;
    private float spawn_point_z = 30f;
    private float Plane_spawn_z = 130f;
    public GameObject instantiatedPlane;

    private void FixedUpdate()
    {
        if (GameManager.GM.instantiatedObject != null)
        {
            if (GameManager.GM.instantiatedObject.transform.position.z > spawn_point_z)
            {
                Spawner();
            }
        }
    }

    private void Spawner()
    {
        spawn_point_z += 40;
        instantiatedPlane = Instantiate(PlanePrefab, new Vector3(0f, 0f, Plane_spawn_z), Quaternion.identity);
        Plane_spawn_z += 100;
    }
}