using System;
using UnityEngine;

namespace GameClient.System_Game {
    public static partial class RoleDomain {
        public static void Loco_Any_Execute(RoleEntity role, float dt, float garv) {
            var input = role.InputComponent;

            float xAxis = input.Get_XAxis();
            float yAxis = input.Get_YAxis();

            Loco_ChangeState(role, input);

            // TODO: 这里状态机不能这么写, 不智能
            // switch (role.State) {
            //     case RoleState.Jump:
            //         Loco_Jump(role, dt);
            //         break;

            //     case RoleState.Move:
            //         Loco_Move(role, xAxis, dt);
            //         break;

            //     case RoleState.Fall:
            //         Loco_Fall(role, garv, dt);
            //         break;

            //     case RoleState.Climb:

            //         break;
            //     case RoleState.Idle:
            //         break;

            //     default:
            //         break;
            // }
            if (role.State.HasFlag(RoleState.Move)) {
                Loco_Move(role, xAxis, dt);
            }

            if (role.State.HasFlag(RoleState.Fall)) {
                Loco_Fall(role, garv, dt);
            }

            if (role.State.HasFlag(RoleState.Jump)) {
                Loco_Jump(role, dt);
            } else if (role.State.HasFlag(RoleState.Climb)) {
                Loco_Climb(role, yAxis, dt);
            }
        }

        public static void Loco_ChangeState(RoleEntity role, RoleInputComponent input) {
            if (role.CanClimb) {
                role.Add_State(RoleState.Climb);
            } else {
                role.Remove_State(RoleState.Climb);
            }

            if (!role.IsCollision) {
                role.Add_State(RoleState.Fall);
            }

            if (input.IsJump) {
                if (role.canJump) {
                    role.Add_State(RoleState.Jump);
                }
            }

            if (role.State.HasFlag(RoleState.Jump) && !role.canJump) {
                float axisY = role.Get_VeloY();
                if (axisY < 0) {
                    role.Remove_State(RoleState.Jump);
                }
            }

            if (input.IsMove) {
                role.Add_State(RoleState.Move);
            }
        }

        public static void Loco_Move(RoleEntity role, float xAxis, float dt) {
            if (dt <= 0) { return; }

            role.Move(xAxis);
        }

        public static void Loco_Climb(RoleEntity role, float yAxis, float dt) {
            if (dt <= 0) { return; }

            role.Climb(yAxis);
        }

        public static void Loco_Jump(RoleEntity role, float dt) {
            if (dt <= 0) { return; }
            if (!role.canJump) { return; }

            role.Jump();
        }

        public static void Loco_Fall(RoleEntity role, float garv, float dt) {
            if (dt <= 0) { return; }

            role.Fall(garv);
        }
    }
}