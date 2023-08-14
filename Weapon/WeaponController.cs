using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] float WeaponSpeed = 12.0f;
    [SerializeField] GameObject EnemyObject;
    [SerializeField] GameObject BossEnemy;

    // [SerializeField] AudioClip HitSound;
    AudioSource HitSource;

    int EnemyIndex = 0;
    int WeaponIndex = 0;

    void Start()
    {
        HitSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // transform.Translate(new Vector3(0.007f, 0, 0));

        if (transform.position.x > 11.5f)
        {
            Destroy(gameObject);
        }

        // 일반 Enemy 실행코드
        if (EnemyIndex < WeaponPrefab.EnemyList.Count && WeaponIndex < Bullet.BulletList.Count)
        {
            Vector3 EnemyPos = WeaponPrefab.EnemyList[EnemyIndex].transform.position;
            Vector3 WeaponPos = transform.position;
            Vector3 Distance = EnemyPos - WeaponPos;
            Vector3 Direction = Distance.normalized;

            transform.Translate(Direction * WeaponSpeed * Time.deltaTime);

            //EnemyIndex++;
            //WeaponIndex++;
        }

        if (WeaponPrefab.EnemyList.Count == 0 && WeaponPrefab.BossEnemyList.Count == 0)
        {
            Destroy(gameObject);
        }

        // 보스 Enemy 실행코드
        if (WeaponPrefab.BossEnemyList.Count > 0)
        {
            Vector3 BossEnemyPos = WeaponPrefab.BossEnemyList[0].transform.position;
            Vector3 WeaponPos = transform.position;
            Vector3 Distance = BossEnemyPos - WeaponPos;
            Vector3 Direction = Distance.normalized;

            transform.Translate(Direction * WeaponSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            WeaponPrefab.EnemyList.RemoveAt(EnemyIndex);
            Bullet.BulletList.RemoveAt(WeaponIndex);

            if (HitSource != null && HitSource.enabled)
            {
                HitSource.mute = false;
                HitSource.Play();
            }
            //HitSource.clip = HitSound;
            //HitSource.Play();
        }

        if (collision.gameObject.tag == "BossEnemy")
        {
            if (HitSource != null && HitSource.enabled)
            {
                HitSource.mute = false;
                HitSource.Play();
            }
            Destroy(gameObject);
            Bullet.BulletList.RemoveAt(WeaponIndex);
        }
    }
}
