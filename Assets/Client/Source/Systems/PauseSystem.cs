using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;


namespace Client
{
    public class PauseSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsSharedInject<Shared> _shared = default;

        EcsPool<EcsUguiClickEvent> _clickEventsPool;
        EcsFilter _clickEvents;

        bool pauseUiBtn;
        public void Init(IEcsSystems systems) {
            var world = systems.GetWorld();

            _clickEventsPool = world.GetPool<EcsUguiClickEvent>();
            _clickEvents = world.Filter<EcsUguiClickEvent>().End();
        }

        public void Run(IEcsSystems systems) {
            var ecsWorld = systems.GetWorld();
            var filter = systems.GetWorld().Filter<InputComponent>()
                .End();

            var inputPool = systems.GetWorld().GetPool<InputComponent>();

            foreach (var entity in _clickEvents){
                ref EcsUguiClickEvent data = ref _clickEventsPool.Get(entity);
                if (data.WidgetName == "PauseBtn")
                {
                    _shared.Value.runtimeDataService.IsPaused = !_shared.Value.runtimeDataService.IsPaused;
                    _shared.Value.ui.pauseUI.uiAnimations.ScaleIn();
                    //_shared.Value.ui.pauseUI.gameObject.SetActive(_shared.Value.playerService.IsPaused);
                    //Time.timeScale = _shared.Value.playerService.IsPaused ? 0f : 1f;
                }
            }


            foreach (var entity in filter) {
                ref var inputComponent = ref inputPool.Get(entity);
                if (inputComponent.pause_INPUT_DOWN ){
                    _shared.Value.runtimeDataService.IsPaused = !_shared.Value.runtimeDataService.IsPaused;
                    _shared.Value.ui.pauseUI.gameObject.SetActive(_shared.Value.runtimeDataService.IsPaused);
                    Time.timeScale = _shared.Value.runtimeDataService.IsPaused ? 0f : 1f;
 
                }
            }

            foreach (var entity in _clickEvents) {
                ref EcsUguiClickEvent data = ref _clickEventsPool.Get(entity);
                if (data.WidgetName == "ContinueBtn"){
                    _shared.Value.runtimeDataService.IsPaused = !_shared.Value.runtimeDataService.IsPaused;
                    _shared.Value.ui.pauseUI.uiAnimations.ScaleOut();
                    //_shared.Value.ui.pauseUI.gameObject.SetActive(_shared.Value.playerService.IsPaused);
                    //Time.timeScale = _shared.Value.playerService.IsPaused ? 0f : 1f;

                }
            }
        }

    }

}
