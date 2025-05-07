using System;
using UnityEngine;

namespace GameClient.System_Game
{
    public static class RoleDomain
    {
        public static RoleEntity Spawn(GameSystemContext ctx, int typeID) {
            var role = GameFactory.Role_Create(ctx.assetModule, typeID);
            ctx.roleRepo.Add(role);
            return role;
        }
    }
}