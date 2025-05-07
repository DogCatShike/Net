using System;
using UnityEngine;

namespace GameClient {
    public class RoleEntity : MonoBehaviour {
        public IDSignature idSig;
        public int typeID;

        [SerializeField] RoleInputComponent inputComponent;
        public RoleInputComponent InputComponent => inputComponent;

        public void Ctor() {
            inputComponent = new RoleInputComponent();
        }
    }
}