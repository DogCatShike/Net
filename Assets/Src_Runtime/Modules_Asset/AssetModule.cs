using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameClient {
    public class AssetModule : MonoBehaviour {
        Dictionary<string, GameObject> entityDict;
        AsyncOperationHandle entityHandle;

        // TM
        AsyncOperationHandle roleHandle;
        Dictionary<int, RoleTM> roleDict;

        public void Ctor() {
            entityDict = new Dictionary<string, GameObject>();
        }

        public async Task LoadAll() {
            await Entity_Load();
        }

        public void UnLoadAll() {
            Entity_Release();
        }

        #region Entity
        async Task Entity_Load() {
            AssetLabelReference labelReference = new AssetLabelReference();

            labelReference.labelString = "Entity";
            var handle = Addressables.LoadAssetsAsync<GameObject>(labelReference, null);

            var all = await handle.Task;
            foreach (var item in all) {
                entityDict.Add(item.name, item);
            }

            entityHandle = handle;
        }

        void Entity_Release() {
            if (entityHandle.IsValid()) {
                Addressables.Release(entityHandle);
            }
        }

        public bool Entity_Role_TryGet(out GameObject role) {
            return entityDict.TryGetValue("entity_role", out role);
        }
        #endregion

        #region == Role ==
        async Task RoleTM_Load() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "So_Role";
            var handle = Addressables.LoadAssetsAsync<RoleSo>(labelReference, null);
            var list = await handle.Task;
            foreach (var so in list) {
                var tm = so.tm;
                bool succ = roleDict.TryAdd(tm.typeID, tm);
                if (!succ) {
                    Debug.LogError("Role Add: Already Exist: " + tm.typeID);
                }
            }
            roleHandle = handle;
        }

        void Role_Release() {
            if (roleHandle.IsValid()) {
                Addressables.Release(roleHandle);
            }
        }

        public bool Role_TryGet(int typeID, out RoleTM tm) {
            return roleDict.TryGetValue(typeID, out tm);
        }
        #endregion
    }
}