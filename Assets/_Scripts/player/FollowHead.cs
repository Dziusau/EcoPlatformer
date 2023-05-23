using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // the speed of the platform
    [SerializeField] private float offset = 1f;
    [SerializeField] private float bounds = 1f;
    
    private Transform player;
    private Vector3 target; // the current target point
    private bool m_FacingRigth;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offset, player.position.z);
        target = new Vector3(transform.position.x + bounds, transform.position.y, transform.position.z);
        m_FacingRigth = true;
    }

    private void Update()
    {
        target.y = player.position.y + offset;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); // move the platform towards the current target

        if (transform.position.x <= player.position.x - bounds) // if we've reached left bound
        {
            target = new Vector3(player.position.x + bounds, player.position.y + offset, player.position.z); // start moving towards right 
            if (!m_FacingRigth)
                Flip();
        }
        else if (transform.position.x >= player.position.x + bounds) // if we've reached right bound
        {
            target = new Vector3(player.position.x - bounds, player.position.y + offset, player.position.z); // start moving towards left
            if (m_FacingRigth)
                Flip();
        }
    }

    private void Flip()
    {
        m_FacingRigth = !m_FacingRigth;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
