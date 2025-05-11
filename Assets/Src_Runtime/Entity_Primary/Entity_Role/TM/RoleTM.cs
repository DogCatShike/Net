using System;
using UnityEngine;

namespace GameClient {
    [Serializable]
    public class RoleTM {
        public int typeID;
        public string name;

        public float moveSpeed;
        public float climbSpeed;
        public float jumpForce;
        public int maxJumpTimes;
    }
}