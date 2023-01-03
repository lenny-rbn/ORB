using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] bool desactivateOnTrigger;
    [SerializeField] GameObject[] objects;

    void Start()
    {
        if (desactivateOnTrigger)
            foreach (GameObject obj in objects)
                obj.SetActive(true);
        else
            foreach (GameObject obj in objects)
                obj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovePlayer>() != null)
        {
            if (desactivateOnTrigger)
                foreach (GameObject obj in objects)
                    obj.SetActive(false);
            else
                foreach (GameObject obj in objects)
                    obj.SetActive(true);
        }
    }
}
