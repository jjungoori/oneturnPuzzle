using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class BoardGrid : MonoBehaviour
{
    public int x, y;
    private TextMeshPro textBox;
    private SpriteRenderer spr;

    private List<Color> colorForLevel;

    void Init()
    {
        textBox = GetComponentInChildren<TextMeshPro>();
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        colorForLevel = new List<Color>()
        {
            Color.gray,
            Color.white,
            Color.yellow,
            Color.red,
        };
    }
    
    public void SetInfo(int x, int y)
    {
        Init();

        this.x = x;
        this.y = y;
        
        Reload();
    }

    public void Activated()
    {
        if (GetNumber() > 0)
        {
            Managers.Game.GridActivated();
        }
        Managers.Game.grid[y, x] -= 1;
        Reload();
    }

    private void Reload()
    {
        var val = Managers.Game.grid[y, x];
        textBox.text = val.ToString();
        spr.color = GetColor(val);
    }

    private Color GetColor(int index)
    {
        if (index > colorForLevel.Count - 1 || index < 0)
        {
            return Color.black;
        }
        else
        {
            return colorForLevel[index];
        }
    }

    public int GetNumber()
    {
        return Managers.Game.grid[y, x];
    }
}
