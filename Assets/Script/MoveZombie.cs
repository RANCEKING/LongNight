using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZombie : MonoBehaviour {

    private CharacterController enemyController;
    private Animator animator;
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed=1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;
    //  目的地フラグ
    private int goback=1;
    //　敵の状態
    private EnemyState state;
    //[SerializeField]
    //private string ZombieName;

    //private GameObject Zombie;
    //[SerializeField]
    //AudioSource audioSource;
    //[SerializeField] AudioClip deathClip = null;
    //[SerializeField] AudioClip chaseClip = null;

    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Bite,
        Dead
    };
    //　追いかけるキャラクター
    private Transform playerTransform;


    // Use this for initialization
    void Start () {
        //Zombie = GameObject.Find(ZombieName);
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition(goback);
        destination = setPosition.GetDestination();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState("walk");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == EnemyState.Chase)
            {
                setPosition.SetDestination(playerTransform.position);
                destination = setPosition.GetDestination();
                //if (audioSource != null)
                //{
                 //   audioSource.clip = chaseClip;
                 //   audioSource.Play();
               // }
                if (enemyController.isGrounded || !enemyController.isGrounded)
                {
                    velocity = Vector3.zero;
                    animator.SetFloat("Speed", 3.0f);
                    direction = (destination - transform.position).normalized;
                    transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                    velocity = direction * walkSpeed * 2f;
                    //Debug.Log(destination);
                    //Debug.Log(Vector3.Distance(transform.position, destination));
                }
                velocity.y += Physics.gravity.y * Time.deltaTime;
                enemyController.Move(velocity * Time.deltaTime);

                //　目的地に到着したかどうかの判定
                if (Vector3.Distance(transform.position, destination) < 2f)
                {
                    //Debug.Log("bite2");
                    SetState("bite");
                    animator.SetFloat("Speed", 0.4f);
                }
            }

            if (state == EnemyState.Walk)
            {
                if (enemyController.isGrounded)
                {
                    velocity = Vector3.zero;
                    animator.SetFloat("Speed", 2.0f);
                    direction = (destination - transform.position).normalized;
                    transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                    velocity = direction * walkSpeed;
                    //Debug.Log(destination);
                }
                velocity.y += Physics.gravity.y * Time.deltaTime;
                enemyController.Move(velocity * Time.deltaTime);

                //　目的地に到着したかどうかの判定
                if (Vector3.Distance(transform.position, destination) < 0.5f)
                {
                    //Debug.Log("wait");
                    SetState("wait");
                    animator.SetFloat("Speed", 0.0f);

                }
            }
        }
        //　到着していたら一定時間待つ    
        else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;

            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                goback = -1 * goback;
                //Debug.Log(goback);
                SetState("walk");
                arrived = false;
                elapsedTime = 0f;
            }
            //Debug.Log(elapsedTime);
        }
        //
        else if (state == EnemyState.Bite)
        {
            //Debug.Log("bite");
            setPosition.SetDestination(playerTransform.position);
            destination = setPosition.GetDestination();
            //Debug.Log(destination);
            //Debug.Log(Vector3.Distance(transform.position, destination));
            if (Vector3.Distance(transform.position, destination) > 2f)
            {
                //Debug.Log("bite to chase");
                animator.SetBool("IsBite", false);
                animator.SetFloat("Speed", 3.0f);
                SetState("chase", playerTransform);
                
            }
        }
        else if (state == EnemyState.Dead)
        {
            //Debug.Log("dead");
            animator.SetFloat("Speed", 0.0f);
            animator.SetBool("IsDead", true);
            //if (audioSource != null)
            // {
            //   audioSource.clip = deathClip;
            //  audioSource.Play();
            // }
           
            this.GetComponent<MeshCollider>().enabled=false;
            this.GetComponent<CharacterController>().enabled = false;
            Debug.Log(this.GetComponent<MeshCollider>().enabled);

        }
    }

    //　敵キャラクターの状態変更メソッド
    public void SetState(string mode, Transform obj = null)
    {
        if (mode == "walk")
        {
            arrived = false;
            elapsedTime = 0f;
            state = EnemyState.Walk;
            setPosition.CreateRandomPosition(goback);
            destination = setPosition.GetDestination();
        }
        else if (mode == "chase")
        {
            state = EnemyState.Chase;
            //　待機状態から追いかける場合もあるのでOff
            arrived = false;
            //　追いかける対象をセット
            playerTransform = obj;
        }
        else if (mode == "wait")
        {
            elapsedTime = 0f;
            state = EnemyState.Wait;
            arrived = true;
            animator.SetFloat("Speed", 0f);
        }
        else if (mode == "bite")
        {
            elapsedTime = 0f;
            state = EnemyState.Bite;
            arrived = true;
            animator.SetFloat("Speed", 0.4f);
            animator.SetBool("IsBite", true);
        }
        else if (mode == "dead")
        {
            state = EnemyState.Dead;
            animator.SetBool("IsDead", true);
        }
    }

    public EnemyState GetState()
    {
        return state;
    }
}
