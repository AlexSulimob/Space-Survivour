using UnityEngine;

namespace Client {
    struct PlayerComponent {
        public Transform playerTransform;
        public CapsuleCollider playerCollider;
        public PlayerWeapons weapons;

    }
}