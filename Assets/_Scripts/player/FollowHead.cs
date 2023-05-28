using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // the speed of the platform
    [SerializeField] private float offset = 1f;
    [SerializeField] private float bounds = 1f;
    
    private Vector3 target; // the current target point
    private bool m_FacingRigth;

    private void Start()
    {
        transform.position = new Vector3(x: GameManager.player.transform.position.x, 
                                         y: GameManager.player.transform.position.y + offset,   
                                         z: GameManager.player.transform.position.z);
        target = new Vector3(transform.position.x + bounds, transform.position.y, transform.position.z);
        m_FacingRigth = true;
    }

    private void Update()
    {
        if (GameManager.player == null)
            return;

        target.y = GameManager.player.transform.position.y + offset;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); // move the platform towards the current target

        if (transform.position.x <= GameManager.player.transform.position.x - bounds) // if we've reached left bound
        {
            target = new Vector3(x: GameManager.player.transform.position.x + bounds, 
                                 y: GameManager.player.transform.position.y + offset, 
                                 z: GameManager.player.transform.position.z); // start moving towards right 
            if (!m_FacingRigth)
                Flip();
        }
        else if (transform.position.x >= GameManager.player.transform.position.x + bounds) // if we've reached right bound
        {
            target = new Vector3(x: GameManager.player.transform.position.x - bounds, 
                                 y: GameManager.player.transform.position.y + offset, 
                                 z:  GameManager.player.transform.position.z); // start moving towards left
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
