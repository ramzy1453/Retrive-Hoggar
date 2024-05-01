using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTip;             
    public GameObject bulletPrefab;      
    public float bulletSpeed = 30f;      
    public float recoilForce = 200f;     
    public float maxDistance = 100f;     

    private AudioSource gunAudioSource;
    public AudioClip gunClip;

    void Start()
    {
        gunAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))   
        {
            Shoot();  
        }
    }

    void Shoot()
    {
        // Perform raycast from gunTip to detect hits
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, maxDistance))
        {
            // Instantiate a new bullet at the hit point and rotation
            GameObject bullet = Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            // Get the rigidbody component of the bullet
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // Calculate bullet direction based on hit normal (surface normal of the hit point)
                Vector3 bulletDirection = Vector3.Reflect(gunTip.forward, hit.normal).normalized;
                bulletRigidbody.velocity = bulletDirection * bulletSpeed;
            }

            // Play gun shoot sound if audio source is assigned
            if (gunAudioSource != null)
            {
                gunAudioSource.Play();
            }

            // Apply recoil force to the gun at the gunTip position in the opposite direction
            Rigidbody gunRigidbody = GetComponent<Rigidbody>();
            if (gunRigidbody != null)
            {
                // Calculate the recoil direction from gunTip to gun position
                Vector3 recoilDirection = (transform.position - gunTip.position).normalized;
                gunRigidbody.AddForce(recoilDirection * recoilForce);
            }

            // Example: Apply damage to the hit object if it has a Health script
            Health health = hit.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);  // Adjust damage amount as needed
            }
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, gunTip.forward*maxDistance, gunTip.rotation);

            // Get the rigidbody component of the bullet
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // Apply forward force to the bullet
                bulletRigidbody.velocity = gunTip.forward * bulletSpeed;
            }

            // Play gun shoot sound if audio source is assigned
            if (gunAudioSource != null)
            {
                gunAudioSource.PlayOneShot(gunClip, 1f);
            }

            // Apply recoil force to the gun at the gunTip position in the opposite direction
            Rigidbody gunRigidbody = GetComponent<Rigidbody>();
            if (gunRigidbody != null)
            {
                // Calculate the recoil direction from gunTip to gun position
                Vector3 recoilDirection = (transform.position - gunTip.position).normalized;
                gunRigidbody.AddForce(recoilDirection * recoilForce);
            }
        }
    }


    private void OnDrawGizmos()
    {
        // Draw a red line in the Scene view to represent the raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gunTip.position, gunTip.position + gunTip.forward * maxDistance);   
    }
}
