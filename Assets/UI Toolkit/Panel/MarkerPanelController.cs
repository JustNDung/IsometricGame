using UnityEngine;
using UnityEngine.UIElements;

public class MarkerPanelController : MonoBehaviour
{
    private VisualElement _marker;
    private VisualElement _infoPanel;
    private Button _btnClose;

    // Tên class trong USS
    private const string HIDDEN_CLASS = "panel-hidden";

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Tìm các phần tử theo tên đã đặt trong UXML
        _marker = root.Q<VisualElement>("Marker");
        _infoPanel = root.Q<VisualElement>("InfoPanel");
        _btnClose = root.Q<Button>("BtnClose");

        // Đăng ký sự kiện
        _marker.RegisterCallback<MouseEnterEvent>(e => ShowPanel());
        _marker.RegisterCallback<ClickEvent>(e => TogglePanel());
        _btnClose.clicked += HidePanel;
    }

    void Start()
    {
        // Khi Game bắt đầu, ẩn ngay lập tức
        HidePanel();
    }

    private void TogglePanel()
    {
        if (_infoPanel.ClassListContains(HIDDEN_CLASS))
            ShowPanel();
        else
            HidePanel();
    }

    private void ShowPanel()
    {
        _infoPanel.RemoveFromClassList(HIDDEN_CLASS);
    }

    private void HidePanel()
    {
        // Thêm class panel-hidden để kích hoạt display: none và opacity: 0
        if (!_infoPanel.ClassListContains(HIDDEN_CLASS))
        {
            _infoPanel.AddToClassList(HIDDEN_CLASS);
        }
    }
}