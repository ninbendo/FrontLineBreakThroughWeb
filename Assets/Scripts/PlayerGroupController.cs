using UnityEngine;

public class PlayerGroupController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float sideSpeed = 5.0f;

    [Header("X Clamp")]
    [SerializeField] private float minX = -5.0f;
    [SerializeField] private float maxX = 5.0f;

    [Header("References")]
    [SerializeField] private PlayerInputAdapter inputAdapter;

    private void Awake()
    {
        if (inputAdapter == null)
        {
            inputAdapter = GetComponent<PlayerInputAdapter>();
        }
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        Vector3 pos = transform.position;

        float xInput = 0f;
        bool isPointerInput = false;

        if (inputAdapter != null)
        {
            xInput = inputAdapter.CurrentHorizontal;
            isPointerInput = inputAdapter.IsPointerInput;
        }

        if (isPointerInput)
        {
            pos.x += xInput * sideSpeed;
        }
        else
        {
            pos.x += xInput * sideSpeed * dt;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}
