using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class CameraSettings : ScriptableObject {

    public Vector3 cameraSensitivity;
    public float minY, maxY;
    public float minZ, maxZ;
    public Vector3 damping;
    public LayerMask collideLayer;
}
