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
    
    public Inventory PlayerInventory;

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
            boxTop.transform.localRotation = Quaternion.Euler(-240,0,0);
            AddNewItem();
            TaskManager.Instance.Tasks[0].TaskCompletion++;
        }
    }

    private void AddNewItem()
    {
        if (!PlayerInventory.itemList.Contains(thisItem))
        {
            if (PlayerInventory.itemList.Count > PlayerInventory.MaxItemListCount) return;
            for (int i = 0; i < PlayerInventory.itemList.Count; i++)
            {
                if (!PlayerInventory.itemList[i])
                {
                    PlayerInventory.itemList[i] = thisItem;
                    return;
                }
            }
            PlayerInventory.itemList.Add(thisItem);
        }
        else
        {
            thisItem.itemHeld += 1;
        }
    }
}
