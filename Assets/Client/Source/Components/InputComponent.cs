using UnityEngine;

namespace Client {
    struct InputComponent {
        public Vector3 movement;

        public bool shoot_INPUT_DOWN;
        public bool shoot_INPUT_HOLD;
        public bool shoot_INPUT_REALEASE;

        public bool pause_INPUT_DOWN;
        public bool pause_INPUT_HOLD;
        public bool pause_INPUT_REALEASE;
    }
}