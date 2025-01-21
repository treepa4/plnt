using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BallPrefab; // Префаб пули
    public float LaunchForce = 100; // Сила выстрела

    private Plane plane; // Плоскость для определения точки попадания
    private Transform visuals; // Визуализация оружия

    private void Start()
    {
        plane = new Plane(Vector3.right, transform.position); // Плоскость, параллельная оси YZ
        visuals = transform.GetChild(0); // Предполагается, что первый дочерний объект — визуальная модель
    }

    private void Update()
    {
        // Луч от камеры через позицию мыши
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hit;
        if (plane.Raycast(ray, out hit))
        {
            Vector3 hitPoint = ray.GetPoint(hit);

            // Расчет направления в плоскости YZ
            Vector3 direction = (hitPoint - transform.position).normalized;
            direction.x = 0; // Фиксируем ось X

            // Ограничиваем вращение только в плоскости YZ
            float angle = Mathf.Atan2(direction.z, direction.y) * Mathf.Rad2Deg;
            visuals.localRotation = Quaternion.Euler(0, 0, angle);

            // Выстрел по клику мыши
            if (Input.GetMouseButtonDown(0))
            {
                Shoot(direction);
            }
        }
    }

    private void Shoot(Vector3 direction)
    {
        // Создаем объект пули
        GameObject ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

        // Проверяем наличие Rigidbody
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * LaunchForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("BallPrefab не содержит компонента Rigidbody!");
        }
    }
}
