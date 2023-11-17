using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChargerAI : MonoBehaviour
{

    public Transform target;
    public Transform position;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpForce = 100f;

    public float enemyChargerMaxHealth;
    float enemyChargerCurrentHealth;

    bool grounded = true;

    public Transform enemyChargerGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Vector2 direction;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        position = GetComponent<Transform>();

        AstarPath.active.logPathResults = PathLog.None;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (direction.x > 0f)
        {
            enemyChargerGFX.localScale = new Vector3(1f, 1f, 1f);
            rb.transform.Translate(new Vector2(speed * Time.deltaTime, 0f));

        }
        else if(direction.x < 0f) 
        {
            enemyChargerGFX.localScale = new Vector3(-1f, 1f, 1f);
            rb.transform.Translate(new Vector2(-speed*Time.deltaTime, 0f));
        }

        /*if(direction.y > 0f && grounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        } */
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            grounded = false;
        }
    }
}
