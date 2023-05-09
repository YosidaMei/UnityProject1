using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject clearText;
    //レベルデザイン用の配列の宣言    
    int[,] map;
    //ゲーム管理用の配列
    GameObject[,] field;
    
    //メソッド
    Vector2Int GetPlayerIndex()
    {
        for(int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {

                if (field[y, x] == null)
                {
                    continue;
                }
                else if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }

            }
        }
        return new Vector2Int(-1,-1);
    }

    bool MoveNumber(string tag,Vector2Int moveFrom,Vector2Int moveTo)
    {
        //移動先が範囲外なら移動不可
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0) ||
            moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }
        //箱がいたら
        //nullチェック
        if (field[moveTo.y,moveTo.x]!=null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            if(!success) { return false; }
        }
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);

        field[moveTo.y,moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;

        return true;

    }

    bool IsCleard()
    {
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                  //格納場所のインデックスを控えておく
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for(int i=0; i<goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
               
                //一つでも箱がなかったら条件未達成
                return false;
            }
        }
        return true;
    }
    

    // Start is called before the first frame update
    void Start()
    {

        
        //配列の初期化
        map = new int[,] {
            { 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 2, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 2, 2, 3 },
            { 0, 2, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 3, 0, 0 },
        };
        field = new GameObject[
            map.GetLength(0),
            map.GetLength(1)
            ];

        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                //debugText += map[y, x].ToString() + ",";
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
            }
        }

       
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //debugText += map[y, x].ToString() + ",";
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //右押すと右に動く
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex_m = GetPlayerIndex();
            playerIndex_m.x +=  1;
            MoveNumber("Player", playerIndex, playerIndex_m);
        }
        //左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex_m = GetPlayerIndex();
            playerIndex_m.x -= 1;
            MoveNumber("Player", playerIndex, playerIndex_m);
        }
        //下
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex_m = GetPlayerIndex();
            playerIndex_m.y += 1;
            MoveNumber("Player", playerIndex, playerIndex_m);
        }
        //上
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex_m = GetPlayerIndex();
            playerIndex_m.y-= 1;
            MoveNumber("Player", playerIndex, playerIndex_m);
        }

        if (IsCleard() == true)
        {
            clearText.SetActive(true);
            //デバッグ
            Debug.Log("Clear!");

        }

    }
}
