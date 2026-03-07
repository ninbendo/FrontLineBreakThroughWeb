using TMPro;
using UnityEngine;

public class BuildVersionLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private string prefix = "Build ";

    private void Awake()
    {
        if (targetText == null)
        {
            targetText = GetComponent<TMP_Text>();
        }

        if (targetText != null)
        {
            targetText.text = $"{prefix}{Application.version}";
        }
    }
}
