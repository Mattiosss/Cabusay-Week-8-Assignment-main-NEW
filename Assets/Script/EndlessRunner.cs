using UnityEngine;

public class EndlessRunner : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
        if (player.HP <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }
}
