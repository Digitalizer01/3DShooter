using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    public GameObject DecalPrefab;
    public AudioClip FireSound;
    private AudioSource m_AudioSource;
    private List<GameObject> m_Decals = new List<GameObject>();
    private int m_DecalLimit = 10;
    public int Damage = 10;

    // Referencias a los dos gameobjects hijos
    public GameObject SmallWeapon;
    public GameObject BigWeapon;

    private bool showSmallWeapon = true; // Variable para controlar la visibilidad

    // Parámetros de las armas
    public int SmallWeaponDamage = 10;
    public int SmallWeaponAmmoCapacity = 10;
    public float SmallWeaponFireRate = 1f; // Tiempo entre disparos en segundos

    public int BigWeaponDamage = 5;
    public int BigWeaponAmmoCapacity = 30;
    public float BigWeaponFireRate = 0.1f; // Tiempo entre disparos en segundos
    public float BigWeaponRecoil = 0.1f; // Magnitud del retroceso

    private float smallWeaponNextFireTime = 0f;
    private float bigWeaponNextFireTime = 0f;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        GetComponent<PlayerController>().Ammo = GetComponent<PlayerController>().MaxAmmo; // Inicializar la munición actual al máximo
        ToggleChildVisibility();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            ToggleChildVisibility(); // Alternar la visibilidad al presionar la tecla B
        }

        // Disparar con SmallWeapon
        if (Input.GetMouseButtonDown(0) && GetComponent<PlayerController>().Ammo > 0 && showSmallWeapon && Time.time >= smallWeaponNextFireTime)
        {
            ShootSmallWeapon();
        }

        // Disparar con BigWeapon
        if (Input.GetMouseButton(0) && GetComponent<PlayerController>().Ammo > 0 && !showSmallWeapon && Time.time >= bigWeaponNextFireTime)
        {
            ShootBigWeapon();
        }
    }

    void ShootSmallWeapon()
    {
        // Actualizar el tiempo de próximo disparo
        smallWeaponNextFireTime = Time.time + SmallWeaponFireRate;

        // Realizar el disparo
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
        {
            if (hit.collider.CompareTag("Enemy") && hit.collider.gameObject.GetComponent<enemyAI>().smallCollider == hit.collider)
            {
                hit.collider.gameObject.GetComponent<enemyAI>().Hit(SmallWeaponDamage);
            }
            else
            {
                GameObject decal = Instantiate(DecalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));
                m_Decals.Add(decal);
            }

            // Play fire sound
            m_AudioSource.clip = FireSound;
            m_AudioSource.Play();

            // Reducir la munición
            GetComponent<PlayerController>().Ammo--;

            // Check if decal count exceeds limit
            if (m_Decals.Count > m_DecalLimit)
            {
                // Destroy the oldest decal
                Destroy(m_Decals[0]);
                m_Decals.RemoveAt(0);
            }
        }
    }

    void ShootBigWeapon()
    {
        // Actualizar el tiempo de próximo disparo
        bigWeaponNextFireTime = Time.time + BigWeaponFireRate;

        // Realizar el disparo
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
        {
            if (hit.collider.CompareTag("Enemy") && hit.collider.gameObject.GetComponent<enemyAI>().smallCollider == hit.collider)
            {
                hit.collider.gameObject.GetComponent<enemyAI>().Hit(BigWeaponDamage);
            }
            else
            {
                GameObject decal = Instantiate(DecalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));
                m_Decals.Add(decal);
            }

            // Play fire sound
            m_AudioSource.clip = FireSound;
            m_AudioSource.Play();

            // Reducir la munición
            GetComponent<PlayerController>().Ammo--;

            // Aplicar retroceso
            Vector3 recoil = Random.insideUnitSphere * BigWeaponRecoil;
            recoil.z = 0f; // Solo queremos el retroceso en el eje Z (horizontal)
            transform.localPosition += recoil;

            // Check if decal count exceeds limit
            if (m_Decals.Count > m_DecalLimit)
            {
                // Destroy the oldest decal
                Destroy(m_Decals[0]);
                m_Decals.RemoveAt(0);
            }
        }
    }

    // Método para alternar la visibilidad de los gameobjects hijos
    void ToggleChildVisibility()
    {
        showSmallWeapon = !showSmallWeapon; // Alternar el estado

        // Activar o desactivar los gameobjects según el estado
        SmallWeapon.SetActive(showSmallWeapon);
        BigWeapon.SetActive(!showSmallWeapon);
    }

    public void Hit(float damage)
    {
        Debug.Log("Enemy hit. Damage: " + damage);
    }

}
