using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject clearText;
    //���x���f�U�C���p�̔z��̐錾    
    int[,] map;
    //�Q�[���Ǘ��p�̔z��
    GameObject[,] field;
    
    //���\�b�h
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
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0) ||
            moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }
        //����������
        //null�`�F�b�N
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
                  //�i�[�ꏊ�̃C���f�b�N�X���T���Ă���
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for(int i=0; i<goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
               
                //��ł������Ȃ�������������B��
                return false;
            }
        }
        return true;
    }
    

    // Start is called before the first frame update
    void Start()
    {

        
        //�z��̏�����
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
        //�E�����ƉE�ɓ���
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex_m = GetPlayerIndex();
            playerIndex_m.x +=  1;
            MoveNumber("Player", playerIndex, playerIndex_m);
        }
        //��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex_m = GetPlayerIndex();
            playerIndex_m.x -= 1;
            MoveNumber("Player", playerIndex, playerIndex_m);
        }
        //��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex_m = GetPlayerIndex();
            playerIndex_m.y += 1;
            MoveNumber("Player", playerIndex, playerIndex_m);
        }
        //��
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
            //�f�o�b�O
            Debug.Log("Clear!");

        }

    }
}
