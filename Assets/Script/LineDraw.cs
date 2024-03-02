using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{

    [SerializeField]LineRenderer lineRenderer; 
    public Transform point_one; 
    public Transform point_two; 
    // Start is called before the first frame update

    public void HideLine(){
        gameObject.SetActive(false);
    }

    public void SetLine(int index, Vector2 position){
        switch(index){
            case 0:
            point_one.position = position;
            point_one.position += Vector3.forward * 50;
            break;
            case 1:
            point_two.position = position;
            point_two.position += Vector3.forward * 50;
                if(!gameObject.activeSelf){
                gameObject.SetActive(true);
            }
            break;
        }
        lineRenderer.SetPosition(0,point_one.position);
        lineRenderer.SetPosition(1,point_two.position);
        
    }
    // Update is called once per frame
    // void Update()
    // {
    //     lineRenderer.SetPosition(0,point_one.position);
    //     lineRenderer.SetPosition(1,point_two.position);
    // }
}
