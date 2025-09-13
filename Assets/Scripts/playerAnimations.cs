using UnityEngine;

public class playerAnimations : MonoBehaviour
{
    [Header("Animator Parameters")]
    [SerializeField] string pIsRunning = "IsRunning";
    [SerializeField] string pIsCrouching = "IsCrouching";
    [SerializeField] string pJump = "Jump";
    [SerializeField] string pDeath = "Death";

    Animator anim;
    int hIsRunning, hIsCrouching, hJump, hDeath;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hIsRunning = Animator.StringToHash(pIsRunning);
        hIsCrouching = Animator.StringToHash(pIsCrouching);
        hJump = Animator.StringToHash(pJump);
        hDeath = Animator.StringToHash(pDeath);
    }

    public void SetRunning(bool value) => anim.SetBool(hIsRunning, value);
    public void SetCrouching(bool value) => anim.SetBool(hIsCrouching, value);
    public void PlayJump() => anim.SetTrigger(hJump);
    public void PlayDeath() => anim.SetTrigger(hDeath);
}
