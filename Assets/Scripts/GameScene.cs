using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class GameScene : MonoBehaviour
{
    private float spacing = 1;
    private GameObject cellPrefab;

    private GameObject previousActivedObject;
    
    private MapGenerator _mapGenerator;
    private GridVisualizer _gridVisualizer;
    private GameObject camera;

    public void Start()
    {
        Debug.Log("gamemanager init start");
        _mapGenerator = new MapGenerator();
        _gridVisualizer = GameObject.Find("@GridVisualizer").GetOrAddComponent<GridVisualizer>();
        Debug.Log("gamemanager init done");

        camera = GameObject.Find("Main Camera");
        
        GameStart();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // 마우스 클릭 위치를 월드 공간 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 레이를 쏘아냅니다.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, new UnityEngine.Vector2(0,0));

            // 레이가 콜라이더와 충돌했는지 확인합니다.
            if (hit.collider != null)
            {
                // 충돌한 게임 오브젝트 처리
                if (hit.collider.gameObject != previousActivedObject && IsAbleToMove(hit.collider.gameObject))
                {
                    Debug.Log("yay");

                    Animator targetAnimator = hit.collider.GetComponent<Animator>();
                    targetAnimator.Play("active", -1, 0f);
                    hit.collider.GetComponent<BoardGrid>().Activated();
                    
                    previousActivedObject = hit.collider.gameObject;

                }
            }
        }
    }

    public void GameStart()
    {
        int size = 4;

        Managers.Game.remainingBlockCount = 10;
        Managers.Game.grid = _mapGenerator.GenerateMap(size, size, Managers.Game.remainingBlockCount);
        _gridVisualizer.GenerateGrid(Managers.Game.grid, spacing);

        Vector3 visualizerPos = _gridVisualizer.transform.position;
        camera.transform.position = new Vector3(visualizerPos.x + spacing * (size-1)/2, visualizerPos.y + spacing * (size-1)/2, -10);
    }

    private bool IsAbleToMove(GameObject grid)
    {
        if (previousActivedObject == null)
            return true;
        
        int targetX = grid.GetComponent<BoardGrid>().x;
        int targetY = grid.GetComponent<BoardGrid>().y;
        
        int prvX = previousActivedObject.GetComponent<BoardGrid>().x;
        int prvY = previousActivedObject.GetComponent<BoardGrid>().y;

        //prv - target 은 x, y 둘 다 0이 될 수 없음 (같은 그리드 터치 감지 안 함)
        if (Mathf.Abs(prvX - targetX) + Mathf.Abs(prvY - targetY) < 2)
            return true;

        return false;
    }
}
