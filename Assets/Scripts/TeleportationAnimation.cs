using UnityEngine;

public class TeleportationAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnHoverAnimation(bool isHover)
    {
        animator.SetBool("Hover", isHover);
    }
}
