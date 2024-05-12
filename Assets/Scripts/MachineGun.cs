using UnityEngine;

public class MachineGun : Gun
{
    /*private float fireRate = 0.1f; // Cadencia de fuego en segundos

    public override void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            // Implementa la lógica de disparo de la ametralladora
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                GameObject decal = Instantiate(DecalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));
                m_Decals.Add(decal);

                // Reproducir sonido de disparo
                m_AudioSource.clip = FireSound;
                m_AudioSource.Play();

                Hit(5); // Supongamos que la ametralladora causa 5 de daño
            }

            nextFireTime = Time.time + fireRate;
        }
    }*/
}
