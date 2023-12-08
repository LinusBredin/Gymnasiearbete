using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;

public class EnemyBirdAI : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float knockbackValue = 1000f;

    public int tracker;

    public Transform enemyGFX;

    Path path;
    bool tracked;
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
        switch (tracker)
        {
            case 2:
                tracked = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().tracked2;
                break;
            case 3:
                tracked = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().tracked3;
                break;
            default:
                break;
        }
        if (path == null)
        {
            return;
        }

        if ( tracked==true)
        {
            if (direction.x > 0.2)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        


        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        if (tracked == true)
        {
        rb.AddForce(force);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >=  0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    public void DealtContactDamage()
    {
        Vector2 knockback = direction * -1 * knockbackValue;
        rb.AddForce(knockback);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tracker2")
        {
            tracker = 2;
        }
        if (other.tag == "Tracker3")
        {
            tracker = 3;
        }
    }
}
