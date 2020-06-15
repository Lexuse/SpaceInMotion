using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{

    //массив содержащий точки пути по которым будет перемещаться враг
    [HideInInspector] public Transform[] path_Points;

    //скорость с которой будет перемещаться враг
    [HideInInspector] public float speed_Enemy;

    //Булевая переменная отвечаюшая за то будет ли уничтожен враг в конце своего пути, или продолжит движение
    [HideInInspector] public bool is_return;

    //массив для хранения векторов наших точек перемещения
    [HideInInspector] public Vector3[] new_Position;

    //переменная для хранения порядкового номера точки пути, благодаря ей враг будет знать куда ему двигаться
    private int cur_Pos;




    private void Start()
    {
        //поместим значения наших точек пути для в массив для хранения векторов
        new_Position = NewPositionByPath(path_Points);

        //точка появления врага
        transform.position = new_Position[0];
    }


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new_Position[cur_Pos], speed_Enemy * Time.deltaTime);
        //если враг добрался до точки, добавляем единицу к порядковому номеру точки пути
        if (Vector3.Distance(transform.position, new_Position[cur_Pos]) < 0.2f)
        {
            cur_Pos++;
            if (is_return && Vector3.Distance(transform.position, new_Position[new_Position.Length - 1]) < 0.3f)
            {
                cur_Pos = 0;
            }
        }

        if (Vector3.Distance(transform.position, new_Position[new_Position.Length - 1]) < 0.2f && !is_return)
        {
            Destroy(gameObject);
        }
    }



    Vector3[] NewPositionByPath(Transform[] pathPos)
    {
        Vector3[] pathPositions = new Vector3[pathPos.Length];
        for (int i =0; i < path_Points.Length; i++)
        {
            pathPositions[i] = pathPos[i].position;
        }
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        return pathPositions;
    }

    //функция сглаживания пути
    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.21f;
            j += 2;
        }
        return new_Path_Positions;
    }
}
