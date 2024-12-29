using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivity = 60.0f;
    public float rotationSpeed = 4.0f;
    public float cameraDistance = 2.0f;
    public float cameraHeight = 1.0f;

    private float rotHorizontal;
    private float rotVertical;
    private float currentCameraVerticalAngle = 0f;
    public float cameraVerticalAngleLimit = 70f;

    public GameObject characterCamPos;

    void Update()
    {
        // Input for camera rotation
        rotHorizontal = Input.GetAxisRaw("Mouse X") * sensitivity;
        rotVertical = Input.GetAxisRaw("Mouse Y") * sensitivity;

        CameraRotation(rotHorizontal, rotVertical);
    }

    void CameraRotation(float rotHorizontal, float rotVertical)
    {
        // Horizontal rotation (left/right)
        transform.Rotate(0, rotHorizontal * Time.deltaTime, 0);

        // Vertical rotation (up/down)
        currentCameraVerticalAngle -= rotVertical * Time.deltaTime;
        currentCameraVerticalAngle = Mathf.Clamp(currentCameraVerticalAngle, -cameraVerticalAngleLimit, cameraVerticalAngleLimit);

        // Update the camera's rotation
        transform.localRotation = Quaternion.Euler(currentCameraVerticalAngle, transform.eulerAngles.y, 0);

        // Update camera position
        Vector3 targetPosition = characterCamPos.transform.position - characterCamPos.transform.forward * cameraDistance + Vector3.up * cameraHeight;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);
        transform.LookAt(characterCamPos.transform.position + Vector3.up * 1f);
    }
}
