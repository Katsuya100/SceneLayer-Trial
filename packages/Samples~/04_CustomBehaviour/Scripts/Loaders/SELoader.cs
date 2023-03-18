using Katuusagi.SceneLayer;
using System;
using System.Collections;
using UnityEngine;

public class SELoader : LoaderBehaviour
{
    [SerializeField, ResourcesProperty(typeof(AudioClip))]
    private string _se;

    private AudioClip _seClip = null;
    private AudioSource _source = null;

    public override object GetInstance()
    {
        return _seClip;
    }

    public override Type GetInstanceType()
    {
        return typeof(AudioClip);
    }

    protected override IEnumerator Load()
    {
        var req = Resources.LoadAsync<AudioClip>(_se);
        yield return req;
        _seClip = req.asset as AudioClip;
    }

    protected override IEnumerator InstantiateObject()
    {
        var obj = new GameObject("AudioSource");
        _source = obj.AddComponent<AudioSource>();
        _source.clip = _seClip;
        _source.Play();
        yield break;
    }

    protected override IEnumerator DestroyObject()
    {
        Destroy(_source.gameObject);
        _source = null;
        yield break;
    }

    protected override IEnumerator Unload()
    {
        Resources.UnloadAsset(_seClip);
        _seClip = null;
        yield break;
    }
}
