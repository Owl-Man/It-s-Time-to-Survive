using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;

    private Transform player;

    public void SpawnDroppedItem()
    {
        player = LinkManager.instance.PlayerObject.transform;

        float randX = Random.Range(-2.7f, 2.7f);
        float randY = Random.Range(2f, 3.2f);

        Vector2 playerPos = new Vector2(player.position.x + randX, player.position.y + randY);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
