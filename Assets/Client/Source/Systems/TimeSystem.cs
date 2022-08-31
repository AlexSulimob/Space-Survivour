using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TimeSystem : IEcsRunSystem, IEcsInitSystem {

        readonly EcsSharedInject<Shared> _shared = default;
        public void Init(IEcsSystems systems)
        {
            _shared.Value.runtimeDataService.GameTime = 0.1f;
        }

        public void Run (IEcsSystems systems) 
        {
            if (_shared.Value.runtimeDataService.IsPaused)
                return;
            _shared.Value.runtimeDataService.GameTime += Time.deltaTime;
        }
    }
}