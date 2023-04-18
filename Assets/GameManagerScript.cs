using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //配列の宣言    
    int[] map;
    //メソッド
    void PritArray()
    {
        string debugText = "";
        for(int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
            
        }
            Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for(int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        //移動先が範囲外なら移動不可
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }
        //箱がいたら
        if (map[moveTo] == 2)
        {
            //動く方向
            int velocity = moveTo - moveFrom;
            //再起関数
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            if (!success)
            {
                return false;
            }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //配列の初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PritArray();
    }

    // Update is called once per frame
    void Update()
    {
        //右押すと右に動く
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);
            PritArray();
           
        }
        //左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            PritArray();
        }

    }
}
