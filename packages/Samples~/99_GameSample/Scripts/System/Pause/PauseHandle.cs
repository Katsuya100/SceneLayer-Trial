using System;
using System.Collections.Generic;

public class PauseHandle : IDisposable
{
    private IEnumerable<IPausable> _targets;
    public PauseHandle(IEnumerable<IPausable> targets)
    {
        foreach (var target in targets)
        {
            if (target == null ||
                target is UnityEngine.Object unityTarget && unityTarget == null)
            {
                return;
            }

            target.Pause();
        }

        _targets = targets;
    }

    public void Resume()
    {
        ((IDisposable)this).Dispose();
    }

    void IDisposable.Dispose()
    {
        foreach (var target in _targets)
        {
            if (target == null ||
                target is UnityEngine.Object unityTarget && unityTarget == null)
            {
                return;
            }

            target.Resume();
        }
    }
}
