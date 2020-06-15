using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsAndBonus : MonoBehaviour
{
    //переменная для хранения бонуса
    public GameObject obj_bonus;
    //задержка между генерацией бонусов
    public float time_Bonus_Spawn;
    //Массив содержащий планеты для генерации
    public GameObject[] obj_Planets;
    //задержка между генерацией планет
    public float time_Planet_Spawn;
    //Скорость перемещения планет
    public float speed_Planet;
    //список запрещающий дублирование планет подряд
    List<GameObject> planetList = new List<GameObject>();


    private void Start()
    {
        //Запуск функции генерации планет
        StartCoroutine(PlanetsCreation());
        StartCoroutine(BonusCreation());
    }

   IEnumerator PlanetsCreation()
    {
        //Добавим планеты в список исползуя цикл
        for (int i = 0; i < obj_Planets.Length; i++)
        {
            planetList.Add(obj_Planets[i]);
         }
        //После заполнения списка ждем 7 секунд и запускаем выполнение кода
        yield return new WaitForSeconds(7);

        //Создаем планеты в бесконечном цикле
        while (true)
        {
            //выбираем случайную планету из списка
            int randomIndex = Random.Range(0, planetList.Count);
            GameObject newPlanet = Instantiate(planetList[randomIndex],
                new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                PlayerMoving.instance.borders.maxY * 2f), Quaternion.Euler(0, 0, Random.Range(-25, 25)));
            //после создания планеты удаляем ее из списка, чтобы она не дублировалась
            planetList.RemoveAt(randomIndex);
            //если список стал пустым, мы заполняем его заново
            if (planetList.Count == 0)
            {
                for (int i = 0; i < obj_Planets.Length; i++)
                {
                    planetList.Add(obj_Planets[i]);
                }
            }
            //У созданной планеты мы находим компонент objectMoving и задаем скорость с которой она будет двигаться
            newPlanet.GetComponent<ObjectMoving>().speed = speed_Planet;

            //добавим паузу, после которой наш код повториться и появиться новая планета
             yield return new WaitForSeconds(time_Planet_Spawn);
        }
    }

    IEnumerator BonusCreation()
    {
        while (true)
        {
            //задержка перед выплнением кода
            yield return new WaitForSeconds(time_Bonus_Spawn);
            //создаем бонус с учетом ограничения движения игрока и выше видимости камеры      
            Instantiate(obj_bonus, new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                PlayerMoving.instance.borders.maxY * 1.5f), Quaternion.identity);
        }
    }
}
