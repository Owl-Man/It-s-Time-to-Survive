using UnityEngine;

public abstract class SpawnBase : MonoBehaviour 
{
    public GameObject[] item;

    public float time;

    public Transform transform;

    public void Spawn(Transform transform) 
    {
    	float randX = Random.Range(-5f, 5f);
    	float randY = Random.Range(-4f, 4f);
        int randObj = Random.Range(0, item.Length);

        Vector2 newPosition = new Vector2(transform.position.x + randX, transform.position.y + randY);

        Instantiate(item[randObj], newPosition, Quaternion.identity);
    }
}