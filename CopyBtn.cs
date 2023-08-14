using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyBtn : MonoBehaviour
{
    [SerializeField] GameObject PlayerObjectPrefab; // ������ ���� ����

    public static GameObject pop;
    private GameObject[] go; // go �迭 ����

    int MaxObject = 5; // �ִ� �÷��̾� ���� ��
    int CurrentObject = 0; // ���� �÷��̾� ���� ��

    bool IsCreationObject; // ���� ����

    Vector2 v2;

    void Start()
    {
        go = new GameObject[MaxObject]; // go �迭 ����
        IsCreationObject = false;
    }

    void Update()
    {
        // ���콺 Ŭ�� �� ��ġ�� ���� �÷��̾� ��ü ��ġ
        if (IsCreationObject && Input.GetMouseButton(0))
        {
            v2 = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            pop.transform.position = new Vector2(v2.x, v2.y);
            IsCreationObject = false; // �������� ����
            Debug.Log("��ġ�Ϸ�");
        }
    }

    // ��ư�� ���� ��ũ�ϴ� �޼ҵ�
    // �÷��̾ ��ư�� Ŭ���ؼ� ����
    public void CopyObject()
    {
        if (MaxObject > CurrentObject)
        {
            pop = Instantiate(PlayerObjectPrefab) as GameObject;
            go[CurrentObject] = pop;
            CurrentObject++;
            IsCreationObject = true;
            Debug.Log("�����Ϸ�");
        }

        else
        {
            Debug.Log("���� ���� Ƚ�� �ʰ�");
        }
    }
}