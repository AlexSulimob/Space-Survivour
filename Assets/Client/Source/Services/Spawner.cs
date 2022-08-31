using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class Spawner
    {
        public EnemyStats enemyStat;
        
        public float SpawnTime { get; set; }


        public ICommandGetNewInstance GetNewInstance;

        public Spawner(ICommandGetNewInstance GetNewInstance)
        {
            this.GetNewInstance = GetNewInstance;
        }

        
    }

}
