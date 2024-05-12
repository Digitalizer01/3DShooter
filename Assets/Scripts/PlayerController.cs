using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float Life = 100;
    public float Shield = 100;
    public int Ammo;
    public float ShieldAbsorption = 0.5f; // Porcentaje de absorción del escudo
    public float MaxLife = 100;
    public float MaxShield = 100;
    public int MaxAmmo = 30;
    public string gameOverSceneName;
    public string victorySceneName;
    public TMP_Text LifeText;
    public TMP_Text ShieldText;
    public TMP_Text AmmoText;
    public TMP_Text KeyText;
    public AudioClip shieldHitSound;
    public AudioClip playerHitSound;
    public AudioClip pickupItemSound;
    private AudioSource audioSource;
    [HideInInspector] public bool _hasKey;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Añadir componente AudioSource
        _hasKey = false;
    }

    public void Update()
    {
        RefreshUI();
    }

    public void Hit(float damage)
    {
        // Si hay escudo disponible
        if (Shield > 0)
        {
            // Reducir el daño del escudo
            Shield -= damage;
            // Reproducir el sonido de daño al escudo
            if (shieldHitSound != null)
            {
                audioSource.PlayOneShot(shieldHitSound);
            }
            // Reducir el daño del jugador
            Life -= damage / 3;
            // Si el escudo queda en negativo, establecerlo a 0
            if (Shield < 0)
            {
                Shield = 0;
            }
            Debug.Log("Shield hit: " + Shield);
        }
        else
        {
            // Si no hay escudo, el daño afecta directamente a la vida
            Life -= damage;
            // Reproducir el sonido de daño al jugador
            if (playerHitSound != null)
            {
                audioSource.PlayOneShot(playerHitSound);
            }
            Debug.Log("Player hit: " + Life);
        }
        if(Life <= 0)
        {
            // Ir a la siguiente escena
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    // Método para recuperar el escudo
    public void RecoverShield(float amount)
    {
        Shield += amount;
        // No permitir que el escudo supere el máximo
        if (Shield > MaxShield)
        {
            Shield = MaxShield;
        }
        Debug.Log("Shield recovered: " + Shield);
    }

    // Método para recargar munición
    public void ReloadAmmo(int ammoAmount)
    {
        Ammo += ammoAmount;
        // No permitir que la munición supere el máximo
        if (Ammo > MaxAmmo)
        {
            Ammo = MaxAmmo;
        }
        Debug.Log("Ammo reloaded: " + ammoAmount);
    }

    // Método para recuperar la salud
    public void RecoverHealth(float amount)
    {
        Life += amount;
        // No permitir que la salud supere el máximo
        if (Life > 100)
        {
            Life = 100;
        }
        Debug.Log("Health recovered: " + Life);
    }

    private void RefreshUI()
    {
        LifeText.text = "Life: " + Life;
        ShieldText.text = "Shield: " + Shield;
        AmmoText.text = "Ammo: " + Ammo + " / " + MaxAmmo;
        if(_hasKey)
        {
            KeyText.text = "Has key!";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AmmoBox"))
        {
            if(Ammo < MaxAmmo)
            {
                ReloadAmmo(10);
                audioSource.PlayOneShot(pickupItemSound);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("ShieldBox"))
        {
            if(Shield < MaxShield)
            {
                RecoverShield(40);
                audioSource.PlayOneShot(pickupItemSound);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("HealthBox"))
        {
            if(Life < MaxLife)
            {
                RecoverHealth(40);
                audioSource.PlayOneShot(pickupItemSound);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("KeyItem"))
        {
            _hasKey = true;
            audioSource.PlayOneShot(pickupItemSound);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
        else if (other.gameObject.CompareTag("VictoryCollider"))
        {
            SceneManager.LoadScene(victorySceneName);
        }
    }
}
