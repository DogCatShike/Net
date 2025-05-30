using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameClient {
    public class RoleRepo {
        Dictionary<IDSignature, RoleEntity> all;

        RoleEntity[] temArray;

        public RoleRepo() {
            all = new Dictionary<IDSignature, RoleEntity>();
            temArray = new RoleEntity[100];
        }

        public void Add(RoleEntity entity) {
            all.Add(entity.idSig, entity);
        }

        public void Remove(RoleEntity entity) {
            all.Remove(entity.idSig);
        }

        public int TakeAll(out RoleEntity[] array) {
            if (all.Count > temArray.Length) {
                temArray = new RoleEntity[all.Count * 2];
            }
            all.Values.CopyTo(temArray, 0);
            array = temArray;
            return all.Count;
        }

        public bool TryGet(IDSignature idSig, out RoleEntity entity) {
            return all.TryGetValue(idSig, out entity);
        }


        public void Clear() {
            all.Clear();
        }
    }
}