using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenShake : MonoBehaviour
{
    [Header("Wall Break")]
    [SerializeField] private float magnitude_WB;
    [SerializeField] private float duration_WB;
    [SerializeField] private AnimationCurve curve_WB;

    [Header("Enemy Death")]
    [SerializeField] private float magnitude_ED;
    [SerializeField] private float duration_ED;
    [SerializeField] private AnimationCurve curve_ED;

    [Header("Sticked Ball Recal")]
    [SerializeField] private float magnitude_SBR;
    [SerializeField] private float duration_SBR;
    [SerializeField] private AnimationCurve curve_SBR;

    [Header("Trigger Pendulum")]
    [SerializeField] private float magnitude_TB;
    [SerializeField] private float duration_TB;
    [SerializeField] private AnimationCurve curve_TB;

    private float timer;
    private float duration;
    private float magnitude;
    private float magnitudeMax;
    private Vector3 camPos;

    private Camera cameraClass;
    private AnimationCurve curve;

    private void Start()
    {
        cameraClass = GetComponent<Camera>();
        timer = 0;
    }

    private void screenShaking()
    {
        if (timer < duration)
        {
            float value = curve.Evaluate( timer / duration );
            magnitude = Mathf.Lerp(0f, magnitudeMax, value);

            transform.position = camPos + magnitude * (Vector3)Random.insideUnitCircle;
            timer += Time.deltaTime;
            cameraClass.SetShaking(true);
        }
        else
        {
            cameraClass.SetShaking(false);
        }
    }

    public void DoScreenShake()
    {
        if (GameEvent.wallBreak)
        {
            magnitudeMax = magnitude_WB;
            duration = duration_WB;
            curve = curve_WB;
            timer = 0;
        }

        if (GameEvent.enemyDeath)
        {
            magnitudeMax = magnitude_ED;
            duration = duration_ED;
            curve = curve_ED;
            timer = 0;
        }

        if (GameEvent.recallBall && OrangeMove.sticked)
        {
            magnitudeMax = magnitude_SBR;
            duration = duration_SBR;
            curve = curve_SBR;
            timer = 0;
        }

        if (GameEvent.pendulumTriggered)
        {
            magnitudeMax = magnitude_TB;
            duration = duration_TB;
            curve = curve_TB;
            timer = 0;
        }

        if (cameraClass.follow)
            camPos = cameraClass.GetBasicPosition();
        else
            camPos = cameraClass.currentFixZone.GetCamPosition();

        screenShaking();
    }
}