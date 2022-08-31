using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class ThundersSystem : IEcsRunSystem, IEcsInitSystem {
        float xMin;
        float xMax;
        float yMin;
        float yMax;
        float padding = 3f;


        readonly EcsSharedInject<Shared> _shared = default;
        int count = 0;
        public void Run(IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;
            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<EnemyComponent>()
                .Inc<HealthComponent>()
                .End();

            var enemyPool = systems.GetWorld().GetPool<EnemyComponent>();
            var healthPool = systems.GetWorld().GetPool<HealthComponent>();
            var runTimeData = _shared.Value.runtimeDataService;
            if (runTimeData.thunderSkill.ThunderSpawnTime + runTimeData.thunderSkill.cd >= runTimeData.GameTime)
            {
                return;
            }
            
            foreach (var entity in filter) {
                ref var enemyComponent = ref enemyPool.Get(entity);

                bool enemyIsInPlayField = (enemyComponent.transform.position.x > xMin && enemyComponent.transform.position.x < xMax)
                 && (enemyComponent.transform.position.y > yMin && enemyComponent.transform.position.y < yMax);

                if (!enemyIsInPlayField)
                    continue;

                if (runTimeData.thunderSkill.ThunderSpawnCount <= count)
                {
                    count = 0;
                    break;
                }
                _shared.Value.audioService.PlaySound(_shared.Value.audioService.clips.ThunderSound);
                runTimeData.thunderSkill.ThunderSpawnTime = _shared.Value.runtimeDataService.GameTime;
                ref var healthComponent = ref healthPool.Get(entity);
                healthComponent.health -= 1;
                count += 1;
                Shared.thunderFactory.GetNewInstance(enemyComponent.transform.position);

            }
        }

        public void MoveBorders()
        {
            Camera gameCamera = Camera.main;

            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            //Debug.Log(xMin + " " + xMax);
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }

        public void Init(IEcsSystems systems)
        {
            MoveBorders();
        }
    }
}