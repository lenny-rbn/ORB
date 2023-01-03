using UnityEngine;

public class Brasero : MonoBehaviour
{
    [SerializeField] float activeTime;
    [SerializeField] float inactiveTime;

    private bool isActive = false;
    private float timer;
    private ParticleSystem particle;

    void Start()
    {
        timer = 0;
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OrangeMove ball = other.gameObject.GetComponent<OrangeMove>();
        if (ball != null && !ball.GetinHand() && isActive)
            ball.SetBuff(true);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (isActive)
            {
                particle.Stop();
                timer = inactiveTime;
                isActive = false;
            }
            else
            {
                timer = activeTime;
                particle.Play();
                isActive = true;
            }
        }
    }
}
