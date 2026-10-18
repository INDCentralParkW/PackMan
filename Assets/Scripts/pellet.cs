using UnityEngine;

public class Pellet : MonoBehaviour
{
    public GameState gameState;
    private void EatPellet()
    {
        Destroy(gameObject);
        gameState.score += 10;
        Debug.Log(gameState.score);
    }
    
    void OnTriggerStay2D(Collider2D other) {
        bool isCollidingWithPacman = other.gameObject.CompareTag("Packman");
        if (isCollidingWithPacman) {
            Vector2 pacmanPosition = other.bounds.center;
            Vector2 pelletPosition = GetComponent<Collider2D>().bounds.center;
            bool isWithinDistance = Vector2.Distance(pacmanPosition,pelletPosition) <= 0.25f;
            if (isWithinDistance) EatPellet();
        }
    }
}
