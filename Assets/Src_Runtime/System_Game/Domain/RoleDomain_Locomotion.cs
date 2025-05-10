using System;
using UnityEngine;

namespace GameClient.System_Game {
    public static partial class RoleDomain {
        public static void Loco_Any_Execute(RoleEntity role, float dt, float garv) {
            var input = role.InputComponent;

            float xAxis = input.Get_XAxis();

            switch (role.State) {
                case RoleState.Fall:
                    Loco_Fall(role, garv, dt);
                    break;


                case RoleState.Move:
                    Loco_Move(role, xAxis, dt);
                    break;

                case RoleState.Jump:
                    Loco_Jump(role, dt);
                    break;

                case RoleState.Climb:

                    break;
                case RoleState.Idle:
                    break;

                default:
                    break;
            }
        }

        public static void Loco_Move(RoleEntity role, float xAxis, float dt) {
            if (dt <= 0) { return; }

            role.Move(xAxis);
        }

        public static void Loco_Jump(RoleEntity role, float dt) {
            if (dt <= 0) { return; }

            role.Jump();
        }

        public static void Loco_Fall(RoleEntity role, float garv, float dt) {
            if (dt <= 0) { return; }

            role.Fall(garv);
        }
    }
}