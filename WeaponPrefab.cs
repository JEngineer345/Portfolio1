using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPrefab : MonoBehaviour
{
    public GameObject EnemyObjectPrefab;

    public static List<GameObject> EnemyList;
    public static List<GameObject> BossEnemyList;

    public static int MaxEnemyCount = 5;
    public static int CurrentEnemyCount = 0;

    float EnemyDeltaTime = 0;
    float EnemySpanTime = 1.0f;

    float EndToStartDeltaTime = 0;
    float EndToStartSpanTime = 3.0f;

    float StageStartDeltaTime = 0;
    float StageStartSpanTime = 3.0f;

    float BossStartDeltaTime = 0;
    float BossStartSpanTime = 2.0f;

    private bool BossStateSpawned = false;

    int row = 1;

    [SerializeField] GameObject gameObjectData;
    [SerializeField] GameObject StageClearText;
    [SerializeField] GameObject StageShow;
    [SerializeField] GameObject BossStageText;

    [SerializeField] AudioSource audioSource;

    public GameObject BossEnemy;

    DataManager dataManager;

    void Start()
    {
        EnemyList = new List<GameObject>();
        BossEnemyList = new List<GameObject>();
        dataManager = gameObjectData.GetComponent<DataManager>();

        StageClearText.SetActive(false);
        StageShow.SetActive(false);
        BossStageText.SetActive(false);

        //audioSource.mute = true;
    }

    void Update()
    {
        string[,] data = dataManager.LoadCSV();

        this.EnemyDeltaTime += Time.deltaTime;
        if (this.EnemyDeltaTime > this.EnemySpanTime)
        {
            int data_parse = int.Parse(data[row, 0]);

            if (data_parse == row)
            {
                int data_parse_EnemyN = int.Parse(data[row, 1]);
                if (data_parse_EnemyN > CurrentEnemyCount)
                {
                    CurrentEnemyCount++;
                    this.EnemyDeltaTime = 0;
                    GameObject ego = Instantiate(EnemyObjectPrefab);
                    EnemyList.Add(ego);
                    int py = Random.Range(-2, 4);
                    ego.transform.position = new Vector3(11f, py, 0);
                }
            }

            if (EnemyList.Count == 0)
            {
                this.EndToStartDeltaTime += Time.deltaTime;
                if (this.EndToStartDeltaTime < this.EndToStartSpanTime)
                {
                    audioSource.Play();
                    StageClearText.SetActive(true);
                    StageClearText.GetComponent<Text>().text = "Stage" + row + " Clear!";
                }

                else
                {
                    StageClearText.SetActive(false);
                    this.StageStartDeltaTime += Time.deltaTime;
                    if (this.StageStartDeltaTime < this.StageStartSpanTime)
                    {
                        StageShow.SetActive(true);
                        if (row < 5)
                        {
                            StageShow.GetComponent<Text>().text = "Stage" + (row + 1) + " Start!";
                        }

                        else
                        {
                            StageShow.SetActive(false);
                        }
                    }

                    else
                    {
                        if (row < 5)
                        {
                            row++;
                            CurrentEnemyCount = 0;
                            this.EnemyDeltaTime = 0;
                            this.EndToStartDeltaTime = 0;
                            this.StageStartDeltaTime = 0;
                            EnemyList.Clear();
                            StageShow.SetActive(false);
                        }

                        else
                        {
                            BossStageText.SetActive(true);
                            BossStageText.GetComponent<Text>().text = "Boss Stage!";
                            this.BossStartDeltaTime += Time.deltaTime;
                            if (this.BossStartDeltaTime > this.BossStartSpanTime)
                            {
                                BossStageText.SetActive(false);
                            }

                            if (!BossStateSpawned)
                            {
                                GameObject bsgo = Instantiate(BossEnemy, new Vector3(5, 0, 0), Quaternion.identity);
                                if (BossEnemyList.Count < 2)
                                {
                                    BossEnemyList.Add(bsgo);
                                }
                                
                                BossStateSpawned = true;
                            }
                        }
                    }
                }
            }
        }
    }
}