using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : SpawnBase
{
    private void Start() => StartCoroutine(Spawner());

    IEnumerator Spawner() 
    {
        yield return new WaitForSeconds(time);
        Spawn(transform);
        StartCoroutine(Spawner());
    }
}
