using System;
using UnityEngine;

namespace GameClient {
    public class RoleInputComponent {
        // 移动
        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;
        public void Set_MoveAxis(Vector2 value) => moveAxis = value;
        public float Get_XAxis() => moveAxis.x;
        public float Get_YAxis() => moveAxis.y;
        
        bool isMove;
        public bool IsMove => isMove;
        public void Set_IsMove(bool value) => isMove = value;

        // 跳跃
        bool isJump;
        public bool IsJump => isJump;
        public void Set_IsJump(bool value) => isJump = value;
    }
}