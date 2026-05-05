using UnityEngine;
using UnityEngine.UIElements;

public class WrenchExperimentUIController : MonoBehaviour
{
    // Các Controller riêng biệt cho từng vector lực trong UXML
    private ForceVectorController _f1aController;
    private ForceVectorController _f2aController;
    private ForceVectorController _f1bController;
    private ForceVectorController _f2bController;

    [Header("Dữ liệu Lực - Nhóm A")]
    public Vector2 forceF1a = new Vector2(0, 100);
    public Vector2 forceF2a = new Vector2(0, 100);

    [Header("Dữ liệu Lực - Nhóm B")]
    public Vector2 forceF1b = new Vector2(0, 100);
    public Vector2 forceF2b = new Vector2(0, 100);

    [Header("Cấu hình hiển thị")]
    public float visualScale = 1.0f;
    public Color colorGroupA = Color.red;
    public Color colorGroupB = Color.blue;

    private void OnEnable()
    {
        // Lấy rootVisualElement từ UIDocument gắn trên cùng GameObject
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        VisualElement f1aEl = root.Q<VisualElement>("F1a");
        if (f1aEl != null) _f1aController = new ForceVectorController(f1aEl);

        VisualElement f2aEl = root.Q<VisualElement>("F2a");
        if (f2aEl != null) _f2aController = new ForceVectorController(f2aEl);

        VisualElement f1bEl = root.Q<VisualElement>("F1b");
        if (f1bEl != null) _f1bController = new ForceVectorController(f1bEl);

        VisualElement f2bEl = root.Q<VisualElement>("F2b");
        if (f2bEl != null) _f2bController = new ForceVectorController(f2bEl);
    }

    private void Update()
    {
        // Cập nhật Nhóm A
        UpdateVectorUI(_f1aController, forceF1a, "F1a", colorGroupA);
        UpdateVectorUI(_f2aController, forceF2a, "F2a", colorGroupA);

        // Cập nhật Nhóm B
        UpdateVectorUI(_f1bController, forceF1b, "F1b", colorGroupB);
        UpdateVectorUI(_f2bController, forceF2b, "F2b", colorGroupB);
    }

    /// <summary>
    /// Hàm hỗ trợ cập nhật cả hình ảnh và nhãn chữ cho từng Vector
    /// </summary>
    private void UpdateVectorUI(ForceVectorController controller, Vector2 force, string labelPrefix, Color color)
    {
        if (controller == null) return;

        // Cập nhật độ dài, hướng và màu sắc
        controller.UpdateVectorFromForce(force, visualScale, color);

        // Cập nhật nội dung nhãn (ví dụ: F1a = 100)
        float magnitude = Mathf.Round(force.magnitude * 10f) / 10f;
        controller.SetLabelText($"{labelPrefix} = {magnitude}");
    }
}