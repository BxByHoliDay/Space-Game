using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drop Item")]
public class ItemDrop : ScriptableObject
{
    public GameObject prefab;
    public int score;
    [Range(0f, 1f)]
    public float dropChance;
}
