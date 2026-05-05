using UnityEngine;
using UnityEngine.UIElements;

public class DoorExperimentUIController : MonoBehaviour
{
    private MarkerPanelController _hingeLogic;
    private MarkerPanelController _handleLogic;

    [Header("Dữ liệu Test")]
    public Vector2 currentForce;
    public string hingeStatus = "Ổn định";
    public string handleStatus = "Sẵn sàng";

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Khởi tạo logic cho cụm Bản lề
        var hingeEl = root.Q<VisualElement>("HingeInfoPanel");
        if (hingeEl != null) _hingeLogic = new MarkerPanelController(hingeEl);

        // Khởi tạo logic cho cụm Tay nắm
        var handleEl = root.Q<VisualElement>("HandleInfoPanel");
        if (handleEl != null) _handleLogic = new MarkerPanelController(handleEl);
        
    }

    private void Update()
    {
        _hingeLogic?.UpdateStatus(hingeStatus);
        _handleLogic?.UpdateStatus(handleStatus);
    }
}