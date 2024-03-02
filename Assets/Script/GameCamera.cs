using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Camera gameCamera;
    public Vector3 camPosition;
    public float camMove_speed;
    public float camScale;
    public float camScale_speed;

    public float camScale_Max;
    public float camScale_Min;


    public List<GameObject> objectsToInclude = new List<GameObject>();

    private void Awake() {
        gameCamera = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameCamera.orthographicSize != camScale){
            gameCamera.orthographicSize += (camScale - gameCamera.orthographicSize) / camScale_speed * Time.deltaTime;
        }
        if(gameCamera.transform.position != camPosition){
            gameCamera.transform.position = Vector3.MoveTowards(gameCamera.transform.position, Vector3.Lerp(gameCamera.transform.position, camPosition, 0.05f),camMove_speed * Time.deltaTime);
        }

        if(objectsToInclude.Count > 0){
             Vector3 center = Vector3.zero;
             float avDist = 0;

            // 모든 오브젝트의 위치를 합산합니다.
            foreach (var obj in objectsToInclude)
            {
                center += obj.transform.position;

            }
            center /= objectsToInclude.Count;
            foreach (var obj in objectsToInclude)
            {
                avDist += Vector2.Distance(center,obj.transform.position);

            }
            avDist /= objectsToInclude.Count;

            camPosition = new Vector3(center.x,center.y,-10);
            camScale = avDist * 0.5f + 1.5f;
        }
        else{
            camPosition = Vector3.back * 10;
            camScale = 5;
        }
        // AdjustCameraToFitObjects();
    }

    // void AdjustCameraToFitObjects()
    // {
    //     if (gameCamera == null || objectsToInclude.Count == 0)
    //     {
    //         Debug.LogWarning("카메라 또는 포함시킬 오브젝트가 할당되지 않았습니다.");
    //         return;
    //     }

    //     Bounds bounds = CalculateBounds(objectsToInclude);

    //     float distance = CalculateCameraDistance(bounds);

    //     // 카메라 위치와 시야 조정
    //     gameCamera.transform.position = bounds.center - gameCamera.transform.forward * distance;
    //     gameCamera.orthographicSize = Mathf.Max(bounds.extents.x, bounds.extents.y);
    // }

    //  Bounds CalculateBounds(List<GameObject> objects)
    // {
    //     Bounds bounds = new Bounds(objects[0].transform.position, Vector3.zero);

    //     foreach (GameObject obj in objects)
    //     {
    //         bounds.Encapsulate(obj.GetComponent<Renderer>().bounds);
    //     }

    //     return bounds;
    // }

    // float CalculateCameraDistance(Bounds bounds)
    // {
    //     float cameraFieldOfView = gameCamera.fieldOfView;
    //     float cameraHeight = 2f * Mathf.Tan(0.5f * cameraFieldOfView * Mathf.Deg2Rad);
    //     float cameraDistance = bounds.size.y / (2f * Mathf.Tan(0.5f * cameraFieldOfView * Mathf.Deg2Rad));

    //     return cameraDistance;
    // }


}
