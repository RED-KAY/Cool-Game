using UnityEngine;

public class ThirdPersonCamera : ICameraState {

    #region Variables
    private CameraRequirements cameraRequirements;
    private Vector3 mouseInput = new Vector3();
    private Vector3 newCameraPosition = new Vector3();
    private Vector3 dampVelocities = new Vector3();
    //private Camera cameraRequirementscam;
    //private Transform target;
    //private Transform playerTransform;
    //private CameraSettings cameraSettings;
    #endregion

    #region Properties
    public Camera Cam { get { return cameraRequirements.cam; } }
    public Transform Target { get { return cameraRequirements.target; } }
    public Transform PlayerTransform { get { return cameraRequirements.playerTransform; } }
    #endregion

    #region Methods

    public ThirdPersonCamera(CameraRequirements camReq) {
        //cam = _cam;
        //target = _target;
        //playerTransform = _playerTransform;
        //cameraSettings = _cameraSettings;
        cameraRequirements = camReq;
    }

    public void GetMouseInput() {
        mouseInput.x = (mouseInput.x - Input.GetAxis(InputManager.mouseX) * cameraRequirements.cameraSettings.cameraSensitivity.x * Time.deltaTime) % 360;
        mouseInput.y = Mathf.Clamp(mouseInput.y + Input.GetAxis(InputManager.mouseY) * cameraRequirements.cameraSettings.cameraSensitivity.y * Time.deltaTime, cameraRequirements.cameraSettings.minY, cameraRequirements.cameraSettings.maxY);
        mouseInput.z = Mathf.Clamp(mouseInput.z + Input.GetAxis(InputManager.scrollWheel) * cameraRequirements.cameraSettings.cameraSensitivity.z * Time.deltaTime, cameraRequirements.cameraSettings.minZ, cameraRequirements.cameraSettings.maxZ);
        Debug.Log(mouseInput.z);
    }

    public void Turn() {

        GetMouseInput();
        //if(mouseInput != Vector3.zero)
        //    Debug.Log(mouseInput.x);
        //Debug.Log("Quaternion.AngleAxis X Vector3: " + Quaternion.AngleAxis(-mouseInput.y, Vector3.right) * -Vector3.forward * 10);

        newCameraPosition.x = Mathf.SmoothDampAngle(newCameraPosition.x, mouseInput.x, ref dampVelocities.x, cameraRequirements.cameraSettings.damping.x);
        newCameraPosition.y = Mathf.SmoothDampAngle(newCameraPosition.y, mouseInput.y, ref dampVelocities.y, cameraRequirements.cameraSettings.damping.y);
        newCameraPosition.z = Mathf.SmoothDamp(newCameraPosition.z, mouseInput.z, ref dampVelocities.z, cameraRequirements.cameraSettings.damping.z);

        //Clip(cameraRequirements.cam);

        cameraRequirements.cam.transform.position = cameraRequirements.target.position + Quaternion.AngleAxis(-newCameraPosition.x, Vector3.up) * (Quaternion.AngleAxis(-newCameraPosition.y, Vector3.right) * -Vector3.forward * 10);
        cameraRequirements.cam.transform.LookAt(cameraRequirements.target);
    }

    public void Follow() {


    }

    public void Zoom() { }

    public void Clip(Camera cam) {
        Vector3[] frustumFarPoints = new Vector3[4], frustumNearPoints = new Vector3[4];
        //cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumFarPoints);
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumNearPoints);

        

        for (int i = 0; i < frustumNearPoints.Length; i++)
        {
            //frustumFarPoints[i] = cam.transform.TransformPoint(frustumFarPoints[i]);
            frustumNearPoints[i] = cam.transform.TransformPoint(frustumNearPoints[i]);
        }

        Debug.DrawRay(frustumFarPoints[0], frustumNearPoints[0] - frustumFarPoints[0], Color.green);
        Debug.DrawRay(frustumFarPoints[1], frustumNearPoints[1] - frustumFarPoints[1], Color.green);
        Debug.DrawRay(frustumFarPoints[2], frustumNearPoints[2] - frustumFarPoints[2], Color.green);
        Debug.DrawRay(frustumFarPoints[3], frustumNearPoints[3] - frustumFarPoints[3], Color.green);
        Debug.DrawRay(frustumFarPoints[3], frustumNearPoints[3] - frustumFarPoints[3], Color.green);

        for (int i = 0; i < frustumFarPoints.Length; i++)
        {
            Ray ray = new Ray(frustumFarPoints[i], frustumNearPoints[i] - frustumFarPoints[i]);
            RaycastHit hit;
            float rayDistance = Vector3.Distance(frustumFarPoints[i], frustumNearPoints[i]);

            if (Physics.Raycast(ray, out hit, rayDistance, cameraRequirements.cameraSettings.collideLayer, QueryTriggerInteraction.Ignore)) {
                Debug.Log("f");
                Debug.DrawRay(frustumFarPoints[i], frustumNearPoints[i] - frustumFarPoints[i], Color.red);
            }
        }
    }
    #endregion             
}
