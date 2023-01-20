using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoomTransparency
{
    public class RoomHiding : MonoBehaviour
    {
        private List<Room> rooms = new List<Room>();
        private Room currentRoom;

        private void Start()
        {
            rooms = FindObjectsOfType<Room>().ToList();
            foreach (var room in rooms)
            {
                room.onRoomEnter += onEnterRoom;
            }
        }

        public void onEnterRoom(Room room)
        {
            Debug.Log("Entering Room");
            if (currentRoom != null)
            {
                currentRoom.showRoom(false);
            }

            room.showRoom(true);
            currentRoom = room;
        }
    }
}