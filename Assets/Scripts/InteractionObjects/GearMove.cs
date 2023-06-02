using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMove : MonoBehaviour
{
    public float Speed { get; set; }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, Speed * Time.deltaTime);
        }
    }
}
