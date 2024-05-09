using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Blocker2_spawner : MonoBehaviour
{
    public GameObject cam1;
    public GameObject Blocker2;
    public Vector3 offset;
    public int starttime;
    public int waittime;
    public bool spawn = false;
    private bool start;

    // Update is called once per frame
    private void Update()
    {
        if (cam1.transform.position.z > 5)
        {
            spawn = true;
            this.transform.position = cam1.transform.position + offset;
        }
        if (cam1.transform.position.z > 1500 && !start)
        {
            StartCoroutine(Blocker2_spawner_spawn());
            start = true;
        }
    }

    public IEnumerator Blocker2_spawner_spawn()
    {
        yield return new WaitForSeconds(starttime);
        while (spawn == true)
        {
            Vector3 randomSpawnPosition = new Vector3(UnityEngine.Random.Range(-3.9f, 3.9f), 0.5f, UnityEngine.Random.Range(transform.position.z, transform.position.z + 30));
            Instantiate(Blocker2, randomSpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(waittime);
        }
    }
}