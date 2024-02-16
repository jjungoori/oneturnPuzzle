using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 필요

public class GridVisualizer : MonoBehaviour
{
    public GameObject cellPrefab; // Editor에서 할당할 prefab
    
    public void GenerateGrid(int[,] grid, float spacing)
    {
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // cellPrefab의 인스턴스를 생성하고, 부모를 현재 GameObject로 설정
                GameObject cell = Instantiate(cellPrefab, new Vector3(j * spacing, i * spacing, 0), Quaternion.identity);
                cell.transform.SetParent(this.transform, false);

                BoardGrid cellComponent = cell.GetComponent<BoardGrid>();
                cellComponent.SetInfo(i ,j);
            }
        }
    }
}