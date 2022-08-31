using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Client {
    sealed class StatisticSystem : IEcsRunSystem,IEcsInitSystem {
        readonly EcsSharedInject<Shared> _shared = default;
        //TODO make constant static class with key string values
        bool isKillSaved = false;
        public void Init(IEcsSystems systems)
        {
            isKillSaved = false;
            if (!PlayerPrefs.HasKey("Kills"))
            {
                PlayerPrefs.SetFloat("Kills", 0f);
            }
            if (!PlayerPrefs.HasKey("LongestSurvival"))
            {
                PlayerPrefs.SetFloat("LongestSurvival", 0f);
            }
            if (!PlayerPrefs.HasKey("KillsInRow"))
            {
                PlayerPrefs.SetFloat("KillsInRow", 0);
            }
            if (!PlayerPrefs.HasKey("HighestLevel"))
            {
                PlayerPrefs.SetFloat("HighestLevel", 0);
            }
        }

        public void Run (IEcsSystems systems) {
            if (_shared.Value.runtimeDataService.IsPaused)
            {
                if (!isKillSaved)
                {
                    if (_shared.Value.runtimeDataService.Health <= 0)
                    {
                        PlayerPrefs.SetFloat("Kills", PlayerPrefs.GetFloat("Kills") + _shared.Value.runtimeDataService.Kills);
                        isKillSaved = true;
                    }
                }
                return;
            }

        


            if(PlayerPrefs.GetFloat("LongestSurvival")< _shared.Value.runtimeDataService.GameTime)
            {
                PlayerPrefs.SetFloat("LongestSurvival", _shared.Value.runtimeDataService.GameTime);
            }

            if (PlayerPrefs.GetFloat("KillsInRow") < _shared.Value.runtimeDataService.Kills)
            {
                PlayerPrefs.SetFloat("KillsInRow", _shared.Value.runtimeDataService.Kills);
            }
            if (PlayerPrefs.GetFloat("HighestLevel") < _shared.Value.runtimeDataService.CurrentLevel + 1)
            {
                PlayerPrefs.SetFloat("HighestLevel", _shared.Value.runtimeDataService.CurrentLevel + 1);
            }
        }
    }
}