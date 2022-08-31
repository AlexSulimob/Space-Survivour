using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
namespace Client {
    sealed class PlayerCollectExpSystem : IEcsRunSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        public void Run (IEcsSystems systems) {
            var ecsWorld = systems.GetWorld();

            var filter = systems.GetWorld().Filter<HitComponent>()
                .End();

            var hitPool = systems.GetWorld().GetPool<HitComponent>();
            var expPool = systems.GetWorld().GetPool<ExpComponent>();


            //var expPool = systems.GetWorld().GetPool<ExpComponent>();

            foreach (var entity in filter)
            {
                ref var hitComponent = ref hitPool.Get(entity);
                if (hitComponent.first.TryGetComponent<Player>(out Player player)
                    && hitComponent.other.TryGetComponent<Exp>(out Exp expMB))
                {
                    _shared.Value.audioService.PlaySound(_shared.Value.audioService.clips.expSound);
                    _shared.Value.runtimeDataService.Exp += expPool.Get(expMB.ecsEntity).amountExp;
                    _shared.Value.expFactory.ReleaseInstance(expMB.gameObject);
                    expPool.Del(expMB.ecsEntity);
                      
                    hitPool.Del(entity);
                    
                }

            }
        }
    }
}