using UnityEngine;

[System.Serializable]
public class CameraRequirements
{
    public Camera cam;
    public Transform target;
    public Transform playerTransform;
    public CameraSettings cameraSettings;
}

public class CameraController : MonoBehaviour {

    private ICameraState cameraState;

    public CameraRequirements cameraRequirements;

	// Use this for initialization
	void Start () {
        cameraState = new ThirdPersonCamera(cameraRequirements);
	}
	
	// Update is called once per frame
	void Update () {
        cameraState.Turn();
	}
}
