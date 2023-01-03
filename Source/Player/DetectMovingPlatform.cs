using UnityEngine;

public class DetectMovingPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12) // 12 : moving platforms layer
            transform.SetParent(collision.gameObject.transform.parent, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 12) // 12 : moving platforms layer
        {
            transform.SetParent(null, true);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
