using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolingManager : MonoBehaviour
{
    public ManagedPrefabDatabase prefabDatabase;

    private Dictionary<string, GameObject> prefabDic;

    private Dictionary<string, List<GameObject>> managedObjects;

    private void Awake()
    {
        prefabDic = new Dictionary<string, GameObject>();
        managedObjects = new Dictionary<string, List<GameObject>>();

        foreach (var managedPrefab in prefabDatabase.prefabs)
        {
            prefabDic.Add(managedPrefab.prefabName, managedPrefab.prefabGameObject);
        }

    }

    public GameObject Get(string gameObjectName)
    {
        if (!prefabDic.ContainsKey(gameObjectName))
            return null;
        else
        {
            if (!managedObjects.ContainsKey(gameObjectName))
                managedObjects.Add(gameObjectName, new List<GameObject>());

            if (managedObjects[gameObjectName].Any(obj => !obj.activeInHierarchy))
            {
                var possibleObject = managedObjects[gameObjectName].FirstOrDefault(obj => !obj.activeInHierarchy);
                possibleObject.SetActive(true);

                return possibleObject;
            }
            else
            {
                var newObject = Instantiate(prefabDic[gameObjectName]);
                managedObjects[gameObjectName].Add(newObject);

                return newObject;
            }
        }
    }

    public GameObject Get(string gameObjectName, Vector3 position, Quaternion rotation)
    {
        var go = Get(gameObjectName);
        go.transform.position = position;
        go.transform.rotation = rotation;

        return go;
    }
}