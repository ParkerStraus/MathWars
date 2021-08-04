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
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
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
        {
            foreach (Room room in Rooms)
            {
                room.GetRoomObject().SetActive(false);
            }
            CurrentRoom.GetPrevRoom().SetActive(true);
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
}
