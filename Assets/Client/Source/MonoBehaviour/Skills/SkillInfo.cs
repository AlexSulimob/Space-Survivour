using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    [CreateAssetMenu(fileName ="Create Skill Info")]
    public class SkillInfo: ScriptableObject
    {
        public string Name;
        [TextArea(10, 100)]
        public string Description;
        public Sprite ico;
    }
}   