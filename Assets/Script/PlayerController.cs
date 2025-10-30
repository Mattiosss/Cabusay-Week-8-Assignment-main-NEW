using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float laneOffset = 2f;
    public float laneSwitchSpeed = 10f;
    public float jumpHeight = 2f;
    public float jumpDuration = 0.6f;

    public float HP = 100f;
    private float maxHP = 100f;
    public TextMeshProUGUI hpText;

    private int currentLane = 1;
    public bool isJumping = false;
    private float jumpTimer = 0f;
    private Vector3 startPos;
    public Vector3 playerPos;

    void Start()
    {
        startPos = transform.position;
        playerPos = startPos;
        UpdateHPUI();
        InvokeRepeating(nameof(RegenerateHP), 1f, 1f);
    }

    void Update()
    {
        HandleInput();
        HandleJump();

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(5);
            Debug.Log($"Manual Damage Test: HP = {HP}");
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(0, currentLane - 1);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(2, currentLane + 1);
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            StartJump();
    }

    void HandleJump()
    {
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float t = jumpTimer / jumpDuration;
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;
            Vector3 pos = startPos;
            pos.y += height;
            pos.x = (currentLane - 1) * laneOffset;
            transform.position = pos;
            playerPos = pos;

            if (jumpTimer >= jumpDuration)
            {
                isJumping = false;
                transform.position = new Vector3((currentLane - 1) * laneOffset, startPos.y, startPos.z);
                playerPos = transform.position;
            }
        }
        else
        {
            Vector3 target = new Vector3((currentLane - 1) * laneOffset, startPos.y, startPos.z);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * laneSwitchSpeed);
            playerPos = transform.position;
        }
    }

    void StartJump()
    {
        isJumping = true;
        jumpTimer = 0f;
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP < 0) HP = 0;

        Debug.Log($"Player took {amount} damage! Remaining HP: {HP}");
        UpdateHPUI();
    }

    void RegenerateHP()
    {
        HP = Mathf.Min(maxHP, HP + 1);
        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        if (hpText != null)
            hpText.text = $"HP: {Mathf.RoundToInt(HP)}";
    }
}
