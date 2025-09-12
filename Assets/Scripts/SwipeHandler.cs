using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private Vector2 starTouch;
    private Vector2 endTouch;
    private bool swipeDetected = false;

    [Header("SwipeConfi")]
    [SerializeField] private float swipeDistance;

    private void Update()
    {
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        starTouch = touch.position;
                        swipeDetected = true;
                        break;

                    case TouchPhase.Moved:
                        endTouch = touch.position;
                        break;

                    case TouchPhase.Ended:
                        if (swipeDetected)
                        {
                            DetectSwipe();
                            swipeDetected = false;
                        }
                        break;
                }
            }
        }
    }
    void DetectSwipe()
        {
            Vector2 swipeDelta = endTouch - starTouch;

            if (swipeDelta.magnitude < swipeDistance) return;

            float vertical = Mathf.Abs(swipeDelta.y);
            float horizontal = Mathf.Abs(swipeDelta.x);

            if (vertical > horizontal)
            {
                if (swipeDelta.y > 0)
                {
                    player.OnJumpButtonPressed();
                    Debug.Log("Up");
                }
                else
                {
                    player.OnDuckButtonDown();
                    Debug.Log("Down");
                    Invoke(nameof(ResetDuck), 0.5f);
                }
            }
        }

        void ResetDuck()
        {
            player.OnDuckButtonUp();
        }
}