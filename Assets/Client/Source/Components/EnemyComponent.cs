using UnityEngine;

namespace Client {
    struct EnemyComponent {
        public Transform transform;
        public float speed;
        public EnemyTypes enemyType;

    }
    public enum EnemyTypes
    {
        smallEnemy,
        middleEnemy,
        bigEnemy
    }
}
