using System;
using UnityEngine;

namespace GameClient.System_Game {
    public static partial class RoleDomain {
        public static RoleEntity Spawn(GameSystemContext ctx, int typeID) {
            var role = GameFactory.Role_Create(ctx.assetModule, typeID);
            ctx.roleRepo.Add(role);
            return role;
        }

        #region Input
        public static void Input_Record(GameSystemContext ctx, RoleEntity role) {
            var inputCom = role.InputComponent;
            var input = ctx.inputModule;

            inputCom.Set_MoveAxis(input.MoveAxis);
            inputCom.Set_IsMove(input.IsMove);
            inputCom.Set_IsJump(input.IsJump);
        }
        #endregion
    }
}