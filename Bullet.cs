using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform firePoint;

    public static List<GameObject> BulletList;

    int MaxBulletCount = WeaponPrefab.MaxEnemyCount;
    int CurrentBulletCount = WeaponPrefab.CurrentEnemyCount;

    float BulletDeltaTime = 0;
    float BulletSpanTime = 2.0f;


    void Start()
    {
        BulletList = new List<GameObject>();
    }

    void Update()
    {
        BulletDeltaTime += Time.deltaTime;
        if (BulletDeltaTime > BulletSpanTime)
        {
            BulletDeltaTime = 0;
            GameObject bgo = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
            BulletList.Add(bgo);
        }
    }
}