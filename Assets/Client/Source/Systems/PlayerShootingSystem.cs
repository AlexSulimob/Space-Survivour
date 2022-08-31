using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PlayerShootingSystem : IEcsRunSystem
    {
        readonly EcsSharedInject<Shared> _shared = default;

        public void Run(IEcsSystems systems)
        {

            if (_shared.Value.runtimeDataService.IsPaused) return;

            var ecsWorld = systems.GetWorld();
            var filter = systems.GetWorld().Filter<InputComponent>()
                .Inc<PlayerComponent>()
                .Inc<PlayerFireRateComponent>()
                .End();

            var inputPool = systems.GetWorld().GetPool<InputComponent>();
            var playerPool = systems.GetWorld().GetPool<PlayerComponent>();
            var bulletPool = systems.GetWorld().GetPool<BulletComponent>();
            var playerFireRatePool = systems.GetWorld().GetPool<PlayerFireRateComponent>();

            foreach (var entity in filter)
            {

                ref var inputComponent = ref inputPool.Get(entity);
                ref var playerComponent = ref playerPool.Get(entity);
                ref var playerFireRateComponent = ref playerFireRatePool.Get(entity);
                var fireRate = _shared.Value.runtimeDataService.fireRateSkill.CurrentFireRate;
                bool isCooldowned = _shared.Value.runtimeDataService.GameTime > playerFireRateComponent.shootingStartTime + fireRate;

                if ((inputComponent.shoot_INPUT_DOWN || inputComponent.shoot_INPUT_HOLD)
                    && isCooldowned)
                {
                    for (int i = 0; i < _shared.Value.runtimeDataService.countOfBaseWeapon.CurrentWeapons; i++)
                    {
                        var bulletGo = _shared.Value.playerBulletFactory.GetNewInstance(playerComponent.weapons.weaponsTransforms[i].position,
                            playerComponent.weapons.weaponsTransforms[i].rotation);

                        bulletGo.collisionChecker.ecsWorld = ecsWorld;
                        var bulletEntity = ecsWorld.NewEntity();
                        bulletGo.ecsEntity = bulletEntity;

                        bulletPool.Add(bulletEntity);
                        ref var bulletComponent = ref bulletPool.Get(bulletEntity);
                        bulletComponent.direction = playerComponent.weapons.weaponsTransforms[i].up;
                        bulletComponent.bulletTransform = bulletGo.transform;
                        _shared.Value.audioService.PlaySound(_shared.Value.audioService.clips.laserShootSound);
                    }


                    float _time = _shared.Value.runtimeDataService.GameTime;
                    playerFireRateComponent.shootingStartTime = _time; 
                }

            }
        }

    }
}