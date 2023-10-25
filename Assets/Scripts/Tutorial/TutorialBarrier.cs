using UnityEngine;

public class TutorialBarrier : MonoBehaviour
{
    public void MoveBarrier(float distance)
    {
        var pos = transform.position;
        pos.x += distance;
        pos.z += distance;
        transform.position = pos;
    }
}