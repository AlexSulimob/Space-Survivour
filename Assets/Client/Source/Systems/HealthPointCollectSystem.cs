using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class HealthPointCollectSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<HitComponent>()
                .End();

            var hitPool = systems.GetWorld().GetPool<HitComponent>();


            foreach (var entity in filter)
            {
                ref var hitComponent = ref hitPool.Get(entity);

                if (hitComponent.first.TryGetComponent<Player>(out Player player)
                    && hitComponent.other.TryGetComponent<HealthPoint>(out HealthPoint healthMB))
                {
                    if(_shared.Value.runtimeDataService.Health !=_shared.Value.runtimeDataService.MaxHealth )
                        _shared.Value.runtimeDataService.Health++;

                    _shared.Value.audioService.PlaySound(_shared.Value.audioService.clips.healthUpSound);

                    hitPool.Del(entity);
                }
            }
        }
    }
}