using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    [SerializeField] private SOCamera _mainCamera;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody _characterRigidbody;

    public void Move(Vector2 playerInput)
    {
        Vector3 characterVelocity = _characterRigidbody.velocity;
        Vector3 newMovement = new Vector3(playerInput.x, 0, playerInput.y) * _movementSpeed;
        Quaternion rotation = Quaternion.Euler(0, _mainCamera.value.transform.eulerAngles.y, 0);
        Vector3 rotatedMovement = rotation * newMovement;
        Vector3 clampedMovement = characterVelocity + rotatedMovement;
        clampedMovement = new Vector3(Mathf.Clamp(clampedMovement.x, -_maxSpeed, _maxSpeed), 0, Mathf.Clamp(clampedMovement.z, -_maxSpeed, _maxSpeed));
        _characterRigidbody.velocity = clampedMovement;
    }

    public void RestMovement()
    {
        _characterRigidbody.velocity = Vector3.zero;
    }

}
