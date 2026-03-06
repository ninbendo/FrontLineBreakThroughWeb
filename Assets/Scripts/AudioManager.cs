using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool IsMuted { get; private set; }

    private float _volumeBeforeMute = 1f;

    // Day9最小：全体一括で止める（BGM/SE共通）
    public void ToggleMute()
    {
        SetMuted(!IsMuted);
    }

    public void SetMuted(bool muted)
    {
        // 同じ状態への再設定は何もしない
        if (IsMuted == muted)
        {
            return;
        }

        if (muted)
        {
            // ミュート前の状態を退避
            _volumeBeforeMute = AudioListener.volume;
            AudioListener.pause = true;
            AudioListener.volume = 0f;
            IsMuted = true;
            return;
        }

        // アンミュート時は退避した値へ戻す
        AudioListener.pause = false;
        AudioListener.volume = _volumeBeforeMute;
        IsMuted = false;
    }

    private void OnDestroy()
    {
        // ミュート中のまま破棄されると、グローバル状態が残るため復元する
        if (!IsMuted)
        {
            return;
        }

        AudioListener.pause = false;
        AudioListener.volume = _volumeBeforeMute;
        IsMuted = false;
    }
}
