using System;
using UnityEngine;

namespace GameClient {
    public class RoleMoveComponent {
        Rigidbody2D rb;
        public Vector2 velocity => rb.velocity;

        public void Inject(Rigidbody2D rb) {
            this.rb = rb;
        }

        public void Move(float xAxis, float moveSpeed) {
            var velo = rb.velocity;
            velo.x = xAxis * moveSpeed;
            rb.velocity = velo;
        }

        public void Climb(float yAxis, float climbSpeed) {
            var velo = rb.velocity;
            velo.y = yAxis * climbSpeed;
            rb.velocity = velo;
        }

        public void Jump(float jumpForce) {
            var velo = rb.velocity;
            velo.y = jumpForce;
            rb.velocity = velo;
        }

        public void Fall(float garv) {
            var velo = rb.velocity;
            velo.y -= garv * Time.deltaTime;
            rb.velocity = velo;
        }
    }
}