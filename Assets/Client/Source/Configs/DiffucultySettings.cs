using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Diffuculty")]
public class DiffucultySettings : ScriptableObject
{
    public EnemyStats smallEnemy;
    public EnemyStats medimumEnemy;
    public EnemyStats bigEnemy;

    [Range(0,100)]
    public int HealthDropPercent;
 
}
[System.Serializable]
public struct EnemyStats
{
    public float EnemySpawnRate;
    public float EnemySpeed;
    public int EnemyHp;
    public EnemyTypes enemyType;
}