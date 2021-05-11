using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class HappyGlassMeshScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    // New gameobject for our trail

    GameObject TrailObj;

    LineRenderer LR;

    bool StartDrawing;
    bool DrawStartFirst;

    // To check mouse Position

    //float MousePosX;
    //float MousePosY;

    Vector3 MousePos;

    int CurrentLineIndex=0;

    // Camera

    [SerializeField]
    Camera cam;

    [SerializeField]
    Material LineMat;

    [SerializeField]
    Transform LineCollider_Prefab;

    Transform LastInstantiatedColl;

    Vector3 LineStartPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartDrawing = true;
        DrawStartFirst = true;
        MousePos = Input.mousePosition;
        //MousePosY = Input.mousePosition.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartDrawing = false;
        if (LastInstantiatedColl != null)
        {
            Destroy(LastInstantiatedColl.gameObject);
        }

        LastInstantiatedColl = null;

        GameObject T = new GameObject();

        TrailObj.transform.SetParent(T.transform);
        Rigidbody rb =T.AddComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX;

        //TrailObj.AddComponent<Rigidbody>();
        LR.useWorldSpace = false;

        for (int i = 0; i < LR.positionCount; i++)
        {
            LR.SetPosition(i, LR.GetPosition(i) - LineStartPos);
        }

            

        Start();
    }


    void Start()
    {
        CurrentLineIndex = 0;

        TrailObj = new GameObject();

        //LR = TrailObj.AddComponent<LineRenderer>();

        //LR.startWidth = 0.2f;
        //LR.material = LineMat;
        //LR.useWorldSpace = false;
    }


    void Update()
    {
        if (StartDrawing)
        {
            if(DrawStartFirst)
            {
                LineStartPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f));

                TrailObj.transform.position = LineStartPos;

                LR = TrailObj.AddComponent<LineRenderer>();

                LR.startWidth = 0.2f;
                LR.material = LineMat;

                DrawStartFirst = false;

            }

            Vector3 offset = MousePos - Input.mousePosition;
            float Distance = offset.sqrMagnitude;


            //if (Input.mousePosition.x != MousePosX || Input.mousePosition.y != MousePosY)
            if(Distance > 500f)
            {
                
                LR.SetPosition(CurrentLineIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f)));

                if(LastInstantiatedColl != null)
                {
                    Vector3 CurLinePos = LR.GetPosition(CurrentLineIndex);

                    LastInstantiatedColl.LookAt(CurLinePos);

                    if(LastInstantiatedColl.rotation.y == 0  )
                    {
                        Debug.Log(LastInstantiatedColl);
                    }

                    LastInstantiatedColl.localScale = new Vector3(LastInstantiatedColl.localScale.x, LastInstantiatedColl.localScale.y, Vector3.Distance(LastInstantiatedColl.position,CurLinePos)*0.2f);
                }

                LastInstantiatedColl= Instantiate(LineCollider_Prefab, LR.GetPosition(CurrentLineIndex), Quaternion.identity, TrailObj.transform);
                
                MousePos = Input.mousePosition;
                //MousePosY = Input.mousePosition.y;
                CurrentLineIndex++;

                LR.positionCount =  CurrentLineIndex + 1;

                LR.SetPosition(CurrentLineIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f)));
            }
        }

    }
}
