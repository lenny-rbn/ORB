using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] bool triggerOnce;
    [SerializeField] string triggerName;
    [SerializeField] GameObject objectToAnim;

    private bool triggered;
    private Animator animator;

    void Start()
    {
        animator = objectToAnim.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator != null && other.GetComponent<MovePlayer>() != null)
        {
            if (triggerOnce)
            {
                if (!triggered)
                {
                    DoAnimation();
                    triggered = true;
                }
            }
            else
            {
                DoAnimation();
            }
        }
    }

    private void DoAnimation() { animator.SetTrigger(triggerName); }
}
