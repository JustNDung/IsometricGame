using UnityEngine;
using UnityEngine.UIElements;

public class ForceVectorController
{
    private readonly VisualElement _container;
    private readonly VisualElement _line;
    private readonly VisualElement _arrow;

    // Truyền VisualElement gốc (hoặc document) vào constructor
    public ForceVectorController(VisualElement rootElement)
    {
        _container = rootElement.Q<VisualElement>("force-vector-container");
        _line = rootElement.Q<VisualElement>("force-vector-line");
        _arrow = rootElement.Q<VisualElement>("force-vector-arrow");

        if (_container == null || _line == null || _arrow == null)
        {
            Debug.LogError("ForceVectorController: Không tìm thấy các thành phần UI. Hãy kiểm tra lại UXML.");
        }
    }

    /// <summary>
    /// Cập nhật Vector lực trực tiếp bằng các chỉ số
    /// </summary>
    /// <param name="magnitude">Độ lớn (chiều dài thân vector)</param>
    /// <param name="angleDegrees">Góc quay (độ)</param>
    /// <param name="color">Màu sắc của vector</param>
    public void UpdateVector(float magnitude, float angleDegrees, Color color)
    {
        if (_container == null) return;

        // 1. Thay đổi độ dài (chiều rộng của thân vector)
        // Lưu ý: Có thể dùng Mathf.Max để đảm bảo độ dài không bị âm
        _line.style.width = Mathf.Max(0, magnitude);

        // 2. Thay đổi hướng (góc xoay)
        _container.style.rotate = new StyleRotate(new Rotate(angleDegrees));

        // 3. Thay đổi màu sắc cho cả thân và mũi tên
        _line.style.backgroundColor = color;
        _arrow.style.borderLeftColor = color;
    }

    /// <summary>
    /// Hàm tiện ích: Cập nhật Vector dựa trên một Vector2 (ví dụ: Rigidbody2D.velocity hoặc force)
    /// </summary>
    /// <param name="forceVector">Vector lực</param>
    /// <param name="visualScale">Hệ số nhân để scale độ dài UI so với độ lớn thực tế</param>
    /// <param name="color">Màu sắc</param>
    public void UpdateVectorFromForce(Vector2 forceVector, float visualScale, Color color)
    {
        float magnitude = forceVector.magnitude * visualScale;
        
        // Tính góc quay từ Vector2 (chuyển từ Radian sang Độ)
        float angle = Mathf.Atan2(forceVector.y, forceVector.x) * Mathf.Rad2Deg;

        UpdateVector(magnitude, angle, color);
    }

    /// <summary>
    /// Ẩn/Hiện Vector
    /// </summary>
    public void SetVisible(bool isVisible)
    {
        if (_container != null)
        {
            _container.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}