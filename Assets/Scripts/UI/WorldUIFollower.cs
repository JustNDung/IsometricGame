namespace UI
{
    using UnityEngine;

public class WorldUIFollower : MonoBehaviour
{
    public enum FollowMode
    {
        Transform,      // bám vào Transform (object / empty point)
        WorldPosition,  // bám vào 1 vị trí cố định trong world
        LocalPoint      // bám vào 1 điểm local trong object
    }

    [Header("Follow Settings")]
    public FollowMode followMode = FollowMode.Transform;

    public Transform targetTransform;
    public Vector3 worldPosition;
    public Vector3 localPoint; // dùng khi follow 1 điểm trên object

    [Header("UI")]
    public RectTransform uiElement;
    public Camera cam;

    [Header("Offset")]
    public Vector3 worldOffset;
    public Vector2 screenOffset;

    [Header("Behavior")]
    public bool hideWhenBehind = true;
    public bool smoothFollow = true;
    public float smoothSpeed = 10f;

    [Header("Scale With Distance")]
    public bool scaleByDistance = false;
    public float scaleMultiplier = 10f;
    public Vector2 scaleClamp = new Vector2(0.5f, 1.5f);

    private Vector3 velocity;

    void LateUpdate()
    {
        if (cam == null || uiElement == null) return;

        Vector3 worldPos = GetWorldPosition();
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);

        // Ẩn nếu sau camera
        if (hideWhenBehind && screenPos.z < 0)
        {
            if (uiElement.gameObject.activeSelf)
                uiElement.gameObject.SetActive(false);
            return;
        }

        if (!uiElement.gameObject.activeSelf)
            uiElement.gameObject.SetActive(true);

        // Thêm offset screen
        screenPos += (Vector3)screenOffset;

        // Smooth hoặc snap
        if (smoothFollow)
        {
            uiElement.position = Vector3.Lerp(
                uiElement.position,
                screenPos,
                Time.deltaTime * smoothSpeed
            );
        }
        else
        {
            uiElement.position = screenPos;
        }

        // Scale theo khoảng cách
        if (scaleByDistance)
        {
            float scale = Mathf.Clamp(
                scaleMultiplier / screenPos.z,
                scaleClamp.x,
                scaleClamp.y
            );

            uiElement.localScale = Vector3.one * scale;
        }
    }

    Vector3 GetWorldPosition()
    {
        switch (followMode)
        {
            case FollowMode.Transform:
                if (targetTransform != null)
                    return targetTransform.position + worldOffset;
                break;

            case FollowMode.WorldPosition:
                return worldPosition + worldOffset;

            case FollowMode.LocalPoint:
                if (targetTransform != null)
                    return targetTransform.TransformPoint(localPoint + worldOffset);
                break;
        }

        return Vector3.zero;
    }

    // ===== PUBLIC API =====

    public void SetTarget(Transform t)
    {
        followMode = FollowMode.Transform;
        targetTransform = t;
    }

    public void SetWorldPosition(Vector3 pos)
    {
        followMode = FollowMode.WorldPosition;
        worldPosition = pos;
    }

    public void SetLocalPoint(Transform t, Vector3 localPos)
    {
        followMode = FollowMode.LocalPoint;
        targetTransform = t;
        localPoint = localPos;
    }
}
}