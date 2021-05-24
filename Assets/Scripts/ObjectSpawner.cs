using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] item;
    public float time;
    public Transform transform;

    private void Start() => StartCoroutine(Spawner());

    public void Spawn() 
    {
    	float randX = UnityEngine.Random.Range(-5f, 5f);
    	float randY = UnityEngine.Random.Range(-4f, 4f);
        int randObj = UnityEngine.Random.Range(0, item.Length);

        Vector2 NewPosition = new Vector2(transform.position.x + randX, transform.position.y + randY);

    	Instantiate(item[randObj], NewPosition, Quaternion.identity);
    }


    IEnumerator Spawner() 
    {
        yield return new WaitForSeconds(time);
        Spawn();
        StartCoroutine(Spawner());
    }
}
