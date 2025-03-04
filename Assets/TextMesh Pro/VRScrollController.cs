using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class VRScrollController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public XRNode controllerNode = XRNode.RightHand; // Oder XRNode.LeftHand für linken Controller
    public float scrollSpeed = 0.1f;

    private void Update()
    {
        Vector2 axisInput = Vector2.zero;
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out axisInput))
        {
            float scrollAmount = axisInput.y * scrollSpeed;
            scrollRect.verticalNormalizedPosition += scrollAmount;
        }
    }
}

