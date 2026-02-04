using UnityEngine;

public class PortalProjectile : MonoBehaviour
{
    [SerializeField] GameObject portalPrefab;

    GameObject portalInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portalInstance = Instantiate(portalPrefab);
        portalInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        portalInstance.transform.position = collision.GetContact(0).point;
        portalInstance.transform.LookAt(collision.GetContact(0).point + collision.GetContact(0).normal);
        portalInstance.transform.position = portalInstance.transform.position + portalInstance.transform.forward * 0.05f;
        portalInstance.SetActive(true);

        gameObject.SetActive(false);
    }
}
