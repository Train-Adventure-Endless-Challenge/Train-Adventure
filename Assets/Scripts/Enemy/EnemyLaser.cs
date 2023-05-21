using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    [SerializeField] private Animator _laserAnim;
    [SerializeField] private Animator _laserWarningLineAnim;


    private void OnEnable()
    {
        _laserAnim.SetTrigger("Laser");
        _laserWarningLineAnim.SetTrigger("Laser");

    }
}
