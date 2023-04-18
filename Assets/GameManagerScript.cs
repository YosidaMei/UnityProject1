using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�z��̐錾    
    int[] map;
    //���\�b�h
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
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }
        //����������
        if (map[moveTo] == 2)
        {
            //��������
            int velocity = moveTo - moveFrom;
            //�ċN�֐�
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
        //�z��̏�����
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PritArray();
    }

    // Update is called once per frame
    void Update()
    {
        //�E�����ƉE�ɓ���
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);
            PritArray();
           
        }
        //��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            PritArray();
        }

    }
}