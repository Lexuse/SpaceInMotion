using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public int player_Health = 1;

    //ссылка на объект щит (бонус)
    public GameObject obj_Shield;
    //переменная для хранения очков жизни щита
    public int shield_Health = 1;
    //ссылка на ползунок жизни игрока
    private Slider _slider_Hp_Plalyer;
    //ползунок щита
    private Slider _slider_Shield_Player;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _slider_Hp_Plalyer = GameObject.FindGameObjectWithTag("SL_Hp").GetComponent<Slider>();
        _slider_Shield_Player = GameObject.FindGameObjectWithTag("SL_Shield").GetComponent<Slider>();
    }


    private void Start()
    {
        //если у щита есть очки жизни - сделать его видимым
        if (shield_Health != 0)
        {
            obj_Shield.SetActive(true);
        }

        else
            //в ином случае скрываем щит
            obj_Shield.SetActive(false);
            _slider_Shield_Player.value = 0;
        //

        //установим ползунок равный очкам жизни игрока
        _slider_Hp_Plalyer.value = (float)player_Health / 10f; //делим на 10 потому, что значение слайдера от нуля до единицы
        _slider_Shield_Player.value = (float)shield_Health / 10f;

    }

    //метод получения урона щита
    public void GetDamageShield(int damage)
    {
        //уменьшаем очки жизни щита на количество получаемого урона
        shield_Health -= damage;
        //обновляем слайдер
        _slider_Shield_Player.value = (float)shield_Health / 10;

        if (shield_Health <= 0)
        {
            obj_Shield.SetActive(false);
        }
    }

    //получение урона игроком
    public void GetDamage(int damage)
    {
        player_Health -= damage;
        //обновляем слайдер
        _slider_Hp_Plalyer.value = (float)player_Health / 10f;

        if (player_Health <= 0)
        {
            Destruction();
        }
    }

    public void Destruction()
    {
        Destroy(gameObject);
    }
}
