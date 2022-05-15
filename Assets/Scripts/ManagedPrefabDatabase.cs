using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(order = 0, fileName = "PrefabDatabase", menuName = "Create Prefab DB")]
public class ManagedPrefabDatabase : ScriptableObject
{
    public ManagedPrefab[] prefabs;
}

[Serializable]
public class ManagedPrefab
{
    public string prefabName;
    public GameObject prefabGameObject;
}