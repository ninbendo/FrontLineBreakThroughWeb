using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : MonoBehaviour
{
    [Header("Drag Sensitivity")]
    [SerializeField] private float mouseDragSensitivity = 0.02f;
    [SerializeField] private float touchDragSensitivity = 0.02f;

    [Header("Debug")]
    [SerializeField] private bool enableDebugLog = false;

    public float CurrentHorizontal { get; private set; }

    private void Update()
    {
        CurrentHorizontal = 0f;

        bool usedPointerInput = false;

        // スマホ：タッチドラッグ
        var touchscreen = Touchscreen.current;
        if (touchscreen != null && touchscreen.primaryTouch.press.isPressed)
        {
            float dx = touchscreen.primaryTouch.delta.ReadValue().x;
            if (Mathf.Abs(dx) > 0.01f)
            {
                CurrentHorizontal = Mathf.Clamp(dx * touchDragSensitivity, -1f, 1f);
                usedPointerInput = true;
            }
        }

        // PC：マウスドラッグ
        if (!usedPointerInput)
        {
            var mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.isPressed)
            {
                float dx = mouse.delta.ReadValue().x;
                if (Mathf.Abs(dx) > 0.01f)
                {
                    CurrentHorizontal = Mathf.Clamp(dx * mouseDragSensitivity, -1f, 1f);
                    usedPointerInput = true;
                }
            }
        }

        // 保険：キーボード左右（MVP確認用）
        if (!usedPointerInput)
        {
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
                {
                    CurrentHorizontal -= 1f;
                }

                if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
                {
                    CurrentHorizontal += 1f;
                }

                CurrentHorizontal = Mathf.Clamp(CurrentHorizontal, -1f, 1f);
            }
        }

        if (enableDebugLog && Mathf.Abs(CurrentHorizontal) > 0.01f)
        {
            Debug.Log($"[PlayerInputAdapter] CurrentHorizontal = {CurrentHorizontal:F2}");
        }
    }
}
