using UnityEngine;
using UnityEngine.InputSystem; // Yeni Input System API

public class DokunmaTest : MonoBehaviour
{
    void Update()
    {
        if (Touchscreen.current == null)
        {
            Debug.Log("⚠ Dokunmatik cihaz algılanmadı!");
            return;
        }

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Debug.Log($"📱 Dokunma Algılandı! Konum: {touchPosition}");
        }
    }
}
