using UnityEngine;

public enum CameraState {
    FirstPerson,
    ThirdPerson
}

public interface ICameraState {

    Camera Cam { get; }
    Transform Target { get; }
    Transform PlayerTransform { get; }

    void GetMouseInput();

    void Turn();
}