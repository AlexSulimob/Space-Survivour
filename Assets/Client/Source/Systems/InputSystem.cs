using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class InputSystem : IEcsRunSystem {        
        public void Run (IEcsSystems systems) {

            var filter = systems.GetWorld().Filter<InputComponent>().End();
            var inputPool = systems.GetWorld().GetPool<InputComponent>();

            foreach (var entity in filter) {
                ref var inputComponent = ref inputPool.Get(entity);

                inputComponent.movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

                inputComponent.shoot_INPUT_DOWN = Input.GetKeyDown(KeyCode.Space);
                inputComponent.shoot_INPUT_HOLD = Input.GetKey(KeyCode.Space);
                inputComponent.shoot_INPUT_REALEASE = Input.GetKeyUp(KeyCode.Space);

                inputComponent.pause_INPUT_DOWN = Input.GetKeyDown(KeyCode.P);
                inputComponent.pause_INPUT_HOLD = Input.GetKey(KeyCode.P);
                inputComponent.pause_INPUT_REALEASE = Input.GetKeyUp(KeyCode.P);

            }

        }
    }
}