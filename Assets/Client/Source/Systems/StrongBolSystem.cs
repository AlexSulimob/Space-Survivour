using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class StrongBolSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {


            var ecsWorld = systems.GetWorld();
            var strongBolPool = systems.GetWorld().GetPool<StrongBollComponent>();

            var filter = systems.GetWorld().Filter<StrongBollComponent>().End();
            var cannon = _shared.Value.runtimeDataService.cannonBallSkill;
            foreach (var entity in filter)
            {
                ref var strongBolComponent = ref strongBolPool.Get(entity);   
                strongBolComponent.rb.simulated = _shared.Value.runtimeDataService.IsPaused ? false : true;
            }

            if (_shared.Value.runtimeDataService.IsPaused)
                return;


            if (cannon.CannonBallSpawnTime + cannon.CannonBallCd >= _shared.Value.runtimeDataService.GameTime)
            {
                return;
            }
            for (int i = 0; i < cannon.CannonBallSpawnCount; i++)
            {
                var strongBollEntity = ecsWorld.NewEntity();
                var bolGo = _shared.Value.bolFactory.GetNewInstance(_shared.Value.runtimeDataService.player.position);
                _shared.Value.audioService.PlaySound(_shared.Value.audioService.clips.cannonBalSound);
                bolGo.collisionChecker.ecsWorld = ecsWorld;
                //bolGo.rb
                ref var bollComponent = ref strongBolPool.Add(strongBollEntity);
                bollComponent.transform = bolGo.transform;
                bollComponent.rb = bolGo.rb;
                bolGo.rb.AddForce(new Vector2(Random.Range(-0.2f, 0.2f), 1f) * 10f,ForceMode2D.Impulse);
                bolGo.rb.AddTorque(Random.Range(-1000f, 1000f));

                cannon.CannonBallSpawnTime = _shared.Value.runtimeDataService.GameTime;
            }
        }
    }
}