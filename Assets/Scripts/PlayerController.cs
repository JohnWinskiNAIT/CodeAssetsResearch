using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float movementSpeed, rotationSpeed;

    Vector2 moveValue, rotateValue;

    float fireValue;

    [SerializeField] InputAction moveAction, rotateAction, fireAction;

    [SerializeField] GameObject weaponPivot;

    Vector3 angles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeed = 3.0f;
        rotationSpeed = 200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        rotateValue = rotateAction.ReadValue<Vector2>();
        fireValue = fireAction.ReadValue<float>();

        transform.Rotate(Vector3.up, rotateValue.x * rotationSpeed * Time.fixedDeltaTime);
        weaponPivot.transform.Rotate(Vector3.right, -rotateValue.y * rotationSpeed * Time.fixedDeltaTime);

        angles = weaponPivot.transform.eulerAngles;

        if (angles.x > 60.0f && angles.x < 180.0f)
        {
            weaponPivot.transform.localEulerAngles = new Vector3(60.0f, 0, 0);
        }
        if (angles.x < 300.0f && angles.x > 180.0f)
        {
            weaponPivot.transform.localEulerAngles = new Vector3(300.0f, 0, 0);
        }

        if (fireValue == -1)
        {
            Debug.Log("P fire");
            BroadcastMessage("PrimaryFire");
        }

        if (fireValue == 1)
        {
            BroadcastMessage("SecondaryFire");
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * movementSpeed * Time.fixedDeltaTime);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    private void OnEnable()
    {
        moveAction.Enable();
        rotateAction.Enable();
        fireAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
        fireAction.Disable();
    }
}
