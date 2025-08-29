using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraObj;

    private void Update()
    {
        if (cameraObj == null) return;
        transform.position = cameraObj.position;
    }
}
