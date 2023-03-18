using Katuusagi.SceneLayer;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ResourcesPrefabPoolLoader : LoaderBehaviour
{
    [SerializeField]
    private uint _poolCount = 0;

    [SerializeField, ResourcesProperty(typeof(GameObject))]
    private string _prefab = string.Empty;

    private GameObject _prefabObject = null;

    private GameObject _root;

    private GameObject[] _instances = null;

    public GameObject Get(Transform parent = null)
    {
        var instance = _instances.FirstOrDefault(v => !v.activeSelf);
        instance.SetActive(true);
        instance.transform.SetParent(parent);
        return instance;
    }

    public void Return(GameObject instance)
    {
        instance.transform.SetParent(_root.transform);
        instance.SetActive(false);
    }

    public override object GetInstance()
    {
        return _instances;
    }

    public override Type GetInstanceType()
    {
        return typeof(GameObject[]);
    }

    protected override IEnumerator Load()
    {
        var op = Resources.LoadAsync<GameObject>(_prefab);
        yield return op;
        _prefabObject = op.asset as GameObject;
    }

    protected override IEnumerator InstantiateObject()
    {
        _root = new GameObject("Pool");
        _instances = new GameObject[_poolCount];

        for (int i = 0; i <  _instances.Length; ++i)
        {
            _instances[i] = GameObject.Instantiate(_prefabObject, _root.transform);
            _instances[i].SetActive(false);
        }

        yield break;
    }

    protected override IEnumerator DestroyObject()
    {
        for (int i = 0; i < _instances.Length; ++i)
        {
            GameObject.Destroy(_instances[i]);
        }

        GameObject.Destroy(_root);

        yield break;
    }

    protected override IEnumerator Unload()
    {
        yield return Resources.UnloadUnusedAssets();
    }
}
