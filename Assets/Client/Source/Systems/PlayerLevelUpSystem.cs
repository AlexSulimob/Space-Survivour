using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Client {
    sealed class PlayerLevelUpSystem :IEcsRunSystem, IEcsInitSystem {

        readonly EcsSharedInject<Shared> _shared = default;

        EcsPool<EcsUguiClickEvent> _clickEventsPool;
        EcsFilter _clickEvents;

        Dictionary<string, ISkill> currentLevelUp = new Dictionary<string, ISkill>();
        ISkill[] currentSkillUp = new ISkill[2];
        public void Init(IEcsSystems systems)
        {
            _shared.Value.runtimeDataService.PreviousLevelExp = 0;
            _shared.Value.runtimeDataService.CurrentLevel = 0;
            _shared.Value.runtimeDataService.NextLevelExp = _shared.Value.playerSettings.LevelingSettings[0];

            var world = systems.GetWorld();

            _clickEventsPool = world.GetPool<EcsUguiClickEvent>();
            _clickEvents = world.Filter<EcsUguiClickEvent>().End();
        }

        public void Run(IEcsSystems systems) {

            var ecsWorld = systems.GetWorld();
            var ufoPool = ecsWorld.GetPool<UfoComponent>();
            var exp = _shared.Value.runtimeDataService.Exp;
            var nextLevelExp = _shared.Value.runtimeDataService.NextLevelExp;
            bool isLeveledUp = exp >= nextLevelExp;

            if (isLeveledUp)
            {
                
                _shared.Value.runtimeDataService.PreviousLevelExp = _shared.Value.runtimeDataService.NextLevelExp;
                _shared.Value.runtimeDataService.CurrentLevel++;
                _shared.Value.runtimeDataService.NextLevelExp = _shared.Value.playerSettings.LevelingSettings[_shared.Value.runtimeDataService.CurrentLevel];

                _shared.Value.runtimeDataService.IsPaused = !_shared.Value.runtimeDataService.IsPaused;
                _shared.Value.ui.LevelUpUI.uiAnimations.ScaleIn();
                
                var levelUpsLenght = _shared.Value.runtimeDataService.skillList.Count;
                var levelUpIndex1 = Random.Range(0, levelUpsLenght);
                var levelUpIndex2 = Random.Range(0, levelUpsLenght);
                while(levelUpIndex1 == levelUpIndex2) 
                {
                    levelUpIndex2 = Random.Range(0, levelUpsLenght);
                }
                _shared.Value.ui.LevelUpUI.slot1.SkillName.text = _shared.Value.runtimeDataService.skillList[levelUpIndex1].SkillInfo.Name;
                _shared.Value.ui.LevelUpUI.slot1.Descrition.text = _shared.Value.runtimeDataService.skillList[levelUpIndex1].SkillInfo.Description;
                _shared.Value.ui.LevelUpUI.slot1.icon.sprite = _shared.Value.runtimeDataService.skillList[levelUpIndex1].SkillInfo.ico;


                _shared.Value.ui.LevelUpUI.slot2.SkillName.text = _shared.Value.runtimeDataService.skillList[levelUpIndex2].SkillInfo.Name;
                _shared.Value.ui.LevelUpUI.slot2.Descrition.text = _shared.Value.runtimeDataService.skillList[levelUpIndex2].SkillInfo.Description;
                _shared.Value.ui.LevelUpUI.slot2.icon.sprite = _shared.Value.runtimeDataService.skillList[levelUpIndex2].SkillInfo.ico;


                currentSkillUp[0] = _shared.Value.runtimeDataService.skillList[levelUpIndex1];
                currentSkillUp[1] = _shared.Value.runtimeDataService.skillList[levelUpIndex2];

            }
            
            foreach (var entity in _clickEvents)
            {
                ref EcsUguiClickEvent data = ref _clickEventsPool.Get(entity);

                switch (data.WidgetName)
                {
                    case "slot1":
                        currentSkillUp[0].LevelUp();
                        break;
                    case "slot2":
                        currentSkillUp[1].LevelUp();
                        break;                 
                    default:
                        continue;

                }
                _shared.Value.runtimeDataService.IsPaused = !_shared.Value.runtimeDataService.IsPaused;
                _shared.Value.ui.LevelUpUI.uiAnimations.ScaleOut();
            }

        }

    }
}