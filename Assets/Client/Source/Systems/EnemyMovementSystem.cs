using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class EnemyMovementSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            var filter = systems.GetWorld().Filter<EnemyComponent>()
                .End();

            var enemyPool = systems.GetWorld().GetPool<EnemyComponent>();

            foreach (var entity in filter)
            {
                ref var enemyComponent = ref enemyPool.Get(entity);

                var posX = enemyComponent.transform.position.x;
                var posY = enemyComponent.transform.position.y;

                enemyComponent.transform.position = new Vector3(posX, posY - enemyComponent.speed * Time.deltaTime, 0f);
                //enemyComponent.transform = new Transform
            }

        }
    }
}