using System.Collections;
using UnityEngine;

public abstract class SpawnBase : MonoBehaviour 
{
    public GameObject[] item;

    public float time;

    public Transform transform;

    public void Spawn(Transform transform) 
    {
    	float randX = UnityEngine.Random.Range(-5f, 5f);
    	float randY = UnityEngine.Random.Range(-4f, 4f);
        int randObj = UnityEngine.Random.Range(0, item.Length);

        Vector2 NewPosition = new Vector2(transform.position.x + randX, transform.position.y + randY);

        Instantiate(item[randObj], NewPosition, Quaternion.identity);
    }
}