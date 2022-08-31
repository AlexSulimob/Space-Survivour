using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Client {
    sealed class EnemyHitSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<HitComponent>()
                .End();

            var hitPool = systems.GetWorld().GetPool<HitComponent>();
            var bulletPool = systems.GetWorld().GetPool<BulletComponent>();
            var healthPool = systems.GetWorld().GetPool<HealthComponent>();

            var explosionPool = systems.GetWorld().GetPool<ExplosionComponent>();

            foreach (var entity in filter)
            {
                ref var hitComponent = ref hitPool.Get(entity);
                if (hitComponent.first.TryGetComponent<PlayerProjectile>(out PlayerProjectile playerProjectile)
                    && hitComponent.other.TryGetComponent<Enemy>(out Enemy enemyMB))
                {

                    if (healthPool.Has(enemyMB.ecsEntity))
                    {
                        healthPool.Get(enemyMB.ecsEntity).health -= 1;

                        var explosionEntity = ecsWorld.NewEntity();
                        explosionPool.Add(explosionEntity).point = hitComponent.other.transform.position;

                    }

                }

            }
        }
    }
}
