using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public static bool TryRayCastHit(out RaycastHit hitObject)
    {
#if ENABLE_INPUT_SYSTEM
        // Touch (mobile)
        if (UnityEngine.InputSystem.Touchscreen.current != null)
        {
            var touch = UnityEngine.InputSystem.Touchscreen.current.primaryTouch;
            
            if (touch.press.wasPressedThisFrame)
            {
                Vector2 touchPosition = touch.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                
                if (Physics.Raycast(ray, out hitObject))
                {
                    return true;
                }
            }
        }
        // Mouse (editor/fallback)
        else if (UnityEngine.InputSystem.Mouse.current != null && 
                 UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
            
            if (Physics.Raycast(ray, out hitObject))
            {
                return true;
            }
        }
#endif
        
        hitObject = default;
        return false;
    }
}