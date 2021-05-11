using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateKoolAid : MonoBehaviour
{

    [SerializeField]
    Transform KoolAidPrefab;

    bool aiding;

    int KoolAidAmount;

    public void StartKoolAiding()
    {
        if (aiding == false)
        {
            StartCoroutine(InstKool());
            aiding = true;
        }
    }

    IEnumerator InstKool()
    {
        yield return new WaitForSeconds(0.4f);


        Transform t = Instantiate(KoolAidPrefab, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Quaternion.identity);

        t.GetComponent<Rigidbody>().velocity = new Vector3(2, 0, 0);

        KoolAidAmount++;
        if (KoolAidAmount <= 50)
        {

            StartCoroutine(InstKool());

        }
    }

}
