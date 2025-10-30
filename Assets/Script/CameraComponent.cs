using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    public static float focalLenth = 10f;

    private void Awake()
    {
        if (Camera.main != null)
            Camera.main.orthographic = true;
    }
}
