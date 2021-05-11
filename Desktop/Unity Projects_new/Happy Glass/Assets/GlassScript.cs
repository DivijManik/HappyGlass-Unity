using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GlassScript : MonoBehaviour
{
    List<Transform> KoolAidList = new List<Transform>();

    [SerializeField]
    Text LevelCompText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "KoolAid" && !KoolAidList.Contains(other.transform))
        {
            KoolAidList.Add(other.transform);
        }

        if (KoolAidList.Count > 15)
        {
            LevelCompText.gameObject.SetActive(true);
        }
    }

  
}
