using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerMovementSystem : IEcsRunSystem,IEcsInitSystem {

        float xMin;
        float xMax;
        float yMin;
        float yMax;
        float padding = 0.3f;

        readonly EcsSharedInject<Shared> _shared = default;

        public void Init(IEcsSystems systems)
        {
        }
        public void Run (IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            MoveBorders();
            var filter = systems.GetWorld().Filter<InputComponent>().Inc<PlayerComponent>().End();

            var inputPool = systems.GetWorld().GetPool<InputComponent>();
            var playerPool = systems.GetWorld().GetPool<PlayerComponent>();

            foreach (var entity in filter)
            {
                ref var inputComponent = ref inputPool.Get(entity);
                ref var playerComponent = ref playerPool.Get(entity);

                //playerComponent.playerTransform.position += inputComponent.movement * _shared.Value.playerSettings.speed * Time.deltaTime;

                var deltaX = inputComponent.movement.x * Time.deltaTime * _shared.Value.runtimeDataService.CurrentPlayerSpeed;
                var newPosX = Mathf.Clamp(playerComponent.playerTransform.position.x + deltaX, xMin, xMax);

                var deltaY = inputComponent.movement.y * Time.deltaTime * _shared.Value.runtimeDataService.CurrentPlayerSpeed;
                var newPosY = Mathf.Clamp(playerComponent.playerTransform.position.y + deltaY, yMin, yMax);

                
                playerComponent.playerTransform.position = new Vector2(newPosX, newPosY);
            }
        }
        public void MoveBorders()
        {
            Camera gameCamera = Camera.main;

            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            //Debug.Log(xMin + " " + xMax);
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }
    }
}