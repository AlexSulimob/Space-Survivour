using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
namespace Client {
    sealed class ReleaseSystem : IEcsRunSystem, IEcsInitSystem
    {
        float yMin;
        float yMax;
        float padding = 20f;

        readonly EcsSharedInject<Shared> _shared = default;

        public void Run (IEcsSystems systems) {
            var bulletfilter = systems.GetWorld().Filter<BulletComponent>().End();
            var enemyfilter = systems.GetWorld().Filter<EnemyComponent>().End();
            var healthUpfilter = systems.GetWorld().Filter<HealthPointComponent>().End();
            var expfilter = systems.GetWorld().Filter<ExpComponent>().End();
            var strongBallfilter = systems.GetWorld().Filter<StrongBollComponent>().End();

            var bulletPool = systems.GetWorld().GetPool<BulletComponent>();
            var enemyPool = systems.GetWorld().GetPool<EnemyComponent>();

            var healthUpPool = systems.GetWorld().GetPool<HealthPointComponent>();
            var expPool = systems.GetWorld().GetPool<ExpComponent>();

            var healthPool = systems.GetWorld().GetPool<HealthComponent>();
            var strongBallPool = systems.GetWorld().GetPool<StrongBollComponent>();

            foreach (var bulletEntity in bulletfilter)
            {
                ref var bulletComponent = ref bulletPool.Get(bulletEntity);
                if (bulletComponent.bulletTransform.position.y > yMin)
                {
                    _shared.Value.playerBulletFactory
                        .ReleaseInstance(bulletComponent.bulletTransform.gameObject);

                    bulletPool.Del(bulletEntity);
                }
            }

            foreach (var enemyEntity in enemyfilter)
            {
                ref var enemyComponent = ref enemyPool.Get(enemyEntity);
                if (enemyComponent.transform.position.y < yMax)
                {
                    _shared.Value.smallEnemyFactory
                        .ReleaseInstance(enemyComponent.transform.gameObject);

                    enemyPool.Del(enemyEntity);
                    healthPool.Del(enemyEntity);
                }
            }
            foreach (var healthUpEntity in healthUpfilter)
            {
                ref var healthUpComponent = ref healthUpPool.Get(healthUpEntity);
                if (healthUpComponent.transform.position.y < yMax)
                {
                    _shared.Value.healthFactory
                        .ReleaseInstance(healthUpComponent.transform.gameObject);

                    healthUpPool.Del(healthUpEntity);
                }
            }
            foreach (var expEntity in expfilter)
            {
                ref var expComponent = ref expPool.Get(expEntity);
                if (expComponent.transform.position.y < yMax)
                {
                    _shared.Value.expFactory
                        .ReleaseInstance(expComponent.transform.gameObject);

                    expPool.Del(expEntity);
                }
            }

            foreach (var strongballEntity in strongBallfilter)
            {
                ref var strongballEntityComponent = ref strongBallPool.Get(strongballEntity);
                if (strongballEntityComponent.transform.position.y < yMax)
                {
                    _shared.Value.bolFactory
                        .ReleaseInstance(strongballEntityComponent.transform.gameObject);

                    strongBallPool.Del(strongballEntity);
                }
            }
        }
        public void Init(IEcsSystems systems)
        {
            MoveBorders();
        }
        public void MoveBorders()
        {
            Camera gameCamera = Camera.main;

            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }

    }

}