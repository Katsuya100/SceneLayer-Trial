using Katuusagi.SceneLayer;
using UnityEngine;

public class InputButtonDown : TransitionBehaviour
{
    [SerializeField]
    private string _keyCode;

    protected override bool IsTransitable()
    {
        return Input.GetButtonDown(_keyCode);
    }
}
