using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableObject", menuName = "Sciptable Objects/Spawnable Object")]
public class SpawnableObjectsSO : ScriptableObject
{
    public List<GameObject> prefabs;
}
