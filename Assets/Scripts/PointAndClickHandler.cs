﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public GameObject RoomObject;
    public GameObject PreviousRoom;

    public Room(GameObject RoomObject, GameObject PreviousRoom)
    {
        this.RoomObject = RoomObject;
        this.PreviousRoom = PreviousRoom;
    }

    public GameObject GetRoomObject()
    {
        return RoomObject;
    }

    public GameObject GetPrevRoom()
    {
        return PreviousRoom;
    }
}

public class PointAndClickHandler : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Room[] Rooms;
    [SerializeField] private Room CurrentRoom;
    public PointAndClickObject[] RoomObjects;
    public List<PointAndClickInventoryItem> Items;
    public GameObject UIObject;
    public Animator UIAnimator;
    public GameObject[] ItemBoxs;
    public PointAndClickInventoryItem HeldItem;
    public bool UIEnabled;
    [SerializeField] private GameObject GameCompleteUI;
    [SerializeField] private bool GameDone =false;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        UIObject = GameObject.Find("General HUD");
        UIAnimator = UIObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIEnabled && GameDone == false)
        {
            if (Input.GetMouseButtonDown(0))
            { // if left button pressed...
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit)
                {
                    Debug.Log(hit.collider.gameObject.name + " was touched by an angle");
                    hit.collider.gameObject.GetComponent<Clickable>().TriggerEvent();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {// if right button pressed...
                foreach (Room room in Rooms)
                {
                    room.GetRoomObject().SetActive(false);
                }
                CurrentRoom.GetPrevRoom().SetActive(true);
            }
        }
    }

    public void SwapRoom(int roomNum)
    {
        foreach(Room room in Rooms)
        {
            room.GetRoomObject().SetActive(false);
        }
        Rooms[roomNum].GetRoomObject().SetActive(true);
        CurrentRoom = Rooms[roomNum];
    }

    public void SetRoomObject(int i)
    {
        RoomObjects[i].SwapActiveSetting();
    }

    public void ToggleUISetting()
    {
        UIEnabled = !UIEnabled;
        UIAnimator.SetBool(Animator.StringToHash("UI Toggle"), UIEnabled);
    }

    public void AddItem(PointAndClickInventoryItem item)
    {
        Items.Add(item);
        UpdateItems();
    }
    public void RemoveItem(PointAndClickInventoryItem item)
    {
        Items.Remove(item);
        UpdateItems();
    }

    public void UpdateItems() 
    {
        int i = 0;
        try {
            foreach (GameObject itembox in ItemBoxs)
            {
                itembox.GetComponent<ItemBox>().UpdateItem(Items.ToArray()[i]);
                i++;

            }
        }
        catch(IndexOutOfRangeException exception)
        {

        }
    }

    public void HoldItem(int index)
    {
        Debug.Log(Items.ToArray()[index]);
        ToggleUISetting();
        HeldItem = Items.ToArray()[index];
    }

    public void LetGoHeldItem()
    {
        HeldItem = null;
    }

    public void CompleteLevel()
    {
        Debug.Log("You fiddled the Riddle!");
        GameCompleteUI.SetActive(true);
        GameDone = true;
    }
}
