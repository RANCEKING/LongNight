using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour {


    void OnTriggerStay(Collider col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //　敵キャラクターの状態を取得
            MoveZombie.EnemyState state = GetComponentInParent<MoveZombie>().GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state == MoveZombie.EnemyState.Walk || state == MoveZombie.EnemyState.Wait)
            {
                Debug.Log("プレイヤー発見");
                GetComponentInParent<MoveZombie>().SetState("chase", col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("見失う");
            GetComponentInParent<MoveZombie>().SetState("wait");
        }
    }
}
