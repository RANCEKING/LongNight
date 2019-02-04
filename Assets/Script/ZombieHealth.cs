using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour {
    [Header("Health Properties")]
    [SerializeField]
    int maxHealth = 100;
    [SerializeField] AudioClip deathClip = null;
    [SerializeField] AudioClip damageClip = null;
    [SerializeField] AudioClip biteClip = null;
    [SerializeField] AudioClip normalClip = null;
    [SerializeField] bool isInvulnerable = false;
    [SerializeField] int damageHealth;

    [Header("Components")]
    [SerializeField]
    AudioSource audioSource;

    int currentHealth;

    void Reset()
    {
        audioSource = GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Awake()
    {
        // Debug.Log("touch0");
        currentHealth = maxHealth;
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("touchonoEnter");
        if (other.gameObject.tag == "Weapon")
        {
            //Debug.Log("touch2");
            //isHurt = true;
            TakeDamageZ(damageHealth);
        }
        else if (other.gameObject.tag == "Player")
        {
            if (audioSource != null)
            {
                audioSource.clip = biteClip;
                audioSource.Play();
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("touchonoexit");
        if (other.gameObject.tag == "Weapon")
        {
            if (audioSource != null)
            {
                //audioSource.clip = biteClip;
                audioSource.Play();
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (audioSource != null)
            {
                audioSource.clip = normalClip;
                audioSource.Play();
            }
        }

    }

    public void TakeDamageZ(int amount)
    {

        if (!isInvulnerable)
        {
            //Debug.Log(currentHealth);
            currentHealth -= amount;
        }

        if (!IsAlive())
        {
            Debug.Log("deadin");

            GetComponentInParent<MoveZombie>().SetState("dead");
            if (audioSource != null)
            {
                //Debug.Log("deadvoiceadd");
                audioSource.clip = deathClip;
                audioSource.loop = false;
                audioSource.Play();
            }

        }

        if (audioSource != null)
        {
            //Debug.Log("touch7");
            audioSource.Play();
        }

    }
    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
