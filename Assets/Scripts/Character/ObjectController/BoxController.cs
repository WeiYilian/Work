using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Animator animator;
    
    private bool isOpen;

    private GameObject boxTop;
    private ParticleSystem boxOpen;
    
    public BagItem thisItem;

    private Vector3 target;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxTop = transform.GetChild(3).gameObject;
        boxOpen = transform.GetChild(4).GetComponent<ParticleSystem>();
        target = new Vector3(-150, 0, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            isOpen = true;
            animator.SetTrigger("Open");
            boxOpen.Play();
            AudioManager.Instance.PlayAudio(4,"OpenBox");
            boxTop.transform.localRotation = Quaternion.Euler(-240,0,0);
            InventoryManager.Instance.AddNewItem(thisItem);
            TaskManager.Instance.Tasks[0].TaskCompletion++;
        }
    }
}
