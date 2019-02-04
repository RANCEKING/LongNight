using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [Header("Health Properties")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] AudioClip deathClip = null;
    [SerializeField] AudioClip goalClip = null;
    [SerializeField] bool isInvulnerable = false;
    [SerializeField] int damageHealth;

    [Header("Components")]
    [SerializeField] AudioSource audioSource;

    //public static GameManager Instance;



    //bool isHurt = false;

    int currentHealth;
    

    void Reset()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Use this for initialization
    void Awake ()
    {
       // Debug.Log("touch0");
        currentHealth = maxHealth; 
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("touch1");
        if (other.gameObject.tag == "Trap")
        {
            //Debug.Log("touch2");
            //isHurt = true;
            TakeDamage(damageHealth);
        }
        else if(other.gameObject.tag== "Zombie")
        {
            //Debug.Log("touchZ");
            TakeDamage(damageHealth);
        }
        else if(other.gameObject.tag == "Goal")
        {
            Debug.Log("Goal");
            SceneClear();
        }
    }
    /*
        void OnTriggerExit(Collider other)
        {
            Debug.Log("touch3");
            if (other.gameObject.tag == "Trap")
            {
                Debug.Log("touch4");
                //isHurt = false;

            }    
        }

        void Update()
        {
            //StartCoroutine("AttackPlayer");
            if (isHurt)
            {
                Debug.Log("touch5");
                //GameManager.Instance.Player.TakeDamage(damageHealth);
                //TakeDamage(damageHealth);
            }

        }
        */

    /*
       IEnumerator AttackPlayer()
       {

           if (GameManager.Instance = null)
           {
               yield break;
           }

        }
        */
    public void TakeDamage(int amount)
    {
        /*if (!IsAlive())
        {
            Debug.Log("touch6");
            return;
        }
        */
        if (!isInvulnerable)
        {
            //Debug.Log(currentHealth);
            currentHealth -= amount;
        }

        if (!IsAlive())
        {
            if (audioSource != null)
            {
                //Debug.Log("touch6");
                audioSource.clip = deathClip;
            }

            DeathComplete();
        }

        if (audioSource != null)
        {
            //Debug.Log("touch7");
            audioSource.Play();
        }

    }
	
    public void SceneClear()
    {
        if (audioSource != null)
        {
            Debug.Log("SceneCleargoalAudio");
            audioSource.clip = goalClip;
        }

        ClearComplete();

        if (audioSource != null)
        {
           Debug.Log("SceneClearAudio");
            audioSource.Play();
        }

    }

    public bool IsAlive () {
        return currentHealth > 0;
	}

    void DeathComplete()
    {
        if (GameManager.Instance.Player == this)
        {
            GameManager.Instance.PlayerDeathComplete();
        }
    }

    void ClearComplete()
    {
        if (GameManager.Instance.Player == this)
        {
            GameManager.Instance.PlayerClearComplete();
        }
    }

}
