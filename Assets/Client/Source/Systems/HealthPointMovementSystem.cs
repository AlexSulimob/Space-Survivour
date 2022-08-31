using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class HealthPointMovementSystem : IEcsRunSystem
    {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run(IEcsSystems systems)
        {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<HealthPointComponent>()
                .End();

            var expPool = systems.GetWorld().GetPool<HealthPointComponent>();

            foreach (var entity in filter)
            {
                ref var healthComponent = ref expPool.Get(entity);
                
                healthComponent.transform.Translate(Vector3.down * _shared.Value.playerSettings.HealthSpeed * Time.deltaTime);

            }
        }
    }
}