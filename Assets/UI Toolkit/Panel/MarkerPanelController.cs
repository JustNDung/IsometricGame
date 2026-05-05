using UnityEngine.UIElements;

public class MarkerPanelController
{
    private readonly VisualElement _panel;
    private readonly VisualElement _marker;
    private readonly Label _statusLabel;
    private readonly Button _btnClose;

    private const string HIDDEN_CLASS = "panel-hidden";

    public MarkerPanelController(VisualElement panelRoot)
    {
        if (panelRoot == null) return;

        // Tìm các thành phần con bên trong root của Panel này
        _marker = panelRoot.Q<VisualElement>("Marker");
        _panel = panelRoot.Q<VisualElement>("InfoPanel");
        _statusLabel = panelRoot.Q<Label>(className: "description-text");
        _btnClose = panelRoot.Q<Button>("BtnClose");

        // --- ĐĂNG KÝ LOGIC SỰ KIỆN ---

        // 1. Hover vào Marker thì hiện Panel
        _marker?.RegisterCallback<MouseEnterEvent>(e => ShowPanel());

        // 2. Click vào Marker thì Toggle (Bật/Tắt) Panel
        _marker?.RegisterCallback<ClickEvent>(e => TogglePanel());

        // 3. Click nút Đóng thì ẩn Panel
        if (_btnClose != null)
        {
            _btnClose.clicked += HidePanel;
        }

        // Mặc định ẩn khi mới vào game
        HidePanel();
    }

    public void UpdateStatus(string status)
    {
        if (_statusLabel != null)
            _statusLabel.text = $"Trạng thái: {status}";
    }

    private void TogglePanel()
    {
        if (_panel == null) return;
        if (_panel.ClassListContains(HIDDEN_CLASS)) ShowPanel();
        else HidePanel();
    }

    private void ShowPanel() => _panel?.RemoveFromClassList(HIDDEN_CLASS);
    private void HidePanel() => _panel?.AddToClassList(HIDDEN_CLASS);
}