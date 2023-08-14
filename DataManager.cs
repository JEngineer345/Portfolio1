using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    [SerializeField] TextAsset csvFile; // �ν����Ϳ��� CSV ������ �Ҵ��մϴ�.

    void Start()
    {
        //string[,] grid = LoadCSV(); // CSV ������ �ε��մϴ�.
        // CSV ���Ͽ��� �����͸� ����մϴ�.

        // stage 1 - 5
        // stage 2 - 6
        //for (int row = 1; row < grid.GetLength(0); row++)
        //{
        //    for (int col = 1; col < grid.GetLength(1); col++)
        //    {
        //        Debug.Log(grid[row, col]);
        //    }
        //}
    }

    void Update()
    {
    }

    public string[,] LoadCSV()
    {
        string[,] grid = null;

        if (csvFile != null)
        {
            string[] rows = csvFile.text.Split('\n'); // ���� �и��մϴ�.(����)
            int numRows = rows.Length; // 6

            string[] headers = rows[0].Split(','); // ����� �и��մϴ�.(��/����)
            int numCols = headers.Length; // 0, 1

            grid = new string[numRows, numCols]; // new string[6, 2];

            for (int row = 0; row < numRows; row++)
            {
                string[] cols = rows[row].Split(',');

                for (int col = 0; col < numCols; col++)
                {
                    if (col < cols.Length)
                    {
                        grid[row, col] = cols[col];
                    }
                    else
                    {
                        grid[row, col] = ""; // ������ �⺻�� ���
                    }
                }
            }
        }

        return grid;
    }
}
