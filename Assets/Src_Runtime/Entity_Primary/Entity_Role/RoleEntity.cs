using System;
using UnityEngine;

namespace GameClient {
    public class RoleEntity : MonoBehaviour {
        // 基础
        public IDSignature idSig;
        public int typeID;
        [SerializeField] Rigidbody2D rb;

        // 属性
        float moveSpeed;
        public void Set_MoveSpeed(float value) => moveSpeed = value;
        float jumpForce;
        public void Set_JumpForce(float value) => jumpForce = value;

        float faceAxis;

        [SerializeField] RoleInputComponent inputComponent;
        public RoleInputComponent InputComponent => inputComponent;

        [SerializeField] RoleMoveComponent moveConponent;
        public RoleMoveComponent MoveConponent => moveConponent;

        public void Ctor() {
            inputComponent = new RoleInputComponent();
            moveConponent = new RoleMoveComponent();

            moveConponent.Inject(rb);
        }

        public void Reuse() {
            gameObject.SetActive(true);
        }

        public void TearDown() {
            Destroy(gameObject);
        }

        #region Locomotion
        public void Move(float xAxis) {
            moveConponent.Move(xAxis, moveSpeed);

            if (xAxis != 0 && xAxis != faceAxis) {
                RotateFace(xAxis);
            }
        }

        public void Jump() {
            moveConponent.Jump(jumpForce);
        }

        public void Fall(float garv) {
            moveConponent.Fall(garv);
        }

        void RotateFace(float xAxis) {
            Vector2 scale = transform.localScale;

            if (xAxis > 0) {
                faceAxis = 1;
                scale.x = 1;
            } else if (xAxis < 0) {
                faceAxis = -1;
                scale.x = -1;
            }
            transform.localScale = scale;
        }
        #endregion
    }
}