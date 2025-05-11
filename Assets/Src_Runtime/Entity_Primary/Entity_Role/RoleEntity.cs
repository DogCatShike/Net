using System;
using UnityEngine;

namespace GameClient {
    public class RoleEntity : MonoBehaviour {
        // 基础
        public IDSignature idSig;
        public int typeID;
        [SerializeField] Rigidbody2D rb;

        RoleState state;
        public RoleState State => state;

        // 属性
        float moveSpeed;
        public void Set_MoveSpeed(float value) => moveSpeed = value;
        float climbSpeed;
        public void Set_ClimbSpeed(float value) => climbSpeed = value;
        float jumpForce;
        public void Set_JumpForce(float value) => jumpForce = value;

        int jumpTimes;
        int maxJumpTimes;
        public void Set_MaxJumpTimes(int value) => maxJumpTimes = value;
        public bool canJump => jumpTimes < maxJumpTimes;

        float faceAxis;

        bool isCollision;
        public bool IsCollision => isCollision;

        bool canClimb;
        public bool CanClimb => canClimb;

        // Com
        [SerializeField] RoleInputComponent inputComponent;
        public RoleInputComponent InputComponent => inputComponent;

        [SerializeField] RoleMoveComponent moveConponent;
        public RoleMoveComponent MoveConponent => moveConponent;

        public void Ctor() {
            inputComponent = new RoleInputComponent();
            moveConponent = new RoleMoveComponent();

            moveConponent.Inject(rb);

            jumpTimes = 0;
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

        public void Climb(float yAxis) {
            moveConponent.Climb(yAxis, climbSpeed);
        }

        public void Jump() {
            moveConponent.Jump(jumpForce);
            jumpTimes += 1;
        }

        public void Jump_Reset() {
            jumpTimes = 0;
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

        public void Set_State(RoleState newState) {
            state = newState;
        }

        public void Add_State(RoleState newState) {
            state |= newState;
        }

        public void Remove_State(RoleState newState) {
            state &= ~newState;
        }

        void OnCollisionEnter2D(Collision2D collision) {
            isCollision = true;

            if (collision.gameObject.CompareTag("Ground")) {
                Jump_Reset();
            }
        }

        void OnCollisionExit2D(Collision2D collision) {
            isCollision = false;
        }

        void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Stair")) {
                canClimb = true;
            }
        }

        void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Stair")) {
                canClimb = false;
            }
        }
        #endregion
    }
}