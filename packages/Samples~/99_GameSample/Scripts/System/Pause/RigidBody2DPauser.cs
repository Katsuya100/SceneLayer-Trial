using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidBody2DPauser : MonoBehaviour, IPausable
{
    private Rigidbody2D _rigidBody = null;
    private Rigidbody2D RigidBody
    {
        get
        {
            if (_rigidBody == null)
            {
                _rigidBody = GetComponent<Rigidbody2D>();
            }

            return _rigidBody;
        }
    }

    public bool IsPaused => !_rigidBody.simulated;

    public void Pause()
    {
        RigidBody.simulated = false;
    }

    public void Resume()
    {
        RigidBody.simulated = true;
    }
}
