using UnityEngine;

public class Pistol : Gun
{
    /*private float fireRate = 0.5f; // Cadencia de fuego en segundos
    private float nextFireTime = 0f;

    public override void Fire()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        // Implementa la lógica de disparo de la pistola
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
        {
            GameObject decal = Instantiate(DecalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));
            m_Decals.Add(decal);

            // Reproducir sonido de disparo
            m_AudioSource.clip = FireSound;
            m_AudioSource.Play();

            Hit(10); // Supongamos que la pistola causa 10 de daño
        }
    }*/
}
