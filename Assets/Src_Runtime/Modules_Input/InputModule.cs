using System;
using UnityEngine;

public class InputModule : MonoBehaviour {
    InputControls input;
    public InputControls Input => input;

    Vector2 moveAxis;
    public Vector2 MoveAxis => moveAxis;
    public bool IsMove => moveAxis.x != 0;

    bool isJump;
    public bool IsJump => isJump;

    public void Ctor() {
        input = new InputControls();
        input.Enable();
    }

    public void Process() {
        var world = input.World;

        // Move
        {
            moveAxis.x = world.MoveRight.ReadValue<float>() - world.MoveLeft.ReadValue<float>();
        }
        
        // Jump
        {
            isJump = world.Jump.WasPerformedThisFrame();
        }
    }
}