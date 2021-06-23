using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    private Transform player;
    private int lastX;

    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float upperLimit;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);

            if (currentX > lastX)
            {
                isLeft = false;
            }
            else if (currentX < lastX)
            {
                isLeft = true;
            }

            lastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;

            if (isLeft) target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);

            else target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);

            transform.position = currentPosition;
        }
        
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            transform.position.z
        );
    }
    
    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        lastX = Mathf.RoundToInt(player.position.x);

        if (playerIsLeft)
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        else
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
