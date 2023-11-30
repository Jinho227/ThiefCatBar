using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private Vector3 dragOrigin;
    public float dragSensitivity;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;
        Vector3 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        Camera.main.transform.position = ClampCamera(Camera.main.transform.position + difference*dragSensitivity);

    }

    Vector3 ClampCamera(Vector3 targetPosition)
    {
        targetPosition.x = Mathf.Clamp(targetPosition.x, 0, 0);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -5.2f, 0);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -10, -10);
        return targetPosition;
    }
}
