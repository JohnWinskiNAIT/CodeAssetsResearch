using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] GameObject blueProjectile, blueProjectileInstance, orangeProjectile, orangeProjectileInstance, barrelEnd;

    Rigidbody blueRbody, orangeRbody;

    float fireRate, timeStamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireRate = 1;
        timeStamp = -1;

        blueProjectileInstance = Instantiate(blueProjectile);
        blueRbody = blueProjectileInstance.GetComponent<Rigidbody>();
        blueProjectileInstance.SetActive(false);

        orangeProjectileInstance = Instantiate(orangeProjectile);
        orangeRbody = orangeProjectileInstance.GetComponent<Rigidbody>();
        orangeProjectileInstance.SetActive(false);
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
