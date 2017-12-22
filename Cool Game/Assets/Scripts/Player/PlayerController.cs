using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    private CharacterController characterController;

    [SerializeField] private PlayerSettings playerSettings;

    [SerializeField] private AdvancedSettings advancedSettings;

    private bool isGrounded;

    private Vector2 input;  

    private Vector3 moveDirection = Vector3.zero;
    #endregion

    #region Properties

    public bool IsGrounded {
        get {
            RaycastHit hit;
            Vector3 origin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Ray ray = new Ray(origin, -transform.up);
            if (Physics.SphereCast(ray, characterController.radius, out hit,
                 (characterController.height / 2f) - characterController.radius + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore)) {

                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            return isGrounded;
        }
    }

    #endregion

    #region Class
    [System.Serializable]
    public struct PlayerSettings {

        public float walkSpeed;
        public float sprintSpeed;
    }

    [System.Serializable]
    public class AdvancedSettings
    {
        public float groundCheckDistance = 0.1f;
        public float gravityMultiplier = 2;
    }

    #endregion

    #region Methods
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInput();
        Move();
    }

    private void GetInput()
    {
        input.x = Input.GetAxis(InputManager.horizontalAxis);
        input.y = Input.GetAxis(InputManager.verticalAxis);
    }

    private void Move()
    {
        moveDirection = new Vector3(input.x, 0f, input.y);
        moveDirection *= playerSettings.walkSpeed;
        if(!IsGrounded)
            moveDirection += Physics.gravity * advancedSettings.gravityMultiplier * Time.deltaTime; 

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        float dist = (2f / 2f) - 0.5f + advancedSettings.groundCheckDistance;
        Debug.DrawLine(origin, origin + -transform.up * dist);
        Gizmos.DrawWireSphere(origin + -transform.up * dist, 0.5f);
    }
    #endregion
}
