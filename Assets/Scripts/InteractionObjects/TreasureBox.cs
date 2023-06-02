using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : InteractionObject
{

    private int _GearCount = 5;

    [ContextMenu("OPEN")]
    public override void Interact()
    {
        GetComponent<Collider>().isTrigger = true;
        //TODO: 애니메이션 실행
        //TODO: 아이템 소환
        StartCoroutine(BoxOpenCor());
        
    }

    IEnumerator BoxOpenCor()
    {

        while(_GearCount > 0)
        {
            int gearCount = 1;

            Gear gear = (Instantiate(ItemDataManager.Instance.GearPrefab, transform.position, Quaternion.identity) as GameObject).GetComponent<Gear>();
            gear.AcquisitionGear = gearCount;
            _GearCount--;
            //TODO: 아이템 나오게하기
            gear.GetComponent<Rigidbody>().AddForce(Random.Range(-100f, 100f), Random.Range(200f, 400f), Random.Range(-100f, 100f));

            yield return new WaitForSeconds(0.6f);
        }

        Destroy(gameObject);
    }
}
