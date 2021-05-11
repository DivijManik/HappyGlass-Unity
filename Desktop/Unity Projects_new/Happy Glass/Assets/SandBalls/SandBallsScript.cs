using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.ProBuilder;

using Parabox.CSG;

public class SandBallsScript : MonoBehaviour
{
    ProBuilderMesh Pb_Mesh;

    [SerializeField]
    GameObject Cube_;

    [SerializeField]
    GameObject Sphere_;

    GameObject composite;

    Vector3 SpherePos;

    void Start()
    {
        Pb_Mesh = GetComponent<ProBuilderMesh>();

        //Pb_Mesh.faces[0] = new Face();

        CSG_Model result = Boolean.Subtract(Cube_, Sphere_);

        //for (int i = 0; i < Pb_Mesh.faceCount; i++)
        //{
        //   // Pb_Mesh.faces[i].
        SpherePos = Sphere_.transform.position;
        //}

        composite = new GameObject();
        composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        Destroy(Cube_.gameObject);
        Cube_ = composite;

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CSG_Model result = Boolean.Subtract(Cube_, Sphere_);

            composite = new GameObject();
            composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
            composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
            Destroy(Cube_.gameObject);
            Cube_ = composite;
        }
    }
}
