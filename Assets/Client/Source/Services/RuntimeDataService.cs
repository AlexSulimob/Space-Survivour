using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class RuntimeDataService : MonoBehaviour
    {
        public PlayerSettings playerSettings;
        public Transform player;
        public DiffucultyDic diffucultyDic;
        [HideInInspector] public DiffucultySettings currentDiffuculty;
        [HideInInspector] public float nextDiffucultyTime;
        [HideInInspector] public int currentDiffucultyIndex;
        public float CurrentPlayerSpeed { get; set; }
        //public float CurrentFireRate { get; set; }
        public float CurrentProjectileSpeed { get; set; }

        public float SmallEnemySpawnTime { get; set; }
        public float MediumEnemySpawnTime { get; set; }
        public float BigEnemySpawnTime { get; set; }

        public int CurrentLevel { get; set; }
        public int NextLevelExp { get; set; }
        public int PreviousLevelExp { get; set; }

        public int Exp { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Kills { get; set; }
        public bool IsPaused { get; set; }
        public bool IsLevelingUP { get; set; }
        public float GameTime { get; set; }

        public SkillInfo CountofBaseWeaponInfo;
        public SkillInfo thunderSkillInfo;
        public SkillInfo cannonBallSkillInfo;
        public SkillInfo ufoSkillInfo;
        public SkillInfo fireRateSkillInfo;

        public List<ISkill> skillList = new List<ISkill>();
        public ThunderSkill thunderSkill;
        public CannonBallSkill cannonBallSkill;
        public UfoSkill ufoSkill;
        public CountOfBaseWeaponSkill countOfBaseWeapon;
        public FireRateSkill fireRateSkill;
        /*
         * list<enemy> 
         */

        public void Start()
        {

            thunderSkill = new ThunderSkill(thunderSkillInfo, playerSettings.levelUpthunderSpawnCount, playerSettings.levelUpthunderCd, 0);
            cannonBallSkill = new CannonBallSkill(cannonBallSkillInfo, playerSettings.levelUpCannonBallSpawnCount, 0, playerSettings.cannonBallCd);
            ufoSkill = new UfoSkill(ufoSkillInfo, playerSettings.levelUpUfoSpeed);
            var weapons = player.gameObject.GetComponentInChildren<PlayerWeapons>();
            countOfBaseWeapon = new CountOfBaseWeaponSkill(CountofBaseWeaponInfo, 1, weapons.weaponsTransforms.Length);
            fireRateSkill = new FireRateSkill(fireRateSkillInfo, playerSettings.initShootingRate, playerSettings.levelUpShootingRate,1);
            skillList.Add(thunderSkill);
            skillList.Add(cannonBallSkill);
            skillList.Add(ufoSkill);
            skillList.Add(countOfBaseWeapon);
            skillList.Add(fireRateSkill);


        }
    }
}
