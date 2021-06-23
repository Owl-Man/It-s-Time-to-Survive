using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;

    public LinkManager links;
    private Transform player;

    public void SpawnDroppedItem()
    {
        player = links.PlayerObject.transform;

        float randX = UnityEngine.Random.Range(-2.7f, 2.7f);
        float randY = UnityEngine.Random.Range(2f, 3.2f);

        Vector2 playerPos = new Vector2(player.position.x + randX, player.position.y + randY);
        GameObject SpawnedItem = Instantiate(item, playerPos, Quaternion.identity);

        SpawnedItem.GetComponent<PickUp>().links = links;
    }
}
