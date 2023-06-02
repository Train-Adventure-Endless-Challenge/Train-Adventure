using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMove : MonoBehaviour
{
    public float Speed { get; set; }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("CollisionStay");
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.transform.position = Vector3.MoveTowards(transform.position, other.transform.position, Speed * Time.deltaTime);
        }
    }
}
