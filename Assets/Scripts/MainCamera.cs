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

    private Transform _player;

    private Vector3 _target;

    private int _lastX, _currentX;

    private void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FocusOnPlayer(isLeft);
    }

    private void Update()
    {
        if (_player)
        {
            _currentX = Mathf.RoundToInt(_player.position.x);

            if (_currentX > _lastX) isLeft = false;
            else if (_currentX < _lastX) isLeft = true;

            _lastX = Mathf.RoundToInt(_player.position.x);

            _target = isLeft
                ? new Vector3(_player.position.x - offset.x, _player.position.y + offset.y, transform.position.z)
                : new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);

            Vector3 currentPosition = Vector3.Lerp(transform.position, _target, dumping * Time.deltaTime);

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
        _player = LinkManager.instance.PlayerObject.transform;

        _lastX = Mathf.RoundToInt(_player.position.x);

        transform.position = playerIsLeft
            ? new Vector3(_player.position.x - offset.x, _player.position.y - offset.y, transform.position.z)
            : new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);
    }
}
