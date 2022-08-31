using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerHitSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<HitComponent>()
                .End();

            var hitPool = systems.GetWorld().GetPool<HitComponent>();
            var explosionPool = systems.GetWorld().GetPool<ExplosionComponent>();

            foreach (var entity in filter)
            {
                ref var hitComponent = ref hitPool.Get(entity);
                if (hitComponent.first.TryGetComponent<Player>(out Player player)
                    && hitComponent.other.TryGetComponent<Enemy>(out Enemy enemyMB))
                {
                    var explosionEntity = ecsWorld.NewEntity();
                    explosionPool.Add(explosionEntity).point = _shared.Value.runtimeDataService.player.position;

                    _shared.Value.runtimeDataService.Health -= 1;
                    hitPool.Del(entity);
                }

            }
        }
    }
}