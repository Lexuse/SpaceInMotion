using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{

    // Скорость перемещения объекта
    public float speed;

    private void Update()
    {
        //Перемещаем объект по вертикальной плоскости
        //направление вверх или вниз завсисит от значения скорости
        transform.Translate(Vector3.up * speed * Time.deltaTime);

    }

}
