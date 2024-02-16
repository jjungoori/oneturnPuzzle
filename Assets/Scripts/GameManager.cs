using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager
{
    public int[,] grid;
    public int remainingBlockCount;
    
    public void Init()
    {
        
    }

    public void GridActivated()
    {
        remainingBlockCount -= 1;
        if (remainingBlockCount == 0)
        {
            Debug.LogWarning("Game Complete");
        }
    }
}
