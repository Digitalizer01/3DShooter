using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /*public GameObject DecalPrefab;
    public AudioClip FireSound;
    protected AudioSource m_AudioSource;
    protected List<GameObject> m_Decals = new List<GameObject>();
    protected int m_DecalLimit = 10;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<PlayerController>().Ammo > 0) // Comprobar si hay munición disponible
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                GameObject decal = Instantiate(DecalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));
                m_Decals.Add(decal);

                // Play fire sound
                m_AudioSource.clip = FireSound;
                m_AudioSource.Play();

                GetComponent<PlayerController>().Ammo--; // Reducir la munición actual

                // Check if decal count exceeds limit
                if (m_Decals.Count > m_DecalLimit)
                {
                    // Destroy the oldest decal
                    Destroy(m_Decals[0]);
                    m_Decals.RemoveAt(0);
                }
            }
        }
    }

    public virtual void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
        {
            // Instanciar un decal en el punto de impacto
            GameObject decal = Instantiate(DecalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));
            m_Decals.Add(decal);

            // Reproducir sonido de disparo
            m_AudioSource.clip = FireSound;
            m_AudioSource.Play();

            // Llamar al método Hit del objeto impactado si tiene un componente que lo implementa
            var hitComponent = hit.collider.GetComponent<HitTarget>();
            if (hitComponent != null)
            {
                hitComponent.Hit(10); // Supongamos que el daño es 10
            }
        }
    }*/
}
