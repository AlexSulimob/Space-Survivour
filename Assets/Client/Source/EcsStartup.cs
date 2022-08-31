using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {

        [SerializeField] EcsUguiEmitter _uguiEmitter;

        EcsWorld _world;        
        IEcsSystems _systems;
        public Shared shared;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world, shared);
            _systems

                .Add(new ReleaseSystem())

                .Add(new TimeSystem())
                .Add(new EnemyLevelUpSystem())
                .Add(new PlayerInitSystem())
                .Add(new InputSystem())
                //.Add(new MobileInputSystem())
                .Add(new PlayerShootingSystem())
                .Add(new BulletMovementSystem())
                .Add(new ThundersSystem())
                .Add(new PlayerMovementSystem())
                .Add(new SpawnEnemySystem())
                .Add(new EnemyHitSystem())
                .Add(new EnemyDeathSystem())
                .Add(new ExpSpawnSystem())
                .Add(new HealthPointSpawnSystem())
                .Add(new ExpMovementSystem())
                .Add(new HealthPointMovementSystem())

                .Add(new StrongBolSystem())
                .Add(new UfoSystem())
                .Add(new StatisticSystem())
                .DelHere<EnemyDeathComponent>()
                .Add(new EnemyMovementSystem())
                .Add(new CreateExplosionSystem())
                .Add(new PlayerHitSystem())
                .Add(new PlayerCollectExpSystem())
                .Add(new PlayerLevelUpSystem())
                .Add(new HealthPointCollectSystem())
                .Add(new PlayerDeathSystem())
                .DelHere<HitComponent>()
                .Add(new PauseSystem())
                .Add(new UiSystem())
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .InjectUgui(_uguiEmitter)
                .Inject()
                .Init ();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                //_systems.GetWorld("ugui-events").Destroy();
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}