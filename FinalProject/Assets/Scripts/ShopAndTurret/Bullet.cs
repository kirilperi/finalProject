using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Скорость пули
    public float lifeTime = 5f; // Время существования пули
    public int damage = 10; // Урон пули

    void Start()
    {
        Destroy(gameObject, lifeTime); // Уничтожить пулю через заданное время
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // Двигаем пулю вперед
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Уронить врага (добавьте реализацию здоровья врага)
            Destroy(other.gameObject); // Уничтожаем врага
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}