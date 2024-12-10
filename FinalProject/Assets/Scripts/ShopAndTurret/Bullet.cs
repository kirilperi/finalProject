using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // �������� ����
    public float lifeTime = 5f; // ����� ������������� ����
    public int damage = 10; // ���� ����

    void Start()
    {
        Destroy(gameObject, lifeTime); // ���������� ���� ����� �������� �����
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // ������� ���� ������
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // ������� ����� (�������� ���������� �������� �����)
            Destroy(other.gameObject); // ���������� �����
            Destroy(gameObject); // ���������� ����
        }
    }
}