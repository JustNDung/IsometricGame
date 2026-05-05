using UnityEngine;
using UnityEngine.UIElements;

public class ForceVectorController : MonoBehaviour
{
    private VisualElement _container;
    private VisualElement _shaft;
    private VisualElement _head;
    private Label _label;

    [Header("Cấu hình hiển thị")]
    public float pixelPerNewton = 15f; // 1N = 15 pixel độ dài
    public float minWidth = 10f;
    
    [Header("Màu sắc động")]
    public Color colorLow = new Color(0f, 0.9f, 1f); // Cyan
    public Color colorHigh = Color.red;
    public float forceMaxLimit = 50f; // Mốc lực để chuyển hẳn sang màu đỏ

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // Truy vấn chính xác các phần tử
        _container = root.Q<VisualElement>("ForceContainer");
        _shaft = root.Q<VisualElement>("ArrowShaft");
        _head = root.Q<VisualElement>("ArrowHead");
        _label = root.Q<Label>("ForceLabel");

        if (_container != null) _container.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Gọi hàm này từ script vật lý của bạn (ví dụ: Update hoặc FixedUpdate)
    /// </summary>
    /// <param name="forceVector">Vecto lực thực tế</param>
    public void UpdateForceDisplay(Vector2 forceVector)
    {
        float magnitude = forceVector.magnitude;

        // Ngưỡng tối thiểu để hiển thị
        if (magnitude < 0.05f)
        {
            _container.style.display = DisplayStyle.None;
            return;
        }

        _container.style.display = DisplayStyle.Flex;

        // 1. Xoay container theo hướng vecto
        float angle = Mathf.Atan2(forceVector.y, forceVector.x) * Mathf.Rad2Deg;
        _container.style.rotate = new Rotate(Angle.Degrees(angle));

        // 2. Cập nhật độ dài thân mũi tên
        _shaft.style.width = minWidth + (magnitude * pixelPerNewton);

        // 3. Tính toán và áp dụng màu sắc (Lerp)
        float t = Mathf.Clamp01(magnitude / forceMaxLimit);
        Color currentColor = Color.Lerp(colorLow, colorHigh, t);
        
        _shaft.style.backgroundColor = currentColor;
        _head.style.borderLeftColor = currentColor;
        _label.style.color = currentColor;
        _label.style.borderLeftColor = currentColor; // Viền nhãn cũng đổi màu

        // 4. Hiển thị text
        _label.text = $"{magnitude:F1} N";
    }

    // TEST NHANH TRONG INSPECTOR
    public Vector2 testForce;
    void Update() => UpdateForceDisplay(testForce);
}