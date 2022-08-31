using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class BulletMovementSystem : IEcsRunSystem
    {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run(IEcsSystems systems)
        {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            var ecsWorld = systems.GetWorld();
            var filter = systems.GetWorld().Filter<BulletComponent>().End();

            var bulletPool = systems.GetWorld().GetPool<BulletComponent>();

            foreach (var entity in filter)
            {
                ref var bulletComponent = ref bulletPool.Get(entity);
                //var posX = bulletComponent.bulletTransform.transform.position.x;
                //var posY = bulletComponent.bulletTransform.transform.position.y;
                var projectileSpeed = _shared.Value.runtimeDataService.CurrentProjectileSpeed;


                bulletComponent.bulletTransform.transform.Translate(bulletComponent.direction * projectileSpeed * Time.deltaTime);
            }
        }
    }
}