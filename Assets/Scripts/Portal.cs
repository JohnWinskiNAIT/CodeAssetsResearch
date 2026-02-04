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
    
    public void Teleport(GameObject obj, Vector3 velocity, Vector3 rot)
    {
        obj.transform.position = teleportPoint.transform.position + Vector3.down;
        //obj.transform.rotation = teleportPoint.transform.rotation;
        obj.transform.eulerAngles = rot - transform.eulerAngles;
        obj.transform.Rotate(new Vector3(0, 180, 0));
        timeStamp = Time.time;
        trigger.enabled = false;

        obj.GetComponent<Rigidbody>().linearVelocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 rot = transform.eulerAngles - other.transform.eulerAngles;

        Vector3 velocity = other.gameObject.GetComponent<Rigidbody>().linearVelocity;
        otherPortal.GetComponent<Portal>().Teleport(other.gameObject, velocity, rot);        
    }
}
