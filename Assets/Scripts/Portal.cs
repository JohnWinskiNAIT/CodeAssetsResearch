using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject otherPortal;

    [SerializeField] GameObject teleportPoint;

    [SerializeField] GameObject camera, player;

    Collider trigger;

    float timeStamp;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (trigger.enabled == false && Time.time > timeStamp + 0.5f)
        {
            trigger.enabled = true;
        }

        Vector3 lookVector = (player.transform.position + Vector3.up * 1.5f) - otherPortal.transform.position;

        //Quaternion rotation = Quaternion.AngleAxis(otherPortal.transform.eulerAngles.y - transform.eulerAngles.y, Vector3.up);
        camera.transform.LookAt(transform.position + new Vector3(lookVector.x, -lookVector.y, lookVector.z));

        camera.transform.Rotate(Vector3.up, transform.eulerAngles.y - otherPortal.transform.eulerAngles.y, Space.World);
    }
    public void SetOtherPortal(GameObject portal)
    {
        otherPortal = portal;
        trigger = GetComponent<Collider>();
    }
    
    public void Teleport(GameObject obj, Vector3 velocity, Quaternion relativeRotation, Vector3 rot)
    {
        obj.transform.position = teleportPoint.transform.position - obj.transform.up;
        //obj.transform.eulerAngles = transform.eulerAngles + rot;

        obj.transform.rotation = transform.rotation * relativeRotation;

        if (obj.transform.rotation.eulerAngles.z == 180.0f)
        {
            obj.transform.Rotate(new Vector3(0, 0, 180));
            //obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, 0);
        }
        else
        {
            obj.transform.Rotate(new Vector3(0, 180, 0));
        }
        timeStamp = Time.time;
        trigger.enabled = false;

        obj.GetComponent<Rigidbody>().linearVelocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (otherPortal.activeSelf == true)
        {
            Rigidbody rbody = other.gameObject.GetComponent<Rigidbody>();

            if (rbody != null)
            {
                Quaternion relativeRotation;
                relativeRotation = Quaternion.Inverse(transform.rotation) * other.transform.rotation;
                Vector3 rot = transform.eulerAngles - other.transform.eulerAngles;

                Vector3 velocity = other.gameObject.GetComponent<Rigidbody>().linearVelocity;
                otherPortal.GetComponent<Portal>().Teleport(other.gameObject, velocity, relativeRotation, rot);
            }
        }
                
    }
}
