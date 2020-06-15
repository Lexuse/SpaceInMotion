using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{

    // ссылка на компонент коллайдер находящийся на объекте
    private BoxCollider2D _boundarie_Collider;

    //переменная для угла камеры
    private Vector2 _viewport_Size;


    private void Awake()
    {
        _boundarie_Collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        //метод для расчета коллайдера
        ResizeCollider();
    }

    //метод изменят размер коллайдера под любое разрешение экрана автоматически
    void ResizeCollider()
    {
        //получаем значение верхнего правого угла нашей камеры и умножаем на 2
        _viewport_Size = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        //умножаем ширину и высоту на 1.5
        _viewport_Size.x *= 1.5f;
        _viewport_Size.y *= 1.5f;
        //изменим размер коллайдера используя наши расчеты
        _boundarie_Collider.size = _viewport_Size;

    }

    //логика для объетов покидающих коллайдер, они подлежат уничтожению
    private void OnTriggerExit2D(Collider2D coll)
    {
        switch (coll.tag)
        {
            case "Planet":
                Destroy(coll.gameObject);
                break;

            case "Bullet":
                Destroy(coll.gameObject);
                break;
        } 
    }
}
