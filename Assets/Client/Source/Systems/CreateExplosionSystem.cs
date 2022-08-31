using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class CreateExplosionSystem : IEcsRunSystem {
            
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            var filter = systems.GetWorld().Filter<ExplosionComponent>().End();
            var explosionPool = systems.GetWorld().GetPool<ExplosionComponent>();

            foreach (var entity in filter)
            {
                ref var explosionComponent = ref explosionPool.Get(entity);
                _shared.Value.explosionFactory.GetNewInstance(explosionComponent.point);
                _shared.Value.audioService.PlaySound(_shared.Value.audioService.clips.explosionSound);
                explosionPool.Del(entity);
            }
        }
    }
}