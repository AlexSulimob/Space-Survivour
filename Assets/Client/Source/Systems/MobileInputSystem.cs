using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;


namespace Client {
    sealed class MobileInputSystem : IEcsRunSystem, IEcsInitSystem
    {

        readonly EcsSharedInject<Shared> _shared = default;

        EcsPool<EcsUguiClickEvent> _clickEventsPool;
        EcsPool<EcsUguiUpEvent> _clickUpEventsPool;

        EcsPool<EcsUguiDownEvent> _clickDownEventsPool;
        EcsFilter _inputDownEvents;
        EcsFilter _inputUpEvents;
        public Vector2 move;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _clickEventsPool = world.GetPool<EcsUguiClickEvent>();
            _clickUpEventsPool = world.GetPool<EcsUguiUpEvent>();

            _clickDownEventsPool = world.GetPool<EcsUguiDownEvent>();

            _inputDownEvents = world.Filter<EcsUguiDownEvent>().End();
            _inputUpEvents = world.Filter<EcsUguiUpEvent>().End();

        }

        public void Run(IEcsSystems systems)
        {

            if(Input.touchCount > 0)
            {
                move = Input.GetTouch(0).deltaPosition;
            }
            else
            {
                move = Vector2.zero;
            }

            var filter = systems.GetWorld().Filter<InputComponent>().End();
            var inputPool = systems.GetWorld().GetPool<InputComponent>();

            foreach (var entity in filter)
            {
                ref var inputComponent = ref inputPool.Get(entity);

                inputComponent.movement = move;

                //inputComponent.shoot_INPUT_DOWN = Input.GetKeyDown(KeyCode.Space);
                inputComponent.shoot_INPUT_HOLD = true;
                //inputComponent.shoot_INPUT_REALEASE = Input.GetKeyUp(KeyCode.Space);


            }

        }
    }
}