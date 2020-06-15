using System.Collections;
using System.Collections.Generic;
using UnityEngine;





//сериализуем класс созданный для ограничения выхода игрока за поля экрана. Это позволяет нам видеть поля в инспекторе
[System.Serializable]
public class Borders
{
    //Добавим четыре переменные, в которых будут храниться отступы от левой, правой, верхней и нижних границ экрана
    public float minX_Offset = 1.1f;
    public float maxX_Offset = 1.1f;
    public float minY_Offset = 1.1f;
    public float maxY_Offset = 1.1f;

    //Добавим еще несколько публичных полей, скроем их от инспектора. Они служебные и нужны лишь для расчетов
    [HideInInspector]
    public float minX, maxX, minY, maxY;
}





public class PlayerMoving : MonoBehaviour
{
    //Статическая ссылка на самого себя, через эту ссылку мы сможем менять
    //скорость игрока из других скриптов (Static refernce to the PlayerMoving)
    public static PlayerMoving instance;

    //ссылка на класс определяющий границы движения игрока
    public Borders borders;

    //Переменная скорости игрока
    public int speed_Player = 5;

    //ссылка на камеру, она нужна для взаимодействия с экраном
    private Camera _camera;

    //Хранение координат от нажатия на экран (XY)
    private Vector2 _mousePosition;



    private void Awake()
    {
        //настраиваем ссылку на самого себя, если в переменной пусто то добавляем ссыку на этот скрипт
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //но если на сцене уже есть объекты с этим скриптом, то мы их просто удаляем
            Destroy(gameObject);
        }

        //настроим ссылку на нашу камеру
        _camera = Camera.main;
    }


    private void Start()
    {
        //Вызываем метод расчета границ, который будет ограничивать перемещения игрока
        ResizeBorders();
    }


    private void Update()
    {
        //Проверка условия нажатия левой кнопки мыши по экрану (это работает и для тачскрина)
        if (Input.GetMouseButton(0))
        {
            //если нажатие на экран было произведено, записывам координаты в нашу переменную, созданную для их хранения
            _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            //для того, чтобы корабль не прятался под мышкой, или пальцем на экране тачскрина добавим 1.5 по ос Y
            _mousePosition.y += 1.5f;
            // теперь можно перемещать нашего игрока в записанные координаты. 
            transform.position = Vector2.MoveTowards(transform.position, _mousePosition, speed_Player * Time.deltaTime);      
        }

        //Ограничиваем движение игрока в рамках экрана
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                                         Mathf.Clamp(transform.position.y, borders.minY, borders.maxY));
    }


    //Создаем метод расчета границ, исползуя значения отступов и камеры. Он будет работать с любыми экранами и создавать правильные границы
    private void ResizeBorders()
    {
        //Левая граница
        borders.minX = _camera.ViewportToWorldPoint(Vector2.zero).x + borders.minX_Offset;
        //Нижняя граница
        borders.minY = _camera.ViewportToWorldPoint(Vector2.zero).y + borders.minY_Offset;
        //Правая граница
        borders.maxX = _camera.ViewportToWorldPoint(Vector2.right).x - borders.maxX_Offset;
        //Верхняя граница
        borders.maxY = _camera.ViewportToWorldPoint(Vector2.up).y - borders.maxY_Offset;
    }

}
