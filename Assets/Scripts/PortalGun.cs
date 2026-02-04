using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] GameObject blueProjectilePrefab, blueProjectileInstance, orangeProjectilePrefab, orangeProjectileInstance, barrelEnd;

    Rigidbody blueRbody, orangeRbody;

    float fireRate, timeStamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireRate = 1;
        timeStamp = -1;

        blueProjectileInstance = Instantiate(blueProjectilePrefab);
        blueRbody = blueProjectileInstance.GetComponent<Rigidbody>();
        blueProjectileInstance.SetActive(false);

        orangeProjectileInstance = Instantiate(orangeProjectilePrefab);
        orangeRbody = orangeProjectileInstance.GetComponent<Rigidbody>();
        orangeProjectileInstance.SetActive(false);

        Physics.IgnoreCollision(gameObject.GetComponentInChildren<Collider>(), blueProjectileInstance.GetComponentInChildren<Collider>());
        Physics.IgnoreCollision(gameObject.GetComponentInChildren<Collider>(), orangeProjectileInstance.GetComponentInChildren<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrimaryFire()
    {
        if (Time.time > timeStamp + fireRate)
        {
            timeStamp = Time.time;

            blueProjectileInstance.transform.position = barrelEnd.transform.position;
            blueProjectileInstance.transform.rotation = barrelEnd.transform.rotation;
            blueProjectileInstance.SetActive(true);
            
            if (blueRbody != null )
            {
                blueRbody.linearVelocity = blueProjectileInstance.transform.forward * 20;
            }
        }
    }

    void SecondaryFire()
    {
        if (Time.time > timeStamp + fireRate)
        {
            timeStamp = Time.time;

            orangeProjectileInstance.transform.position = barrelEnd.transform.position;
            orangeProjectileInstance.transform.rotation = barrelEnd.transform.rotation;
            orangeProjectileInstance.SetActive(true);

            if (orangeRbody != null)
            {
                orangeRbody.linearVelocity = orangeProjectileInstance.transform.forward * 20;
            }
        }
    }
}
