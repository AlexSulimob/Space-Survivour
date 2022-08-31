using System;

namespace Client
{
    public class UfoSkill : ISkill
    {
        public int СurrentLevel { get; set; }

        public float UfoSpeed { get; set; }
        public bool UfoSpawned = false;
        public float levelUpUfoSpeed { get; set; }
        public SkillInfo SkillInfo { get; set; }

        public event Action createUfo;

        public UfoSkill(SkillInfo SkillInfo, float levelUpUfoSpeed, int CurrentLevel = 0)
        {
            this.SkillInfo = SkillInfo;
            this.levelUpUfoSpeed = levelUpUfoSpeed;
            this.СurrentLevel = CurrentLevel;

        }

        public void LevelUp()
        {
            
            if (!UfoSpawned)
            {
                createUfo?.Invoke();

                UfoSpawned = true;
            }
            UfoSpeed += levelUpUfoSpeed;
            
        }
    }
}   