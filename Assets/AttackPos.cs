using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPos : MonoBehaviour
{
    public Transform attackPos;
    public Transform currentPos;
    public float attackPosSelect;
    public SpriteRenderer sprite;

    // Update is called once per frame
    void Update()
    {
        if(sprite.flipX == false)
        {
            attackPos.position = new Vector3(currentPos.position.x + attackPosSelect, currentPos.position.y, currentPos.position.z);
        }
        else
        {
            attackPos.position = new Vector3(currentPos.position.x-attackPosSelect, currentPos.position.y, currentPos.position.z);
        }
    }
}
