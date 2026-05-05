using UnityEngine;
using UnityEngine.UIElements;

public class ForceVectorController
{
    private readonly VisualElement _container;
    private readonly VisualElement _line;
    private readonly VisualElement _arrow;
    private readonly Label _label; // THÊM: Biến quản lý Label

    public ForceVectorController(VisualElement rootElement)
    {
        _container = rootElement.Q<VisualElement>("force-vector-container");
        _line = rootElement.Q<VisualElement>("force-vector-line");
        _arrow = rootElement.Q<VisualElement>("force-vector-arrow");
        _label = rootElement.Q<Label>("force-vector-label"); // THÊM: Tìm Label trong UI

        if (_container == null || _line == null || _arrow == null)
        {
            Debug.LogError("ForceVectorController: Không tìm thấy các thành phần UI. Hãy kiểm tra lại UXML.");
        }
    }

    public void UpdateVector(float magnitude, float angleDegrees, Color color)
    {
        if (_container == null) return;

        _line.style.width = Mathf.Max(0, magnitude);
        _container.style.rotate = new StyleRotate(new Rotate(angleDegrees));

        _line.style.backgroundColor = color;
        _arrow.style.borderLeftColor = color;
        
        // THÊM: Đổi màu chữ theo màu của lực cho đồng bộ
        if (_label != null)
        {
            _label.style.color = color;
            
            // TÙY CHỌN: Nếu góc xoay > 90 hoặc < -90 độ (chữ bị lộn ngược),
            // bạn có thể lật chữ lại tại đây nếu muốn. Ở đây tôi giữ nguyên logic để không phức tạp hóa.
        }
    }

    public void UpdateVectorFromForce(Vector2 forceVector, float visualScale, Color color)
    {
        float magnitude = forceVector.magnitude * visualScale;
        float angle = Mathf.Atan2(forceVector.y, forceVector.x) * Mathf.Rad2Deg;

        UpdateVector(magnitude, angle, color);
    }

    // THÊM: Hàm cập nhật nội dung Text (Ví dụ: F1 = 5)
    public void SetLabelText(string text)
    {
        if (_label != null)
        {
            _label.text = text;
        }
    }

    public void SetVisible(bool isVisible)
    {
        if (_container != null)
        {
            _container.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}