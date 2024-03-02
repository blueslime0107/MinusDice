using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    public bool ZFix = true;
    private bool _isMoving;
    
    public bool isMoving(){
        return _isMoving;
    }

    public void MoveToTarget(Vector3 target, float speed,bool changeZ = false){
        StopAllCoroutines();
        StartCoroutine(MoveObjectToTarget(new Vector3(target.x,target.y,(changeZ) ? target.z : transform.position.z),speed));
    }

    IEnumerator MoveObjectToTarget(Vector3 targetPosition,float moveSpeed)
    {
        _isMoving = true;
        float curTime = 0;
        // 오브젝트를 특정 좌표까지 부드럽게 이동
        // while (transform.position != targetPosition)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //     yield return null;
        // }

        while (true){
            curTime += Time.deltaTime;
            float t = Mathf.Clamp01(curTime / moveSpeed);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, t * Vector3.Distance(transform.position, targetPosition));

            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, curTime);
            // curTime += Time.deltaTime / moveSpeed;
            if (t >= 1.0f || transform.position == targetPosition)
            {
                break;
            }
            yield return null;
        }
        _isMoving = false;
    }
}
