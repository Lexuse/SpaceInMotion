using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //переменная для регулировки силы урона наносимого от пули
    public int damage;

    //кому принадлежит пуля, игроку или врагу
    public bool is_Enemy_Bullet;

    //метод разрушения пули
    public void Destruction()
    {
        Destroy(gameObject);
    }

    //логика для столкновения пули с врагом или игроком
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (is_Enemy_Bullet && collision.tag == "Player")
        {
            Player.instance.GetDamage(damage);
            Destruction();
        }


        else if (!is_Enemy_Bullet && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            Destruction();
        }
            //вызов метода повреждения щита у игрока
        else if (is_Enemy_Bullet && collision.tag == "Shield")
        {
            Player.instance.GetDamageShield(damage);
            Destruction();
        }
            

    }

   


}
