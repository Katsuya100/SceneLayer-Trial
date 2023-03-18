using Katuusagi.SceneLayer;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCheck  : TransitionBehaviour
{
    [SerializeField]
    private CrossReference<InputField> _passwordInput;

    [SerializeField]
    private string _password;

    protected override bool IsTransitable()
    {
        return _passwordInput.Instance.text == _password;
    }
}
