using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator door;

    private void Start()
    {
        door = GetComponent<Animator>();
    }

    public void Open()
    {
        door.SetTrigger("Open");
    }
}
