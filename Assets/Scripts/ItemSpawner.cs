using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject item;

    public float time;
    public Transform transform;

    private void Start() 
    {
        StartCoroutine(Spawner());   
    }
    public void Spawn() 
    {
    	float randX = UnityEngine.Random.Range(-2.7f, 2.7f);
    	float randY = UnityEngine.Random.Range(2f, 3.2f);

        Vector2 NewPosition = new Vector2(transform.position.x + randX, transform.position.y + randY);

    	Instantiate(item, NewPosition, Quaternion.identity);
    }


    IEnumerator Spawner() 
    {
        yield return new WaitForSeconds(time);
        Spawn();
        StartCoroutine(Spawner());
    }
}
