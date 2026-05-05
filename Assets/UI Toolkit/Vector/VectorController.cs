using UnityEngine;
using UnityEngine.UIElements;

public class VectorController
{
    private VisualElement _line;
    private VisualElement _handleStart;
    private VisualElement _handleEnd;
    
    private VisualElement _activeHandle;
    private bool _isDragging;

    public VectorController(VisualElement root)
    {
        // Khởi tạo references
        _line = root.Q<VisualElement>("line");
        _handleStart = root.Q<VisualElement>("handle-start");
        _handleEnd = root.Q<VisualElement>("handle-end");

        // Đặt vị trí mặc định
        SetHandlePosition(_handleStart, new Vector2(50, 50));
        SetHandlePosition(_handleEnd, new Vector2(200, 50));
        UpdateLine();

        // Đăng ký sự kiện
        _handleStart.RegisterCallback<PointerDownEvent>(e => OnPointerDown(e, _handleStart));
        _handleEnd.RegisterCallback<PointerDownEvent>(e => OnPointerDown(e, _handleEnd));
        
        root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        root.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt, VisualElement handle)
    {
        _activeHandle = handle;
        _isDragging = true;
        handle.CapturePointer(evt.pointerId);
        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!_isDragging || _activeHandle == null) return;

        // Cập nhật vị trí handle dựa trên vị trí chuột trong container
        SetHandlePosition(_activeHandle, evt.localPosition);
        UpdateLine();
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!_isDragging) return;
        
        _activeHandle?.ReleasePointer(evt.pointerId);
        _activeHandle = null;
        _isDragging = false;
    }

    private void SetHandlePosition(VisualElement handle, Vector2 pos)
    {
        handle.style.left = pos.x;
        handle.style.top = pos.y;
    }

    private void UpdateLine()
    {
        Vector2 start = new Vector2(_handleStart.resolvedStyle.left, _handleStart.resolvedStyle.top);
        Vector2 end = new Vector2(_handleEnd.resolvedStyle.left, _handleEnd.resolvedStyle.top);

        // Tính toán độ dài và góc xoay của đường kẻ
        Vector2 diff = end - start;
        float length = diff.magnitude;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        _line.style.width = length;
        _line.style.left = start.x;
        _line.style.top = start.y;
        
        // Xoay đường kẻ từ điểm gốc (Start Handle)
        _line.style.transformOrigin = new TransformOrigin(0, 0);
        _line.style.rotate = new Rotate(angle);
    }
}