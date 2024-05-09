using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Blocker_spawner : MonoBehaviour
{
    public GameObject cam1;
    public GameObject Blocker;
    public Vector3 offset;
    public int starttime;
    public int waittime;
    public bool spawn = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (cam1 != null)
        {
            StartCoroutine(Blocker_spawner_spawn());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (cam1.transform.position.z > 5)
        {
            spawn = true;
            this.transform.position = cam1.transform.position + offset;
        }
    }

    public IEnumerator Blocker_spawner_spawn()
    {
        yield return new WaitForSeconds(starttime);
        while (spawn == true)
        {
            Vector3 randomSpawnPosition = new Vector3(UnityEngine.Random.Range(-3.9f, 3.9f), 0.5f, UnityEngine.Random.Range(transform.position.z, transform.position.z + 30));
            Instantiate(Blocker, randomSpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(waittime);
        }
    }
}