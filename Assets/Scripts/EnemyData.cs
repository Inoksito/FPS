using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data",menuName = "Enemy/Data")]

public class EnemyData : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private string description;
    [SerializeField] private float speed;
    [SerializeField] private float shootFrecuency;
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private int maxLife;

    public string EnemyName { get => enemyName; }
    public string Description { get => description; }
    public float Speed { get => speed; }
    public float ShootFrecuency { get => shootFrecuency;  }
    public Material EnemyMaterial { get => enemyMaterial; }
    public int MaxLife { get => maxLife; }
}
