using System;
using UnityEngine;

namespace GameClient {
    public static class GameFactory {
        #region Role
        public static RoleEntity Role_Create(AssetModule assetModule, int typeID) {
            bool has = assetModule.Entity_Role_TryGet(out GameObject prefab);
            if (!has) {
                Debug.LogError("Role entity not found!");
                return null;
            }

            has = assetModule.Role_TryGet(typeID, out var tm);
            if (!has) {
                Debug.LogError("TryGetRole() failed");
                return null;
            }

            GameObject go = GameObject.Instantiate(prefab);
            RoleEntity role = go.GetComponent<RoleEntity>();
            role.Ctor();

            role.idSig = new IDSignature(EntityType.Role, typeID);
            role.typeID = typeID;

            role.Set_MoveSpeed(tm.moveSpeed);
            role.Set_JumpForce(tm.jumpForce);

            role.Set_State(RoleState.Idle);

            return role;
        }
        #endregion
    }
}