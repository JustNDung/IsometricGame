using UnityEngine;
using UnityEngine.UIElements;

public class ForceVectorController
{
    private readonly VisualElement _container;
    private readonly VisualElement _line;
    private readonly VisualElement _arrow;
    private readonly Label _label;

    public ForceVectorController(VisualElement rootElement)
    {
        // SỬA TẠI ĐÂY: Gán trực tiếp rootElement vào _container
        _container = rootElement; 

        // Tìm các thành phần con dựa trên class hoặc name (nên dùng class để linh hoạt)
        _line = rootElement.Q<VisualElement>(className: "force-vector-line");
        _arrow = rootElement.Q<VisualElement>(className: "force-vector-arrow");
        _label = rootElement.Q<Label>(className: "force-vector-label");

        // Kiểm tra lỗi nếu không tìm thấy thành phần con
        if (_container == null || _line == null || _arrow == null)
        {
            Debug.LogError($"ForceVectorController: Không tìm thấy các thành phần con trong {rootElement.name}. Hãy kiểm tra lại UXML.");
        }
    }

    public void UpdateVector(float magnitude, float angleDegrees, Color color)
    {
        if (_container == null) return;

        // Cập nhật độ dài
        _line.style.width = Mathf.Max(0, magnitude);
        
        // Cập nhật góc xoay
        _container.style.rotate = new StyleRotate(new Rotate(angleDegrees));

        // Cập nhật màu sắc
        _line.style.backgroundColor = color;
        _arrow.style.borderLeftColor = color;
        
        if (_label != null)
        {
            _label.style.color = color;
        }
    }

    public void UpdateVectorFromForce(Vector2 forceVector, float visualScale, Color color)
    {
        float magnitude = forceVector.magnitude * visualScale;
    
        // Đảo ngược Y (-forceVector.y) để phù hợp với hệ tọa độ UI Toolkit
        // Trong UI Toolkit, Y dương là đi xuống, nên ta cần đảo dấu để Y dương của Vector hướng lên trên.
        float angle = Mathf.Atan2(-forceVector.y, forceVector.x) * Mathf.Rad2Deg;

        UpdateVector(magnitude, angle, color);
    }

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