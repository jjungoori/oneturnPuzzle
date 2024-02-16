using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator
{
    public int[,] GenerateMap(int width, int height, int moves)
    {

        int[,] grid = new int[height, width];
        bool[,] visited = new bool[height, width];

        int currentX = Random.Range(0, width);
        int currentY = Random.Range(0, height);

        visited[currentY, currentX] = false;

        for (int i = 0; i < moves; i++)
        {
            grid[currentY, currentX]++;
            
            List<int> possibleDirections = new List<int>();
            if (currentY > 0 && !visited[currentY - 1, currentX]) possibleDirections.Add(0);
            if (currentY < height - 1 && !visited[currentY + 1, currentX]) possibleDirections.Add(1);
            if (currentX > 0 && !visited[currentY, currentX - 1]) possibleDirections.Add(2);
            if (currentX < width - 1 && !visited[currentY, currentX + 1]) possibleDirections.Add(3);

            if (possibleDirections.Count == 0) break;

            int direction = possibleDirections[Random.Range(0, possibleDirections.Count)];

            switch (direction)
            {
                case 0: currentY--; break;
                case 1: currentY++; break;
                case 2: currentX--; break;
                case 3: currentX++; break;
            }

            visited[currentY, currentX] = false;
        }

        PrintGrid(grid);

        return grid;
    }

    void PrintGrid(int[,] grid)
    {
        string gridText = "";
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                gridText += grid[i, j].ToString() + " ";
            }
            gridText += "\n";
        }
        Debug.Log(gridText);
    }
}
