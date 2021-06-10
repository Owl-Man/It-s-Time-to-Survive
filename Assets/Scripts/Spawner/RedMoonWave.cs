using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RedMoonWave : SpawnBase
{
    public Transform[] points;
    public void RedMoonStart() => StartCoroutine(Spawner());
    
    IEnumerator Spawner()
    {
        if (PlayerPrefs.GetInt("isRedMoonDay") == 1)
        {
            yield return new WaitForSeconds(time);

            int RandCount = UnityEngine.Random.Range(3, 20);
            Wave(RandCount);

            StartCoroutine(Spawner());
        }
    }

    private void Wave(int count) 
    {
        for (int i = 0; i <= count; i++)
        {
            int RandPoint = UnityEngine.Random.Range(1, points.Length);
            Spawn(points[RandPoint]);
        }
    }
}