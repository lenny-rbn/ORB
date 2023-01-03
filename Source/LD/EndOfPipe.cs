using UnityEngine;

public class EndOfPipe : MonoBehaviour
{
    [Header("Send To")]
    [SerializeField] GameObject Pos;
    [SerializeField] Vector3 ExitDir;

    private void OnTriggerEnter(Collider other)
    {
        OrangeMove ball = other.GetComponent<OrangeMove>();

        if (ball != null)
        {
            if (ball.CanTakePipes())
            {
                other.transform.position = Pos.transform.position;
                ball.ExitFromPipe();
                ball.SetDirection(ExitDir);
                GameEvent.ballGoInPipe = true;
            }
        }
    }
}
