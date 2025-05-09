using System;
using UnityEngine;

namespace GameClient.System_Game {
    public static partial class RoleDomain {
        public static void Loco_Any_Execute(RoleEntity role, float dt) {
            var input = role.InputComponent;

            float xAxis = input.Get_XAxis();
            Loco_Move(role, xAxis, dt);
        }

        public static void Loco_Move(RoleEntity role, float xAxis, float dt) {
            if (dt <= 0) { return; }

            role.Move(xAxis);
        }
    }
}