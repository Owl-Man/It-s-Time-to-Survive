using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
    	player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem() 
    {
    	Vector2 playerPos = new Vector2(player.position.x + 1, player.position.y + 2);
    	Instantiate(item, playerPos, Quaternion.identity);
    	//item.GetComponent<Rigidbody2D>().MovePosition(playerPos);
    }
}
