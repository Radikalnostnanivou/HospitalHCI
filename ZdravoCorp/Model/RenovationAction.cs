// File:    RenovationAction.cs
// Author:  halid
// Created: Sunday, 24 April 2022 22:24:01
// Purpose: Definition of Class RenovationAction

using Repository;
using Service;
using System;
using System.Collections.Generic;

namespace Model
{
    public class RenovationAction : IAction
    {
        private DateTime expirationDate;

        private int id_room;
        private bool renovation;

        public RenovationAction()
        {
        }

        public RenovationAction(DateTime expirationDate, int id_room, bool renovation)
        {
            this.ExpirationDate = expirationDate;
            this.Id_room = id_room;
            this.Renovation = renovation;
        }

        public DateTime ExpirationDate { get => expirationDate; set => expirationDate = value; }
        public int Id_room { get => id_room; set => id_room = value; }
        public bool Renovation { get => renovation; set => renovation = value; }

        private void StartRenovation(Room room)
        {
            ActionService service = new ActionService();
            room.Renovating = true;
            room.RenovatedUntil = ExpirationDate;
            RoomService.Instance.Update(room);
            service.CreateAction(new Model.Action(ActionType.renovation, ExpirationDate,
                new RenovationAction(new DateTime(), Id_room, false)));
        }

        private void EndRenovation(Room room)
        {
            room.Renovating = false;
            RoomService.Instance.Update(room);
        }

        public void Execute()
        {
            Room room = RoomService.Instance.Read(Id_room);
            if (Renovation)
            {
                StartRenovation(room);
            }
            else
            {
                EndRenovation(room);
            }
        }

        public void FromCSV(string[] values)
        {
            this.ExpirationDate = DateTime.Parse(values[0]);
            this.Id_room = int.Parse(values[1]);
            this.Renovation =Boolean.Parse(values[2]);
        }

        public List<string> ToCSV()
        {
            List<String> result = new List<String>();
            result.Add(ExpirationDate.ToString());
            result.Add(Id_room.ToString());
            result.Add(Renovation.ToString());

            return result;
        }
    }
}