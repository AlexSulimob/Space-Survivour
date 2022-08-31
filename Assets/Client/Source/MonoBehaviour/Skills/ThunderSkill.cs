namespace Client
{
    public class ThunderSkill : ISkill
    {
        public SkillInfo SkillInfo { get; set; }
        public int СurrentLevel { get; set; }

        public float cd;
        public int ThunderSpawnCount;
        public float ThunderSpawnTime;

        public int levelUpThunderSpawnCount;
        public float levelUpthunderCd;
        public ThunderSkill(SkillInfo SkillInfo, int levelUpThunderSpawnCount, float levelUpthunderCd, int CurrentLevel = 1)
        {
            cd = 8f;
            this.SkillInfo = SkillInfo;
            this.levelUpThunderSpawnCount = levelUpThunderSpawnCount;
            this.СurrentLevel = CurrentLevel;
            this.levelUpthunderCd = levelUpthunderCd;
        }

        public void LevelUp()
        {
            ThunderSpawnCount += levelUpThunderSpawnCount;
            cd /= levelUpthunderCd;
            СurrentLevel += 1;
        }
    }

}