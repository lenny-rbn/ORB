using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 posA;
    [SerializeField] private Vector3 posB;

    private float lerp;
    private float step;

    void Start()
    {
        lerp = 0;
        step = 1;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(posA, posB, lerp);
        lerp += Time.deltaTime * speed * step;

        if(lerp < 0 || lerp > 1)
        {
            step *= -1;
            transform.position = Vector3.Lerp(posA, posB, lerp);
        }
    }
}
