using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;


namespace Client {
    sealed class PlayerDeathSystem : IEcsRunSystem, IEcsInitSystem {
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
            var ecsWorld = systems.GetWorld();
            if (_shared.Value.runtimeDataService.Health <= 0)
            {
                _shared.Value.runtimeDataService.IsPaused = true;
                _shared.Value.ui.deathUI.gameObject.SetActive(_shared.Value.runtimeDataService.IsPaused);
                _shared.Value.ui.deathUI.uiAnimations.ScaleIn();
                //Time.timeScale = _shared.Value.playerService.IsPaused ? 0f : 1f;
            }

            foreach (var entity in _clickEvents)
            {
                ref EcsUguiClickEvent data = ref _clickEventsPool.Get(entity);

                switch (data.WidgetName)
                {
                    case "Restart":
                        _shared.Value.sceneManagmentService.RestartScene();
                        break;
                    case "Exit":
                        _shared.Value.sceneManagmentService.ToMainMenu();
                        break;

                }


            }
        }
    }
}