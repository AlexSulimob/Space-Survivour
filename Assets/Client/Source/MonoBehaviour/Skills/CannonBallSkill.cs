namespace Client
{
    public class CannonBallSkill : ISkill
    {
        public SkillInfo SkillInfo { get; set; }
        public int СurrentLevel { get; set; }

        public int CannonBallSpawnCount { get; set; }
        public float CannonBallCd { get; set; }
        public float CannonBallSpawnTime { get; set; }

        public int levelUpCannonBallSpawnCount;

        public CannonBallSkill(SkillInfo SkillInfo, int levelUpCannonBallSpawnCount, int CurrentLevel = 1, float cannonBallCd = 12f)
        {
            this.SkillInfo = SkillInfo;
            this.levelUpCannonBallSpawnCount = levelUpCannonBallSpawnCount;
            this.СurrentLevel = CurrentLevel;
            CannonBallCd = cannonBallCd;
        }

        public void LevelUp()
        {
            CannonBallSpawnCount += levelUpCannonBallSpawnCount;
        }
    }
}   