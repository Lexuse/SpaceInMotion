using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Boss")]
    public bool is_Boss;
    //переменная для хранения пули босса
    public GameObject obj_Bullet_Boss;
    //задержка между супер выстрелами боса
    public float time_Bullet_Boss_Spawn;
    //переменная для таймера
    private float _timer_Shot_Boss;
    //шанс выстрела, с помощью этой переменной будем настраивать сложность
    public int shot_Chance_Boss;

    //Переменная для хранения количества жизни врага
    public int enemy_Health;
    [Space]
    //перемення для хранения пули врага
    public GameObject obj_Bulllet;
    //интервал времени внутри которого враг будет производить выстрел.
    //это необходимо, чтобы он не стрелял в те промежутки времени когда его не видит игрок
    public float shoot_Time_Min, shoot_Time_Max;
    //шанс выстрела, чтобы не все враги производили выстрел
    public int shoot_Chance;


    private void Start()
        //если враг не босс - делаем один выстрел и больше ничего
        
    {
        if (!is_Boss)
        {
            Invoke("OpenFire", Random.Range(shoot_Time_Min, shoot_Time_Max));
        }
    }


    private void Update()
    {
        if (is_Boss)
        {
            if (Time.time > _timer_Shot_Boss)
            {
                _timer_Shot_Boss = Time.time + time_Bullet_Boss_Spawn;
                OpenFire();
                OpenFireBoss();
            }
        }
    }


    private void OpenFire()
    {
        if (Random.value < (float)shoot_Chance / 100)
        {
            //создаем пулю из позиции врага, без вразения
            Instantiate(obj_Bulllet, transform.position, Quaternion.identity);
        }
    }


    private void OpenFireBoss()
    {
        //условия на шанс выстрела
        if (Random.value < (float)shot_Chance_Boss / 100)
        {
            //чтобы за один раз создать несколько пуль, исползуем цикл(меняем угол по оси Z)
            for (int zZz = -40; zZz <40; zZz += 10)
            {
                Instantiate(obj_Bullet_Boss, transform.position, Quaternion.Euler(0, 0, zZz));
            }
        }
    }


    //метод получения урона врагом
    public void GetDamage(int damage)
    {
        //уменьшаем очки жизни на количество полученного урона
        enemy_Health -= damage;

        //если жизни больше нет вызываем разрушение  врага
        if(enemy_Health <= 0)
        {
            Destruction();
        }
    }

    //Опишем метод разрушения врага
    public void Destruction()
    {
        Destroy(gameObject);
    }

    //действие при столконовении врага с игроком
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetDamage(1);
            Player.instance.GetDamage(1);
        }
        if (collision.tag == "Shield")
        {
            GetDamage(1);
            Player.instance.GetDamageShield(1);
        }
    }
}
