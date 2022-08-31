using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Client
{
    sealed class EnemyLevelUpSystem : IEcsRunSystem,IEcsInitSystem
    {
        readonly EcsSharedInject<Shared> _shared = default;

        public void Init(IEcsSystems systems)
        {
            _shared.Value.runtimeDataService.currentDiffuculty = _shared.Value.runtimeDataService.diffucultyDic.diffuculties[0].diffuculty;
            _shared.Value.runtimeDataService.currentDiffucultyIndex = 0;
            _shared.Value.runtimeDataService.nextDiffucultyTime = _shared.Value.runtimeDataService.diffucultyDic.diffuculties[0].timeToNextLevel;

            UpdateDiffuculty();
        }

        public void Run(IEcsSystems systems)
        {
            float _time = _shared.Value.runtimeDataService.GameTime;
            var ecsWorld = systems.GetWorld();
            var diffcultyDic = _shared.Value.runtimeDataService.diffucultyDic.diffuculties;
            bool nextDiffucultyHasCome = _time > _shared.Value.runtimeDataService.nextDiffucultyTime;

            if(nextDiffucultyHasCome)
            {
                int nextIndex = _shared.Value.runtimeDataService.currentDiffucultyIndex + 1;
                //currentIndex += 1;
                _shared.Value.runtimeDataService.currentDiffucultyIndex = nextIndex;
                _shared.Value.runtimeDataService.currentDiffuculty = diffcultyDic[nextIndex].diffuculty;
                _shared.Value.runtimeDataService.nextDiffucultyTime = diffcultyDic[nextIndex].timeToNextLevel;


                UpdateDiffuculty();

            }
        }
        void UpdateDiffuculty()
        {
            foreach (var item in _shared.Value.spawners)
            {
                item.Value.SpawnTime = 0.1f;
            }
            _shared.Value.spawners[typeof(SmallEnemy)].enemyStat = _shared.Value.runtimeDataService.currentDiffuculty.smallEnemy;
            _shared.Value.spawners[typeof(MediumEnemy)].enemyStat = _shared.Value.runtimeDataService.currentDiffuculty.medimumEnemy;
            _shared.Value.spawners[typeof(BigEnemy)].enemyStat = _shared.Value.runtimeDataService.currentDiffuculty.bigEnemy;
        }
    }
}