using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FixZone : MonoBehaviour
{
    [SerializeField] float transitionDistance = 1;
    [SerializeField] GameObject cameraPosition;
    [SerializeField] GameObject cameraObj;
    [SerializeField] GameObject player;

    private bool playerInZone;
    private float centerSize;
    private Quaternion beginCameraRotation;

    private BoxCollider box;
    private Camera cameraComp;

    private void Start()
    {
        cameraComp = cameraObj.GetComponent<Camera>();
        box = GetComponent<BoxCollider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovePlayer>() != null)
        {
            beginCameraRotation = cameraObj.transform.rotation;
            centerSize = box.size.x - transitionDistance * 2;

            cameraComp.follow = false;
            playerInZone = true;
            cameraComp.currentFixZone = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovePlayer>() != null)
        {
            cameraComp.follow = true;
            playerInZone = false;
        }
    }

    private float GetTransitionValue()
    {
        if (player.transform.position.x < transform.position.x)
            return (player.transform.position.x - transform.position.x + box.size.x / 2) / transitionDistance;
        else
            return 1 - (player.transform.position.x - transform.position.x - centerSize / 2) / transitionDistance;
    }

    public Vector3 GetCamPosition()
    {
        return Vector3.Lerp(cameraComp.GetBasicPosition(), cameraPosition.transform.position, GetTransitionValue());
    }

    public Quaternion GetCamRotation()
    {
        return Quaternion.Lerp(beginCameraRotation, cameraPosition.transform.rotation, GetTransitionValue());
    }


    private void Update()
    {
        if (playerInZone)
        {
            cameraComp.ResetBaseY();

            cameraObj.transform.position = GetCamPosition();

            cameraObj.transform.rotation = GetCamRotation();

            if (cameraComp.HaveScreenShake())
                cameraComp.GetScreenShake().DoScreenShake();
        }
    }
}
