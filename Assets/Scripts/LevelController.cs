using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [System.Serializable]
  public class EnemyWaves
{
    //время через которое мы будем создавать волну
    public float timeToStart_Wave;
    //переменна для хранения волны, которую создадим
    public GameObject wave;
    //логическая переменная условия окончания игры
    public bool is_Last_Wave;

}



public class LevelController : MonoBehaviour
{

    //ссылка на самого себя
    public static LevelController instance;
    //массив для хранения кораблей игрока
    public GameObject[] playerShip;
    //массив для хранения вражеских волн
    public EnemyWaves[] enemyWaves;

    //окончание игры
    private bool is_Final_Game = false;



    private void Awake()
    {
        //настраиваем ссылку на самго себя
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //создаем все вражеские волны
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            //запускаем сопрограмму, которая будет принимать два значения.
            //превое - время когда появится волна. второе - какой тип волны будет создан
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart_Wave, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }

    private void Update()
    {
        //Логика выигрыша
        if (is_Final_Game == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("Win");
        }

        //логика проигрыша
        if (Player.instance == null)
        {
            Debug.Log("Lose");
        }

    }

    IEnumerator CreateEnemyWave(float delay, GameObject Wave, bool final)
    {
        //если время создания волны не равна нулю и игрок жив - создаем волну
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
            Instantiate(Wave);
        if (final == true)
            is_Final_Game = true;
    }

}
