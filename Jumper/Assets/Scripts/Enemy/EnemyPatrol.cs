using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private EnemyHealth Health;

    public Transform ObsticalDetector;

    [Range(0, .5f)] public float checkWallDistance = 0.5f;
    public bool checkWall = true;

    [Range(0, .5f)] public float checkFloorDistance = 0.5f;
    public bool checkPlatform = true;

    public float runSpeed = 1.5f;
    private bool isGoingRight = false;

    Animator anim;

    private void Start()
    {
        Health = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

        if (transform.localScale.x < 0)
            isGoingRight = true;
        else  
            isGoingRight = false;

        
    }

    private void Update()
    {
        if (!isObsticleDetected() && Health.getHealth() != 0)
        {
            transform.Translate(getforward() * runSpeed * Time.deltaTime);
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

    }

    private bool isObsticleDetected()
    {
        RaycastHit2D hit_floor = Physics2D.Raycast(ObsticalDetector.position, Vector2.down, checkFloorDistance);
        RaycastHit2D hit_wall = Physics2D.Raycast(ObsticalDetector.position, getforward(), checkWallDistance);

        if (hit_wall.collider != null && checkWall)
        {
            string tag = hit_wall.collider.tag;
            switch (tag)
            {
                case "Door":
                    return false;
                
                case "Item":
                    return false;
                case "Player":
                    break;

                default:
                    Debug.Log($"Something else was hit -> Tag: {tag}");
                    Flip();
                    break;
            }

            return true;
        }

        if (hit_floor.collider == null && checkPlatform)
        { 
            Flip();
            return true;
        }
        
        

        return false;
    }


    private Vector2 getforward()
    {
        if (isGoingRight)
        {
            return Vector2.right;
        }
        else        
        {
            return Vector2.left;
        }
    }

    private void Flip()
    {
        // Switch the way the Enemy is labelled as facing.
        isGoingRight = !isGoingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //also Swap around healthbar
        Vector3 healthBarScale = Health.Healthbar.transform.localScale;
        healthBarScale.x *= -1;
        Health.Healthbar.transform.localScale = healthBarScale;
    }
}
