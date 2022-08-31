using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class SpawnEnemySystem : IEcsRunSystem, IEcsInitSystem
    {
        float xMin;
        float xMax;
        float yMin;
        float yMax;
        float padding = -1f;

        readonly EcsSharedInject<Shared> _shared = default;

        public void Init(IEcsSystems systems)
        {
            
        }

        public void Run (IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            MoveBorders();
            float _time = _shared.Value.runtimeDataService.GameTime;
            var ecsWorld = systems.GetWorld();

            foreach (var item in _shared.Value.spawners)
            {
                
                if (_time > item.Value.SpawnTime && item.Value.enemyStat.EnemySpawnRate != -1f)
                {
                    var healthPool = systems.GetWorld().GetPool<HealthComponent>();
                    var enemyEntity = ecsWorld.NewEntity();
                    var x = Random.Range(xMin, xMax);
                    var enemyGo = item.Value.GetNewInstance.CommandGetNewInstance(new Vector2(x, yMax));
                    enemyGo.GetComponent<Enemy>().ecsEntity = enemyEntity;

                    var enemyPool = systems.GetWorld().GetPool<EnemyComponent>();
                    enemyPool.Add(enemyEntity);
                    healthPool.Add(enemyEntity).health = item.Value.enemyStat.EnemyHp;

                    ref var enemyComponent = ref enemyPool.Get(enemyEntity);
                    enemyComponent.speed = item.Value.enemyStat.EnemySpeed;
                    enemyComponent.enemyType = item.Value.enemyStat.enemyType;
                    enemyComponent.transform = enemyGo.transform;

                    item.Value.SpawnTime
                        = Random.Range(_time, _time + item.Value.enemyStat.EnemySpawnRate);

                }
            }
           
            
        }
        public void MoveBorders()
        {
            Camera gameCamera = Camera.main;

            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + padding;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }
    }

}