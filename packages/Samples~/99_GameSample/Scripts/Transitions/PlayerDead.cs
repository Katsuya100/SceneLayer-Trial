using Katuusagi.SceneLayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead  : TransitionBehaviour
{
    [SerializeField]
    private CrossReference<MoveCharacterAction> _player;

    [SerializeField]
    private float _time = 3;

    protected override bool IsTransitable()
    {
        return _player.Instance.IsDead;
    }

    protected override IEnumerator OnPreTransit()
    {
        yield return new WaitForSeconds(_time);
    }
}
