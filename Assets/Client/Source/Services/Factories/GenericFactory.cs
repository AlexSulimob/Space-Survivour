using Client;
using System;
using ToolBox.Pools;
using UnityEngine;

public interface ICommandGetNewInstance
{
    public abstract GameObject CommandGetNewInstance(Vector2 pos);
}
public class GenericFactory<T> : ICommandGetNewInstance where T: Component
{
    private T _prefab;
    private Transform _pointToSpawn;
    public GenericFactory(string prefabsResourcesPath)
    {
        _prefab = Resources.Load<T>(prefabsResourcesPath);
    }
    public GameObject CommandGetNewInstance(Vector2 pos)
    {
        return _prefab.gameObject.Reuse(pos, Quaternion.identity);
    }
    public Type GetPrefabType()
    {
        return _prefab.GetType();
    }
    public T GetNewInstance()
    {
        Vector3 pos = new Vector3(_pointToSpawn.position.x, _pointToSpawn.position.y, 0f);

        return _prefab.gameObject.Reuse<T>(pos, Quaternion.identity);
    }
    public T GetNewInstance(Vector2 pos)
    {
        return _prefab.gameObject.Reuse<T>(pos, Quaternion.identity);
    }
    public T GetNewInstance(Vector2 pos, Quaternion quaternion)
    {
        return _prefab.gameObject.Reuse<T>(pos, quaternion);
    }
    public void ReleaseInstance(GameObject go)
    {
        go.Release();
    }


}


