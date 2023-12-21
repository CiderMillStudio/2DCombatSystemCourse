using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    void Update()
    {
        FaceMouse();
    }

    void FaceMouse()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction; //.right refers to the X axis itself, not the direction right 
    }
}
