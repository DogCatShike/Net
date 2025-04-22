using System;
using UnityEngine;

namespace GameClient {
    public class RoleInputComponent {
        // 移动
        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;
        public void Set_MoveAxis(Vector2 value) => moveAxis = value;

        // 跳跃
        bool isJump;
        public bool IsJump => isJump;
        public void Set_IsJump(bool value) => isJump = value;
    }
}