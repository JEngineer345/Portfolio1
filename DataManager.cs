using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    [SerializeField] TextAsset csvFile; // 인스펙터에서 CSV 파일을 할당합니다.

    void Start()
    {
        //string[,] grid = LoadCSV(); // CSV 파일을 로드합니다.
        // CSV 파일에서 데이터를 사용합니다.

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
            string[] rows = csvFile.text.Split('\n'); // 행을 분리합니다.(가로)
            int numRows = rows.Length; // 6

            string[] headers = rows[0].Split(','); // 헤더를 분리합니다.(열/세로)
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
                        grid[row, col] = ""; // 적절한 기본값 사용
                    }
                }
            }
        }

        return grid;
    }
}
