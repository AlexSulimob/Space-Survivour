using Leopotam.EcsLite;

namespace Client
{
    public interface ISkill
    {
        public SkillInfo SkillInfo { get; set; }
        public int ŅurrentLevel { get; set; }
        public void LevelUp();
    }
}   