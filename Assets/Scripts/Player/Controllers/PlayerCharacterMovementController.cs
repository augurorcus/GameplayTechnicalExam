using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterMovementController : PlayerController<CharacterMovement>
{

    public override void ControlUpdate()
    {
        if (playerInputActions.PlayerCharacterInput.Move.IsPressed())
        {
            Vector2 playerMovementInput = playerInputActions.PlayerCharacterInput.Move.ReadValue<Vector2>();
            controlledObject.Move(playerMovementInput);
        }else
        {
            controlledObject.RestMovement();
        }
    }
}
