using UnityEngine;

public class PelletB : MonoBehaviour
{
    public GameState gameState;
    private void EatPellet()
    {
        gameState.score += 50;
        Debug.Log(gameState.score);
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        bool isCollidingWithPacman = other.gameObject.CompareTag("Packman");
        if (isCollidingWithPacman)
        {
            Vector2 pacmanPosition = other.bounds.center;
            Vector2 pelletPosition = GetComponent<Collider2D>().bounds.center;
            bool isWithinDistance = Vector2.Distance(pacmanPosition, pelletPosition) <= 0.5f;
            if (isWithinDistance) EatPellet();
        }
    }
}