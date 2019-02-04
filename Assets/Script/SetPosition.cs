using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour {

    //初期位置
    private Vector3 startPosition;
    //目的地
    private Vector3 destination;
    [SerializeField]
    private float front_go;
    [SerializeField]
    private float updown_go;
    [SerializeField]
    private float yoko_go;
    [SerializeField]
    private float front_back;
    [SerializeField]
    private float updown_back;
    [SerializeField]
    private float yoko_back;

    void Start()
    {
        //　初期位置を設定
        startPosition = transform.position;
        SetDestination(transform.position);
    }

    //　ランダムな位置の作成
    public void CreateRandomPosition(int goback)
    {
        //　ランダムなVector2の値を得る
        //var randDestination = Random.insideUnitCircle * 4;
        //　現在地にランダムな位置を足して目的地とする
        //SetDestination(startPosition + new Vector3(randDestination.x, 0, randDestination.y));
        if (goback == 1)
        {
            SetDestination(new Vector3(front_go, updown_go, yoko_go));
        }
        else
        {
            SetDestination(new Vector3(front_back, updown_back, yoko_back));
        }
    }


    //　目的地を設定する
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //　目的地を取得する
    public Vector3 GetDestination()
    {
        return destination;
    }
}
