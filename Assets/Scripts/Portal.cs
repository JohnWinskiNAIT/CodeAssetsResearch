using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject otherPortal;

    [SerializeField] GameObject teleportPoint;

    Collider trigger;

    float timeStamp;

    private void Update()
    {
        if (trigger.enabled == false && Time.time > timeStamp + 0.5f)
        {
            trigger.enabled = true;
        }
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
