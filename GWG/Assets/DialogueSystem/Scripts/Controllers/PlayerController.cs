using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    private Vector3 direction;
    public InputAction movement;
    private CharacterController controller;
    private Vector3 gravity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        Vector3 convert = new Vector3(move.x, 0, move.y);
        direction = IsoVectorConvert(convert);
    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }


    private void Move()
    {
        controller.Move(direction * speed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        gravity.y += -10f * Time.deltaTime;
        controller.Move(gravity * Time.deltaTime);
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }
}