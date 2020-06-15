using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ShootingSetting
{
    //переменная для настройки шанса выстрела врага в волне
    [Range(0, 100)]
    public int shoot_Chance;
    //интервал внутри которого будет происходить выстрел
    public float shoot_Time_Min, shoot_Time_Max;

}


public class Wave : MonoBehaviour

{
    //ссылка на класс настройки выстрелов
    public ShootingSetting shooting_Setting;
    //отступ в инспекторе
    [Space]

    //переменная хранения врага, которого мы будем генерировать в конкретной волне
    public GameObject obj_Enemy;
    //количество врагов в волне
    public int count_in_Wave;
    //скорость с которой враг будет двигаться
    public int speed_Enemy;
    //задержка между генерацией врагов в волне
    public float time_Spawn;
    //массив для хранения точек по которым будет двигаться влолна
    public Transform[] path_Points;
    //логическая переменная для определения поведения врага в конце пути (продолжит путь сново либо будет уничтожен)
    public bool is_return;

    //логическая переменная для теста. при истинном значении будем гнерерировать данную волну каждые пять секунд (для отладки)
    [Header("Test Wawe!")]
    public bool is_Test;


    private FollowThePath follow_Component;
    //переменная типа Enemy, через нее мы будем передавать данные врагу
    private Enemy _enemy_Component_Script;

    private void Start()
    {
        //сопрограмма генерирующая волну
        StartCoroutine(CreateEnemyWawe());
    }


    IEnumerator CreateEnemyWawe()
    {
        for (int i =0; i<count_in_Wave; i++)
        {
            //создадим экзепляр врага и поместим его в переменну
            GameObject new_Enemy = Instantiate(obj_Enemy, obj_Enemy.transform.position, Quaternion.identity);
            //получим ссылку на компонент FollowThePath который на созданном враге должен появиться
            follow_Component = new_Enemy.GetComponent<FollowThePath>();
            //через ссылку перередадим точки пути по которым должен перемещаться враг
            follow_Component.path_Points = path_Points;
            //также передадим скорость с которой он должен перемещаться
            follow_Component.speed_Enemy = speed_Enemy;
            // значение логической переменной также передаем (должен ли враг быть уничтожен в конце пути)
            follow_Component.is_return = is_return;

            //получим ссылку на компонент Enemy, который висит на созданном враге
            _enemy_Component_Script = new_Enemy.GetComponent<Enemy>();
            //через эту ссылку передадим врагу шанс выстрела
            _enemy_Component_Script.shoot_Chance = shooting_Setting.shoot_Chance;
            //передаем интервал внутри которого будет происходить выстрел
            _enemy_Component_Script.shoot_Time_Min = shooting_Setting.shoot_Time_Min;
            _enemy_Component_Script.shoot_Time_Max = shooting_Setting.shoot_Time_Max;

            new_Enemy.SetActive(true);
            //задержка перед созданием нового врага
            yield return new WaitForSeconds(time_Spawn);
        }

        //ели переменная для теста активна - ждем пять секунд и запускаем волну
         if (is_Test)
        {
            //бесконечная генерация волн
            yield return new WaitForSeconds(5f);
            StartCoroutine(CreateEnemyWawe());
        }

         if (!is_return)
        {
            Destroy(gameObject);
        }
    }

    //визуализируем путь волны для настройки
    private void OnDrawGizmos()
    {
        NewPositionByPath(path_Points);
    }

    void NewPositionByPath(Transform[] path) 
    {
        Vector3[] path_Positions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++) 
        {
            path_Positions[i] = path[i].position;
        }

        //сделаем пути следования волны плавными, без резких изгибов (запустим функцию трижды для усиления сглаживания)
        path_Positions = Smoothing(path_Positions);
        path_Positions = Smoothing(path_Positions);
        path_Positions = Smoothing(path_Positions);

        for (int i =0; i < path_Positions.Length -1; i++)
        {
            Gizmos.DrawLine(path_Positions[i], path_Positions[i + 1]);
        }
    }


    //функция сглаживания пути
    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i =0; i<path_Positions.Length -2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.21f;
            j += 2;
        }
        return new_Path_Positions;
    }

}
