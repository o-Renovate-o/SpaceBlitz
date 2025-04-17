using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    [SerializeField] GameObject targetsContainer;

    void OnDestroy()
    {
        if (targetsContainer != null)
        ActivateGameObjects();
    }

    private void ActivateGameObjects()
    {
        for (int i = 0; i < targetsContainer.transform.childCount; i++)
        {
            targetsContainer.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
