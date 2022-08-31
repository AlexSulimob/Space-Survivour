using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerConfig")]
public class PlayerSettings : ScriptableObject
{
    public float speed;
    public float initShootingRate;
    public float projectileSpeed;
    public float initUfoSpeed = 250f;
    public float thunderCd = 2f;
    public float cannonBallCd = 2f;

    public float levelUpProjectileSpeed;
    public float levelUpShootingRate;
    public float levelUpSpeed;
    public int levelUpMaxHealth;
    public int levelUpthunderSpawnCount = 1;
    public float levelUpthunderCd = 1;
    public int levelUpCannonBallSpawnCount = 1;
    public float levelUpUfoSpeed = 50f;

    public int playerStartMaxHealth;
    public int[] LevelingSettings;

    public int expForSmallEnemy = 5;
    public int expForMiddleEnemy = 10;
    public int expForBigEnemy = 15;

    public float expSpeed = 2f;
    public float HealthSpeed = 2f;

}
