using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
namespace Client {
    sealed class ExpSpawnSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<EnemyDeathComponent>()
                .End();

            var expPool = systems.GetWorld().GetPool<ExpComponent>();
            var enemyDeathPool = systems.GetWorld().GetPool<EnemyDeathComponent>();

            foreach (var entity in filter)
            {
                var enemyDeathComponent = enemyDeathPool.Get(entity);
                //exp span broken srp. todo : another system
                var expGo = _shared.Value.expFactory.GetNewInstance(enemyDeathComponent.deathPoint);

                var expEntity = ecsWorld.NewEntity();
                expGo.ecsEntity = expEntity;
                ref var expComponent = ref expPool.Add(expEntity);
                expComponent.transform = expGo.transform;

                switch (enemyDeathComponent.enemyType)
                {
                    case EnemyTypes.smallEnemy:
                        expComponent.amountExp = _shared.Value.playerSettings.expForSmallEnemy;
                        break;
                    case EnemyTypes.middleEnemy:
                        expComponent.amountExp = _shared.Value.playerSettings.expForMiddleEnemy;
                        break;
                    case EnemyTypes.bigEnemy:
                        expComponent.amountExp = _shared.Value.playerSettings.expForBigEnemy;
                        break;
                }
            }
        }
    }
}