using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class HealthPointSpawnSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<EnemyDeathComponent>()
                .End();

            var healthUpPool = systems.GetWorld().GetPool<HealthPointComponent>();
            var enemyDeathPool = systems.GetWorld().GetPool<EnemyDeathComponent>();

            foreach (var entity in filter)
            {
                //ref var healthComponent = ref healthPool.Get(entity);
                var enemyDeathComponent = enemyDeathPool.Get(entity);
                //health spawn broken srp todo : another system
                int healthRND = Random.Range(0, 100);
                if (healthRND <= _shared.Value.runtimeDataService.currentDiffuculty.HealthDropPercent)
                {
                    var healthGO = _shared.Value.healthFactory.GetNewInstance(enemyDeathComponent.deathPoint);
                    var healthEntity = ecsWorld.NewEntity();
                    healthUpPool.Add(healthEntity).transform = healthGO.transform;
                    //healthEntity.ecsEntity = expEntity;
                }
            }
        }
    }
}