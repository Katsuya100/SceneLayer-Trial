using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorPauser : MonoBehaviour, IPausable
{
    private Animator _animator = null;
    private Animator Animator
    {
        get
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            return _animator;
        }
    }

    private float _tmpSpeed = 0.0f;

    public bool IsPaused => Animator.speed == 0;

    public void Pause()
    {
        if (IsPaused)
        {
            return;
        }

        _tmpSpeed = Animator.speed;
        Animator.speed = 0;
    }

    public void Resume()
    {
        if (!IsPaused)
        {
            return;
        }

        Animator.speed = _tmpSpeed;
    }
}
