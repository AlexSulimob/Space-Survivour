using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class EnemyDeathSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<HealthComponent>()
                .Inc<EnemyComponent>()
                .End();

            var healthPool = systems.GetWorld().GetPool<HealthComponent>();
            var enemyPool = systems.GetWorld().GetPool<EnemyComponent>();

            var enemyDeathPool = systems.GetWorld().GetPool<EnemyDeathComponent>();

            var explosionPool = systems.GetWorld().GetPool<ExplosionComponent>();

            foreach (var entity in filter)
            {
                ref var healthComponent = ref healthPool.Get(entity);
                ref var enemyComponent = ref enemyPool.Get(entity);
                if (healthComponent.health <= 0)
                {
                    var deathEntity = ecsWorld.NewEntity();
                    ref var deathComponent = ref enemyDeathPool.Add(deathEntity);
                    deathComponent.deathPoint = enemyComponent.transform.position;
                    deathComponent.enemyType = enemyComponent.enemyType;

                    var explosionEntity = ecsWorld.NewEntity();
                    explosionPool.Add(explosionEntity).point = enemyComponent.transform.position;

                    _shared.Value.smallEnemyFactory
                        .ReleaseInstance(enemyComponent.transform.gameObject);

                    _shared.Value.runtimeDataService.Kills += 1;
                    enemyPool.Del(entity);
                    healthPool.Del(entity);
                }
            }
        }
    }
}