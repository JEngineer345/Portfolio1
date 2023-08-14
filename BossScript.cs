using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    float PlayerOffensivePower = 30.0f;
    float BossHp = 100f;

    public bool BossState;

    ClearManager clear;

    void Start()
    {
        BossState = true;

        clear = FindObjectOfType<ClearManager>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            BossHp -= PlayerOffensivePower;
            if (BossHp < 0)
            {
                Destroy(gameObject);
                WeaponPrefab.BossEnemyList.RemoveAt(0);
                BossState = false;

                clear.GameClearPanel(BossState);
            }
        }
    }
}
