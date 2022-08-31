using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class Shared : MonoBehaviour
    {
        public PlayerSettings playerSettings;
        public RuntimeDataService runtimeDataService;
        public AudioService audioService;
        public UI ui;

        public GenericFactory<PlayerBullet> playerBulletFactory;
        public GenericFactory<SmallEnemy> smallEnemyFactory;
        public GenericFactory<MediumEnemy> mediumEnemyFactory;
        public GenericFactory<BigEnemy> bigEnemyEnemyFactory;
        public GenericFactory<Explosion> explosionFactory;
        public GenericFactory<Exp> expFactory;
        public GenericFactory<HealthPoint> healthFactory;
        public static GenericFactory<Thunder> thunderFactory; //danger
        public GenericFactory<StrongBol> bolFactory;
        public GenericFactory<Ufo> ufoFactory;

        public SceneManagmentService sceneManagmentService;

        public Dictionary<Type, Spawner> spawners = new Dictionary<Type, Spawner>(); //thats danger too 

        private void Start()
        {
            playerBulletFactory = new GenericFactory<PlayerBullet>("Prefabs/laser-bolts_2");
            smallEnemyFactory = new GenericFactory<SmallEnemy>("Prefabs/enemy-small_0");
            mediumEnemyFactory = new GenericFactory<MediumEnemy>("Prefabs/enemy-medium_0");
            bigEnemyEnemyFactory = new GenericFactory<BigEnemy>("Prefabs/enemy-big_0");
            explosionFactory = new GenericFactory<Explosion>("Prefabs/Explosion");
            expFactory = new GenericFactory<Exp>("Prefabs/Exp");
            healthFactory = new GenericFactory<HealthPoint>("Prefabs/Health");
            thunderFactory = new GenericFactory<Thunder>("Prefabs/Thunder");
            bolFactory = new GenericFactory<StrongBol>("Prefabs/StrongBol");
            ufoFactory = new GenericFactory<Ufo>("Prefabs/ufoBlue");

            spawners[smallEnemyFactory.GetPrefabType()] = new Spawner(smallEnemyFactory);
            spawners[mediumEnemyFactory.GetPrefabType()] = new Spawner(mediumEnemyFactory);
            spawners[bigEnemyEnemyFactory.GetPrefabType()] = new Spawner(bigEnemyEnemyFactory);

            //spawners[typeof(SmallEnemy)].GetNewInstance.CommandGetNewInstance();
        }
    }

}
