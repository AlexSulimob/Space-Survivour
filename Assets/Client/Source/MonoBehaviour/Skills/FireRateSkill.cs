namespace Client
{
    public class FireRateSkill : ISkill
    {
        public SkillInfo SkillInfo { get; set; }
        public int СurrentLevel { get; set; }

        public float CurrentFireRate { get; set; }

        public float levelUpShootingRate { get; set; }

        public FireRateSkill(SkillInfo SkillInfo, float CurrentFireRate, float levelUpShootingRate , int CurrentLevel = 1)
        {
            this.SkillInfo = SkillInfo;
            this.CurrentFireRate = CurrentFireRate;
            this.СurrentLevel = CurrentLevel;
            this.levelUpShootingRate = levelUpShootingRate;
        }

        public void LevelUp()
        {
            СurrentLevel += 1;
            if (CurrentFireRate - levelUpShootingRate <= 0)
            {
                CurrentFireRate = CurrentFireRate / 2f;
            }
            else
            {
                CurrentFireRate -= levelUpShootingRate;
            }
        }
    }
}   