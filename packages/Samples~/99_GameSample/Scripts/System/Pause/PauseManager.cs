using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PauseManager
{
    public static PauseHandle Pause(GameObject findRoot)
    {
        var pausableComponents = findRoot.GetComponentsInChildren<IPausable>();
        return new PauseHandle(pausableComponents);
    }

    public static PauseHandle Pause(IEnumerable<IPausable> pausable)
    {
        return new PauseHandle(pausable);
    }

    public static PauseHandle Pause(GameObject findRoot, IEnumerable<IPausable> pausable)
    {
        var pausableComponents = findRoot.GetComponentsInChildren<IPausable>();
        return Pause(pausable.Concat(pausableComponents));
    }

    public static PauseHandle Pause(GameObject findRoot, params IPausable[] pausable)
    {
        return Pause(findRoot, pausable as IEnumerable<IPausable>);
    }
}
