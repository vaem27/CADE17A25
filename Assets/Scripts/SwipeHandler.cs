using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [Header("Swipe Config")]
    [SerializeField] private float swipeDistance = 50f;
    [SerializeField] private float verticalBias = 1.5f;

    private Vector2 startTouch;
    private bool isDuckingBySwipe = false;
    private bool jumpedThisSwipe = false;

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(t.fingerId))
            return;

        switch (t.phase)
        {
            case TouchPhase.Began:
                startTouch = t.position;
                isDuckingBySwipe = false;
                jumpedThisSwipe = false;
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                {
                    Vector2 delta = t.position - startTouch;
                    if (delta.sqrMagnitude < swipeDistance * swipeDistance) break;

                    float v = Mathf.Abs(delta.y);
                    float h = Mathf.Abs(delta.x);

                    if (v < h * verticalBias) break;

                    if (delta.y > 0f)
                    {
                        if (!jumpedThisSwipe)
                        {
                            player.OnJumpButtonPressed();
                            jumpedThisSwipe = true;
                        }
                    }
                    else
                    {
                        if (!isDuckingBySwipe && player.IsGrounded)
                        {
                            player.OnDuckButtonDown();
                            isDuckingBySwipe = true;
                        }
                    }
                    break;
                }

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (isDuckingBySwipe)
                {
                    player.OnDuckButtonUp();
                    isDuckingBySwipe = false;
                }
                break;
        }
    }
}