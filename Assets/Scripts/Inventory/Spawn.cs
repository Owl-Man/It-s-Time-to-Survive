using UnityEngine;

public class Spawn : LinkManager
{
    public GameObject item;
    private Transform player;

    private void Start() => player = ManagerPlayerObject.transform;

    public void SpawnDroppedItem()
    {
        float randX = UnityEngine.Random.Range(-2.7f, 2.7f);
        float randY = UnityEngine.Random.Range(2f, 3.2f);

        Vector2 playerPos = new Vector2(player.position.x + randX, player.position.y + randY);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
