using Katuusagi.SceneLayer;
using UnityEngine;

public class MoveCharacterAction : MonoBehaviour, IPausable
{
    private static int Speed = Animator.StringToHash ("Speed");
    private static int FallSpeed = Animator.StringToHash ("FallSpeed");
    private static int GroundDistance = Animator.StringToHash ("GroundDistance");
    private static int Dead = Animator.StringToHash ("Dead");

    [SerializeField]
    private LayerMask _groundMask;
    
    [SerializeField]
    private Animator _animator;
    
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private CrossReference<Camera> _worldCamera;

    private bool _isDead = false;
    public bool IsDead => _isDead;

    private Rect _worldRect = Rect.zero;

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    private void Start()
    {
        _rigidBody.gravityScale = 0.3f;
        var min = _worldCamera.Instance.ScreenToWorldPoint(Vector2.zero);
        var max = _worldCamera.Instance.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        _worldRect.xMin = min.x;
        _worldRect.xMax = max.x;
        _worldRect.yMin = min.y;
        _worldRect.yMax = max.y;
    }

    private void Update()
    {
        if (_isDead || _isPaused)
        {
            return;
        }

        float axis = Input.GetAxis ("Horizontal");
        Vector2 velocity = _rigidBody.velocity;
        if (_rigidBody.velocity.y <= 0.1f &&
            Input.GetButtonDown("Jump"))
        {
            velocity.y = 2;
        }
        if (axis != 0)
        {
            _spriteRenderer.flipX = axis < 0;
            velocity.x = axis * 2;
        }

        _rigidBody.velocity = velocity;
        _animator.SetFloat(Speed, Mathf.Abs(axis));
    }


    private void LateUpdate()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, _worldRect.xMin, _worldRect.xMax);
        pos.y = Mathf.Clamp(pos.y, _worldRect.yMin, _worldRect.yMax);
        transform.position = pos;

        var distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down, 1, _groundMask);
        _animator.SetFloat(GroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - 0.2f);
        _animator.SetFloat(FallSpeed, _rigidBody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _isDead = true;

        if (other.CompareTag("DamageObject"))
        {
            _animator.SetTrigger(Dead);
        }
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }
}
