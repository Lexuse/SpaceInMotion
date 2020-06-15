using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackgound : MonoBehaviour
{

    //Переменная для хранения высоты спрайтов в пикселях
    //высота изображения должна быть выше высоты камеры чобы не видеть стыков изображения. Для определения высоты спрайта можно использовать BoxCollider
    public float vertical_Size;

    //Переменная для расчета высоты на которую должен будет подняться спрайт. Заисит от высоты спрайта.
    private Vector2 _offset_Up;


    private void Update()
    {
        //Проверяем, находится ли спрайт ниже своей высоты
        if (transform.position.y < -vertical_Size)
        {
          RepeatBackground();
        }
        
    }


    //Метод для повторения фона. Он будет перемещать спрайты друг за другом, создавая впечатление бесконечного фона
    void RepeatBackground()
    {
        //Расчет смещения для приватной переменной
        _offset_Up = new Vector2(0, vertical_Size * 2);

        transform.position = (Vector2)transform.position + _offset_Up;
    }
}
    