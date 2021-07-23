using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float dumping = 1.5f;
    [SerializeField] private Vector2 offset = new Vector2(2f, 1f);
    [SerializeField] private bool isLeft;

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float upperLimit;

    private Transform player;

    private Vector3 target;

    private int lastX, currentX;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FocusOnPlayer(isLeft);
    }

    void Update()
    {
        if (player)
        {
            currentX = Mathf.RoundToInt(player.position.x);

            if (currentX > lastX) isLeft = false;
            else if (currentX < lastX) isLeft = true;

            lastX = Mathf.RoundToInt(player.position.x);

            target = isLeft
                ? new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z)
                : new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

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
    
    public void FocusOnPlayer(bool playerIsLeft)
    {
        player = LinkManager.instance.PlayerObject.transform;

        lastX = Mathf.RoundToInt(player.position.x);

        transform.position = playerIsLeft
            ? new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z)
            : new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
