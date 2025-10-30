using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 itemPosition;
    private SpriteRenderer spriteRenderer;
    public float damage = 10f;
    private PlayerController player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.color = GetRandomColor();

        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        itemPosition.z -= Time.deltaTime * 5f;

        float perspective = CameraComponent.focalLenth / (CameraComponent.focalLenth + itemPosition.z);
        Vector3 visualPosition = new Vector3(itemPosition.x * perspective, itemPosition.y * perspective, 0);

        transform.position = visualPosition;
        transform.localScale = Vector3.one * perspective;

        CheckHit();
    }

    void CheckHit()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 0.7f)
        {
            player.TakeDamage(damage);
            Debug.Log($"Player hit! -{damage} HP (remaining {player.HP})");
            Destroy(gameObject);
        }
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
