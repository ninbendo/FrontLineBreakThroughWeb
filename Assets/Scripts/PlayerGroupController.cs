using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroupController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float forwardSpeed = 3.0f;
    [SerializeField] private float sideSpeed = 5.0f;

    [Header("X Clamp")]
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX = 2.5f;

    private void Update()
    {
        float dt = Time.deltaTime;

        // 自動前進（Y+）
        Vector3 pos = transform.position;
        pos.y += forwardSpeed * dt;

        // 左右入力（新Input System）
        float xInput = 0f;
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.leftArrowKey.isPressed)  xInput -= 1f;
            if (keyboard.rightArrowKey.isPressed) xInput += 1f;
        }

        // 左右移動
        pos.x += xInput * sideSpeed * dt;

        // X範囲クランプ
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        // 反映
        transform.position = pos;
    }
}