using Katuusagi.SceneLayer;
using UnityEngine;

public class SceneAButtonReceiver : MonoBehaviour
{
    public void OnClickScreen()
    {
        Trigger.Set("ToB");
    }
}
