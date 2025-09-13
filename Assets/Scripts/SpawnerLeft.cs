using UnityEngine;
using TMPro;

public class SpawnerLeft : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    float timer;
    [SerializeField] float intervalo;
    [SerializeField] GameObject[] Spawns;
    bool up;
    float originalIntervalo;
    [SerializeField] TextMeshProUGUI timerText;

    void Start()
    {
        originalIntervalo = intervalo;
        Invoke("Spawn", intervalo);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
        timerText.text = "Tiempo: " + Mathf.FloorToInt(timer);
    }

    void Spawn()
    {
        int i = UnityEngine.Random.Range(0, Spawns.Length);

        int random = UnityEngine.Random.Range(0, Enemies.Length);

        GameObject temporalEnemy = Instantiate(Enemies[random], Spawns[i].transform.position, Quaternion.identity);
        temporalEnemy.GetComponent<Rigidbody2D>().linearVelocityX = 3 + (timer / 3);
        float r = UnityEngine.Random.Range(1, 3);
        intervalo = r;
        Invoke("Spawn", intervalo);
        up = !up;


    }
}
