namespace Client
{
    public class CountOfBaseWeaponSkill : ISkill
    {
        public SkillInfo SkillInfo { get; set; }
        public int СurrentLevel { get; set; }

        public int CurrentWeapons { get; set; }
        public int MaxWeapons { get; set; }


        public CountOfBaseWeaponSkill(SkillInfo SkillInfo, int CurrentWeapons, int MaxWeapons, int CurrentLevel = 1)
        {
            this.SkillInfo = SkillInfo;
            this.CurrentWeapons = CurrentWeapons;
            this.СurrentLevel = CurrentLevel;
            this.MaxWeapons = MaxWeapons;
        }

        public void LevelUp()
        {
            if (CurrentWeapons != MaxWeapons)
                CurrentWeapons += 1;
        }
    }
}   