using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ExpMovementSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<ExpComponent>()
                .End();

            var expPool = systems.GetWorld().GetPool<ExpComponent>();

            foreach (var entity in filter)
            {
                ref var expComponent = ref expPool.Get(entity);

                expComponent.transform.Translate(Vector3.down * _shared.Value.playerSettings.expSpeed * Time.deltaTime);

            }
        }
    }
}