using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool IsMuted { get; private set; }

    // Day9最小：全体一括で止める（BGM/SE共通）
    public void ToggleMute()
    {
        SetMuted(!IsMuted);
    }

    public void SetMuted(bool muted)
    {
        IsMuted = muted;

        // 最小で確実に効く方式（AudioSource個別制御が不要）
        AudioListener.pause = muted;

        // 任意：無音をより確実にしたいなら volume も落とす（pauseだけでもOK）
        AudioListener.volume = muted ? 0f : 1f;
    }

    private void OnDisable()
    {
        // シーン遷移や停止時の安全策（必要なら）
        // AudioListener.pause = false;
        // AudioListener.volume = 1f;
    }
}
