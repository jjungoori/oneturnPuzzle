using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers: MonoBehaviour
{
    public static Managers s_instance = null;
    public static Managers Instance { get { return s_instance; } }

    static GameManager gameManager = new GameManager();
    static SoundManager soundManager = new SoundManager();
    
    public static GameManager Game { get { Init(); return gameManager; } }
    public static SoundManager Sound { get { Init(); return soundManager; } }

    static bool _init = false;
    
    static void Init()
    {
        if (_init)
            return;
        _init = true;
        
        gameManager.Init();
        // soundManager.Init();
    }
}
