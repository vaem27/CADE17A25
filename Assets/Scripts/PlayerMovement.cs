using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    Rigidbody2D rb2d;
    bool isGrounded;
    [SerializeField] private GameObject Top, Bottom;
    [SerializeField] private TextMeshProUGUI puntajeTextFinal;
    [SerializeField] private GameObject EndPanel;

    [Header("UI Buttons")]
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button duckButton;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        jumpButton.gameObject.AddComponent<JumpButtonHandler>().Init(this);
        duckButton.gameObject.AddComponent<DuckButtonHandler>().Init(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) Jump();
        if (Input.GetKeyDown(KeyCode.S)) Duck();
        if (Input.GetKeyUp(KeyCode.S)) Stand();
    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded)
            Jump();
    }

    public void OnDuckButtonDown()
    {
        Duck();
    }

    public void OnDuckButtonUp()
    {
        Stand();
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Duck()
    {
        Bottom.SetActive(true);
        Top.SetActive(false);
    }

    void Stand()
    {
        Top.SetActive(true);
        Bottom.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int p = FindFirstObjectByType<Puntaje>().GetPuntaje();
            puntajeTextFinal.text = "" + p;
            EndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler
{
    PlayerMovement player;

    public void Init(PlayerMovement p) => player = p;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.OnJumpButtonPressed();
    }
}

public class DuckButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerMovement player;

    public void Init(PlayerMovement p) => player = p;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.OnDuckButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.OnDuckButtonUp();
    }
}
