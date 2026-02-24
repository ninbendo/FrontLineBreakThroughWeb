using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : MonoBehaviour
{
    private void Update()
    {
        // 動いている確認（2秒ごと）
        if (Time.frameCount % 120 == 0)
        {
            Debug.Log("[PlayerInputAdapter] Update running");
        }

        // キーボードが取れない環境対策
        if (Keyboard.current == null)
        {
            return;
        }

        // 押した瞬間だけログ（連打を防いで見やすくする）
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            Debug.Log("[PlayerInputAdapter] LEFT pressed");
        }

        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            Debug.Log("[PlayerInputAdapter] RIGHT pressed");
        }
    }
}