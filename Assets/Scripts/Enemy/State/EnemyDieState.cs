using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDieState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;

    public override void OnEnter()
    {
        base.OnEnter();

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        // 만약 이미 죽고 있다면 (죽는 애니메이션 실행 중이라면) return
        if (_enemyController._anim.GetCurrentAnimatorStateInfo(0).IsName("Die")) return;

        _enemyController.GetComponent<CapsuleCollider>().enabled = false;   

        _enemyController._isDie = true;

        _enemyController._anim.SetBool("DieTrue",true);
        _enemyController._anim.SetTrigger("Hit");

        // enemy trail 비활성화
        if (_enemyController.TryGetComponent<EnemyController_Melee>(out EnemyController_Melee enemy))    
        {
            enemy._attackTrail.gameObject.SetActive(false);
        }
         
         _enemyController._enemyUI.DeactivateUI();   // HP UI 삭제
        _enemyController._enemyUI.ToggleExclamationMark(false);
    }

}
