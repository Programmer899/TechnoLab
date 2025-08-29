using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 300f;
    public float sensY = 300f;

    public Transform orientation;

    public Camera normalCamera;

    public bool thirdCameraPerspective;
    public Vector3 thirdCameraOriginPosition = new(0, 0, -3);
    public Camera thirdCamera;
    public LayerMask thirdPersonLayerMask;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetKeyDown(KeyCode.V))
        {
            thirdCameraPerspective = !thirdCameraPerspective;
        }

        UpdateCamera();
        UpdateThirdCamera();
    }

    void UpdateCamera()
    {
        if (thirdCameraPerspective)
        {
            normalCamera.gameObject.SetActive(false);
            thirdCamera.gameObject.SetActive(true);
        }
        else
        {
            normalCamera.gameObject.SetActive(true);
            thirdCamera.gameObject.SetActive(false);
        }
    }

    void UpdateThirdCamera()
    {
        Debug.Log(Vector3.Distance(normalCamera.transform.position, thirdCameraOriginPosition));

        RaycastHit hit;
        if (Physics.Raycast(normalCamera.transform.position, -normalCamera.transform.forward, out hit, Vector3.Distance(normalCamera.transform.position, thirdCameraOriginPosition), thirdPersonLayerMask))
        {
            thirdCamera.transform.position = hit.point + (normalCamera.transform.forward * 0.2f);
        }
        else
        {
            thirdCamera.transform.position = normalCamera.transform.position + normalCamera.transform.TransformDirection(thirdCameraOriginPosition);
        }
    }
}
