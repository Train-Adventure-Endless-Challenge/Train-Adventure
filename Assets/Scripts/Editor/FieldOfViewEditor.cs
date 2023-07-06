using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFieldOfView fov = (EnemyFieldOfView)target;      // 캐스팅

        //radius draw
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov._radius);     

        //angle draw
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov._angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov._angle / 2);
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov._radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov._radius);

        if(fov._isVisiblePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov._playerRef.transform.position);
        }
    }

    /// <summary>
    /// 축에서 시작되어 정해진 각도까지의 vector를 반환
    /// </summary>
    /// <param name="eulerY">y 축 각도</param>
    /// <param name="angleInDegrees">각도</param>
    /// <returns></returns>
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)); 
    }
}
