using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject otherPortal;

    [SerializeField] GameObject teleportPoint;

    public void SetOtherPortal(GameObject portal)
    {
        otherPortal = portal;
    }
    
    public Vector3 Teleport()
    {
        return teleportPoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = otherPortal.GetComponent<Portal>().Teleport() + Vector3.down;
        other.transform.Rotate(new Vector3(0, otherPortal.transform.rotation.y + 180, 0));
        
    }
}
