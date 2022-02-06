using Instruments;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject item;

    private Vector3 _player;

    public void SpawnDroppedItem()
    {
        _player = LinkManager.Instance.playerObject.transform.position;

        float randX = Random.Range(-2.7f, 2.7f);
        float randY = Random.Range(2f, 3.2f);

        Vector2 spawnPos = new Vector2(_player.x + randX, _player.y + randY);
        Instantiate(item, spawnPos, Quaternion.identity);
    }
}
