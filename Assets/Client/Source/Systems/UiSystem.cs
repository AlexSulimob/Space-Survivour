using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client {
    sealed class UiSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsSharedInject<Shared> _shared = default;

        EcsPool<EcsUguiClickEvent> _clickEventsPool;
        EcsFilter _clickEvents;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _clickEventsPool = world.GetPool<EcsUguiClickEvent>();
            _clickEvents = world.Filter<EcsUguiClickEvent>().End();
        }

        public void Run (IEcsSystems systems) {
            _shared.Value.ui.expUI.progressBar.minimum = _shared.Value.runtimeDataService.PreviousLevelExp;
            _shared.Value.ui.expUI.progressBar.maximum = _shared.Value.runtimeDataService.NextLevelExp;
            _shared.Value.ui.expUI.progressBar.current = _shared.Value.runtimeDataService.Exp;

            _shared.Value.ui.healthUI.healthValue.text = _shared.Value.runtimeDataService.Health.ToString();

            var ts = TimeSpan.FromSeconds(_shared.Value.runtimeDataService.GameTime);
            _shared.Value.ui.timerUI.timerValue.text = ts.ToString("mm':'ss");
            //_shared.Value.ui.timerUI.timerValue.text = _shared.Value.playerService.GameTime.ToString();
            _shared.Value.ui.killsUi.killValue.text = _shared.Value.runtimeDataService.Kills.ToString();

            //_shared.Value.ui.timerUI.timerValue.text = TimeSpan.FromSeconds(_shared.Value.playerService.GameTime).ToString("mm\:ss");
            _shared.Value.ui.expUI.CurrentLevelValue.text = String.Concat("Lv. " ,(_shared.Value.runtimeDataService.CurrentLevel + 1).ToString());
            //_shared.Value.ui.expUI.nextLevelExpValue.text = _shared.Value.playerService.NextLevelExp.ToString();



            foreach (var entity in _clickEvents)
            {
                ref EcsUguiClickEvent data = ref _clickEventsPool.Get(entity);
                if (data.WidgetName == "SettingBtn")
                {
                    _shared.Value.ui.settingsUi.uiAnimations.ScaleIn();
                }
                if (data.WidgetName == "SettingBack")
                {
                    _shared.Value.ui.settingsUi.uiAnimations.ScaleOut();
                }
 
            }
            

        }
    }
}