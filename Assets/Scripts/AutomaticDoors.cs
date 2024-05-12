using UnityEngine;
using System.Collections;

public class AutomaticDoors : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Transform leftClosedLocation;
    public Transform rightClosedLocation;
    public Transform leftOpenLocation;
    public Transform rightOpenLocation;

    public float speed = 1.0f;

    bool isOpening = false;
    bool isClosing = false;
    Vector3 distance;

    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip deniedSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update ()
    {
        if (isOpening)
        {
            distance = leftDoor.localPosition - leftOpenLocation.localPosition;
            if (distance.magnitude < 0.001f)
            {
                isOpening = false;
                leftDoor.localPosition = leftOpenLocation.localPosition;
                rightDoor.localPosition = rightOpenLocation.localPosition;
            }
            else
            {
                leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, 
                                                      leftOpenLocation.localPosition, 
                                                      Time.deltaTime * speed);
                rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, 
                                                       rightOpenLocation.localPosition, 
                                                       Time.deltaTime * speed);
            }
        }
        else if (isClosing)
        {
            distance = leftDoor.localPosition - leftClosedLocation.localPosition;
            if (distance.magnitude < 0.001f)
            {
                isClosing = false;
                leftDoor.localPosition = leftClosedLocation.localPosition;
                rightDoor.localPosition = rightClosedLocation.localPosition;
            }
            else
            {
                leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, 
                                                      leftClosedLocation.localPosition, 
                                                      Time.deltaTime * speed);
                rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, 
                                                       rightClosedLocation.localPosition, 
                                                       Time.deltaTime * speed);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(!col.GetComponent<PlayerController>()._hasKey)
        {
            if (deniedSound != null)
                audioSource.PlayOneShot(deniedSound);
        }
        else{
            isOpening = true;
            isClosing = false;
            // Reproducir sonido de apertura
            if (openSound != null)
                audioSource.PlayOneShot(openSound);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<PlayerController>()._hasKey)
        {
            isOpening = true;
            isClosing = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.GetComponent<PlayerController>()._hasKey)
        {
            isClosing = true;
            isOpening = false;
            // Reproducir sonido de cierre
            if (closeSound != null)
                audioSource.PlayOneShot(closeSound);
        }
    }
}
