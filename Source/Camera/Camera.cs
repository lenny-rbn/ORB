using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] float XSlideTime = 0.5f;
    [SerializeField] float YSlideTime = 1f;
    [SerializeField] float maxYOverlap;
    [SerializeField] Vector2 YRange;
    [SerializeField] Vector3 overlap;
    [SerializeField] Vector3 tilt;

    private bool isShaking;
    private bool slideOnY = false;

    public bool follow;
    private float lastMoveSign;
    private float baseYTemp = 0f;
    private float lerpTimer = 0f;
    private float YLerpTimer = 1f;
    private Vector3 basePlayerPos;

    public FixZone currentFixZone;
    private ScreenShake screenShake;
    private MovePlayer player;

    private void Start()
    {
        follow = true;
        isShaking = false;

        screenShake = GetComponent<ScreenShake>();
        player = FindObjectOfType<MovePlayer>();
        basePlayerPos = player.transform.position;
    }

    private void UpdateLerpTimer()
    {
        Vector2 movement = player.GetMoveVector();

        if (movement.x < 0.45f && movement.x > -0.45f)
        {
            if (lerpTimer > 0)
                lerpTimer -= Time.deltaTime;
        }
        else
        {
            if (Mathf.Sign(lastMoveSign) != Mathf.Sign(movement.x) && Mathf.Sign(lastMoveSign) != 0)
            {
                if (lerpTimer > 0)
                    lerpTimer -= Time.deltaTime;
                else
                    lastMoveSign = Mathf.Sign(movement.x);
            }
            else
            {
                if (lerpTimer < XSlideTime)
                    lerpTimer += Time.deltaTime;
                lastMoveSign = Mathf.Sign(movement.x);
            }
        }
    }

    private void SlideOnY()
    {
        if (slideOnY)
        {
            basePlayerPos.y = Mathf.Lerp(baseYTemp, player.transform.position.y, YLerpTimer / YSlideTime);

            if (YLerpTimer < YSlideTime)
            {
                YLerpTimer += Time.deltaTime;
            }
            else
            {
                slideOnY = false;
                YLerpTimer = 0;
                basePlayerPos.y = player.transform.position.y;
            }
        }
        else if (player.transform.position.y > basePlayerPos.y + YRange.y || player.transform.position.y < basePlayerPos.y + YRange.x)
        {
            slideOnY = true;
            YLerpTimer = 0;
            baseYTemp = basePlayerPos.y;
        }
    }


    public Vector3 GetBasicPosition() 
    { 
        return Vector3.Lerp(new Vector3(player.transform.position.x, basePlayerPos.y + overlap.y, basePlayerPos.z + overlap.z), 
                            new Vector3(player.transform.position.x + overlap.x * lastMoveSign, basePlayerPos.y + overlap.y, basePlayerPos.z + overlap.z), 
                            lerpTimer / XSlideTime); 
    }

    void Update()
    {
        if (follow)
        {
            if (screenShake != null)
                screenShake.DoScreenShake();

            if (!isShaking)
                transform.position = GetBasicPosition();    
            
            SlideOnY();
            UpdateLerpTimer();

            transform.rotation = Quaternion.Euler(tilt.x, tilt.y, tilt.z);
        }
    }

    public void ResetBaseY() { basePlayerPos.y = player.transform.position.y; }
    public void SetShaking(bool isShaking) { this.isShaking = isShaking; }
    public bool HaveScreenShake() { return screenShake != null; }
    public Vector3 GetOverlap() { return overlap; }
    public ScreenShake GetScreenShake() { return screenShake; }
}
