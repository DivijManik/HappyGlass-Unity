using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DrawLineScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject LineGO;

    bool StartDrawing;

    Vector3 MousePos;

    LineRenderer LR;

    [SerializeField]
    Material LineMat;

    int CurrentIndex;

    [SerializeField]
    Camera cam;

    [SerializeField]
    Transform Collider_Prefab;

    Transform LastInstantiated_Collider;

    // For happy glass
    [SerializeField]
    InstantiateKoolAid InstKool;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartDrawing = true;
        MousePos = Input.mousePosition;

        LR = LineGO.AddComponent<LineRenderer>();

        LR.startWidth = 0.2f;

        LR.material = LineMat;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartDrawing = false;

        Rigidbody rb = LineGO.AddComponent<Rigidbody>();


        rb.constraints = RigidbodyConstraints.FreezeRotationX;

        LR.useWorldSpace = false;
        LR.enabled = false;

        if (LastInstantiated_Collider != null)
        {
            Destroy(LastInstantiated_Collider.gameObject);
            LastInstantiated_Collider = null;
        }

        InstKool.StartKoolAiding();

        Start();

        CurrentIndex = 0;
    }

    void Start()
    {
        LineGO = new GameObject();

        
    }

    
    void FixedUpdate()
    {
        if(StartDrawing)
        {
            Vector3 Dist = MousePos - Input.mousePosition;

            float Distance_SqrMag = Dist.sqrMagnitude;

            if(Distance_SqrMag > 1200)
            {
                // Set this Position for our line

                LR.SetPosition(CurrentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + -cam.transform.position.z)));

                if(LastInstantiated_Collider != null)
                {
                    Vector3 CurLinePos = LR.GetPosition(CurrentIndex);
                    LastInstantiated_Collider.gameObject.SetActive(true);

                    LastInstantiated_Collider.LookAt(CurLinePos);

                    if (LastInstantiated_Collider.rotation.y == 0)
                    {
                        //Debug.Log(LastInstantiated_Collider);
                        LastInstantiated_Collider.eulerAngles = new Vector3(LastInstantiated_Collider.rotation.eulerAngles.x, 90, LastInstantiated_Collider.rotation.eulerAngles.z);
                    }

                    LastInstantiated_Collider.localScale = new Vector3(LastInstantiated_Collider.localScale.x,LastInstantiated_Collider.localScale.y, Vector3.Distance(LastInstantiated_Collider.position,CurLinePos) *0.5f );
                }

                LastInstantiated_Collider = Instantiate(Collider_Prefab, LR.GetPosition(CurrentIndex), LineGO.transform.rotation, LineGO.transform);

                LastInstantiated_Collider.gameObject.SetActive(false);

                MousePos = Input.mousePosition;

                CurrentIndex++;

                LR.positionCount = CurrentIndex + 1;

                LR.SetPosition(CurrentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 18f)));
            }
        }
    }
}
