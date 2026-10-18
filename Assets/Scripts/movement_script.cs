using UnityEngine;

public class movement_script : MonoBehaviour
{  
    public float moveSpeed;
    private Animator animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private LayerMask _obstaclesLayer; 
    public Vector2 direction;
    public float rayLength;
    public Vector2 futureDirection;
    public bool canTeleport;
    void Start()
    {
        moveSpeed = 4F;
        animator = GetComponent<Animator>();
        animator.speed = 2f;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();   
        _obstaclesLayer = LayerMask.GetMask("Obstacles");
        direction = Vector2.zero;
        rayLength = 0.05F;
        futureDirection = Vector2.zero;
        canTeleport = true;
    }
    private RaycastHit2D boxCast(Vector2 direction) {
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position;

        return Physics2D.BoxCast(origin, size, 0f, direction, rayLength, _obstaclesLayer);
    }
    private bool isColliding(Vector2 direction) {
        RaycastHit2D hit = boxCast(direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacles")) return true;
        return false;
    }
    void OnDrawGizmos() {
        if (_rigidbody == null || _spriteRenderer == null) return;
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position;
        Vector2 endPoint = origin + (direction * rayLength);

        RaycastHit2D hit = boxCast(direction);
        if (isColliding(direction))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(origin + direction * hit.distance, size);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(endPoint, size);
        }
    }    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Pipeline"))  {
            if(canTeleport) {
                float mirrorOffset = (transform.position.x > 0) ? -rayLength : rayLength;
                transform.position = new Vector3(-transform.position.x-mirrorOffset, transform.position.y, transform.position.z);
                canTeleport = false;    
            }
        } else {
            canTeleport = true;
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            futureDirection = Vector2.down; 
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            futureDirection = Vector2.left; 
            
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            futureDirection = Vector2.right; 
            
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            futureDirection = Vector2.up; 
        }  
        if (!isColliding(futureDirection)) direction = futureDirection;

        if (direction == Vector2.down){
            animator.SetBool("DownMunch", true);
            animator.SetBool("UpMunch", false);
            animator.SetBool("RightMunch", false);
            animator.SetBool("LeftMunch", false);
        }
        if (direction == Vector2.up){
            animator.SetBool("UpMunch", true);
            animator.SetBool("DownMunch", false);
            animator.SetBool("LeftMunch", false);
            animator.SetBool("RightMunch", false);
        }
        if (direction == Vector2.left){
            animator.SetBool("UpMunch", false);
            animator.SetBool("RightMunch", false);
            animator.SetBool("DownMunch", false);
            animator.SetBool("LeftMunch", true);
        }
        if (direction == Vector2.right){
            animator.SetBool("RightMunch", true);
            animator.SetBool("DownMunch", false);
            animator.SetBool("UpMunch", false);
            animator.SetBool("LeftMunch", false);  
        }        

        if (!isColliding(direction)) transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
