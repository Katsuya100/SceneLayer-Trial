using UnityEngine;

public class MoveObject : MonoBehaviour, IPausable
{
    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    private void FixedUpdate()
    {
        if (_isPaused)
        {
            return;
        }

        var pos = transform.position;
        pos.x -= _speed / 30.0f;
        _rigidbody.MovePosition(pos);
    }
}
