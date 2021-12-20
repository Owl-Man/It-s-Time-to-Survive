using System.Collections;
using UnityEngine;

public class RedMoonWave : SpawnBase
{
    public Transform[] points;

    public void RedMoonStart() => StartCoroutine(Spawner());

    private IEnumerator Spawner()
    {
        if (PlayerPrefs.GetInt("isRedMoonDay") == 1)
        {
            yield return new WaitForSeconds(time);

            Wave(Random.Range(3, 20));

            StartCoroutine(Spawner());
        }
    }

    private void Wave(int count) 
    {
        for (int i = 0; i <= count; i++)
        {
            Spawn(points[Random.Range(1, points.Length)]);
        }
    }
}