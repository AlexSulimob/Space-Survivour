using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerInitSystem : IEcsInitSystem {

        readonly EcsSharedInject<Shared> _shared = default;
        public void Init(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();

            var playerEntity = ecsWorld.NewEntity();

            var playerPool = ecsWorld.GetPool<PlayerComponent>();


            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);

            var playerInputPool = ecsWorld.GetPool<InputComponent>();
            playerInputPool.Add(playerEntity);
            ref var playerInputComponent = ref playerInputPool.Get(playerEntity);

            var playerFireRatePool = ecsWorld.GetPool<PlayerFireRateComponent>();
            playerFireRatePool.Add(playerEntity);
            ref var playerFireRateComponent = ref playerFireRatePool.Get(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            
            playerGO.GetComponentInChildren<CollisionCheckerView>().ecsWorld = ecsWorld;

            _shared.Value.runtimeDataService.IsPaused = false;

            Time.timeScale = _shared.Value.runtimeDataService.IsPaused ? 0f : 1f;


            _shared.Value.runtimeDataService.CurrentPlayerSpeed = _shared.Value.playerSettings.speed;
            _shared.Value.runtimeDataService.CurrentProjectileSpeed = _shared.Value.playerSettings.projectileSpeed;
            _shared.Value.runtimeDataService.Health = _shared.Value.playerSettings.playerStartMaxHealth;
            _shared.Value.runtimeDataService.MaxHealth = _shared.Value.playerSettings.playerStartMaxHealth;

            playerComponent.playerTransform = playerGO.transform;
            playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.weapons = playerGO.GetComponentInChildren<PlayerWeapons>();

        }

    }
}