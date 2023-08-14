using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyBtn : MonoBehaviour
{
    [SerializeField] GameObject PlayerObjectPrefab; // 프리팹 변수 선언

    public static GameObject pop;
    private GameObject[] go; // go 배열 선언

    int MaxObject = 5; // 최대 플레이어 생성 수
    int CurrentObject = 0; // 현재 플레이어 생성 수

    bool IsCreationObject; // 생성 상태

    Vector2 v2;

    void Start()
    {
        go = new GameObject[MaxObject]; // go 배열 생성
        IsCreationObject = false;
    }

    void Update()
    {
        // 마우스 클릭 시 위치에 따라 플레이어 객체 배치
        if (IsCreationObject && Input.GetMouseButton(0))
        {
            v2 = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            pop.transform.position = new Vector2(v2.x, v2.y);
            IsCreationObject = false; // 생성상태 해지
            Debug.Log("배치완료");
        }
    }

    // 버튼과 직접 링크하는 메소드
    // 플레이어를 버튼을 클릭해서 생성
    public void CopyObject()
    {
        if (MaxObject > CurrentObject)
        {
            pop = Instantiate(PlayerObjectPrefab) as GameObject;
            go[CurrentObject] = pop;
            CurrentObject++;
            IsCreationObject = true;
            Debug.Log("생성완료");
        }

        else
        {
            Debug.Log("생성 가능 횟수 초과");
        }
    }
}