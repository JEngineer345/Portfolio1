using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    CoinManager coinManager;

    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    void Update()
    {
        transform.Translate(new Vector3(-0.002f, 0, 0));

        if (transform.position.x < -11.5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            coinManager.CoinPlus();
        }
    }
}
