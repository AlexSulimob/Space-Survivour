using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class UfoSystem : IEcsRunSystem,IEcsInitSystem,IEcsDestroySystem {

        readonly EcsSharedInject<Shared> _shared = default;
        readonly EcsWorldInject _defaultWorld = default;

        readonly EcsPoolInject<UfoComponent> ufoPool = default;

        public void Init(IEcsSystems systems)
        {
            _shared.Value.runtimeDataService.ufoSkill.createUfo += CreateUfo;
        }

        public void Destroy(IEcsSystems systems)
        {
            _shared.Value.runtimeDataService.ufoSkill.createUfo -= CreateUfo;
        }
        void CreateUfo()
        {
            
            var ufoEntity = _defaultWorld.Value.NewEntity();
            ref var ufoComponent = ref ufoPool.Value.Add(ufoEntity);
            var ufoGO = _shared.Value.ufoFactory.GetNewInstance(_shared.Value.runtimeDataService.player.position);
            ufoComponent.parent = ufoGO.transform;
            ufoComponent.value = ufoGO.GetComponentInChildren<Ufo>();
            ufoGO.collisionChecker.ecsWorld = _defaultWorld.Value;
        }


        public void Run (IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;

            var filter = systems.GetWorld().Filter<UfoComponent>()
                .End();

            foreach (var entity in filter)
            {
                float ufoSpeed = _shared.Value.runtimeDataService.ufoSkill.UfoSpeed;
                ref var ufoComponent = ref ufoPool.Value.Get(entity);
                ufoComponent.parent.transform.position = _shared.Value.runtimeDataService.player.transform.position;
                ufoComponent.value.transform.RotateAround(_shared.Value.runtimeDataService.player.transform.position, new Vector3(0f,0f,-1f), ufoSpeed * Time.deltaTime);
                
            }
        }
    }
}