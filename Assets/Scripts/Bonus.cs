using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //проверяем текущий уровень режима стрельбы
            if (PlayerShooting.instance.cur_Power_Level_Guns < PlayerShooting.instance.max_Power_Level_Guns)
            {
                //повышаем уровень режима стрельбы
                PlayerShooting.instance.cur_Power_Level_Guns++;
            }
            //уничтожаем бонус
             Destroy(gameObject);
        }
        
    }

}
