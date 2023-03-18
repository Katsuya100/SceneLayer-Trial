using Katuusagi.SceneLayer;
using UnityEngine;

public class CheckTutorial  : TransitionBehaviour
{
    private static bool _masterLookedTutorial = false;

    [SerializeField]
    private bool _isLookedTutorial = false;

    protected override bool IsTransitable()
    {
        var result = _isLookedTutorial == _masterLookedTutorial;
        _masterLookedTutorial = true;
        return result;
    }
}
