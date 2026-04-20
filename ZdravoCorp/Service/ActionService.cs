// File:    ActionService.cs
// Author:  halid
// Created: Sunday, 24 April 2022 22:50:44
// Purpose: Definition of Class ActionService

using Repository;
using System;
using System.Collections.Generic;
using Model;
using ZdravoCorp.Utility;
using System.Threading;
using System.Collections.ObjectModel;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.Model.Rooms;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;
using ZdravoCorp.Service.Interfaces;

namespace Service
{

    public class ActionService
    {
        protected static readonly object key = new object();
        private static ActionService instance = null;
        public IActionRepository repository = ActionRepository.Instance;
        //public IEquipmentService equipmentService = EquipmentService.Instance;
        public void CheckActions(Object stateInfo)
        {
            Console.WriteLine("{0} Timer activated", DateTime.Now.ToString("h:mm:ss.fff"));
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            List<Model.Action> actions = GetAllActions();
            List<Model.Action> removed = new List<Model.Action>();
            DateTime now = DateTime.Now;
            for (int i = 0; i < actions.Count; i++)
            {
                try
                {
                    if (DateManipulator.checkIfLaterDate(now, actions[i].ExecutionDate))
                    {
                        removed.Add(actions[i]);
                        actions[i].Object.Execute();
                    }
                    else
                    {
                        if (removed.Count != 0)
                            RemoveExecutedActions(removed);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    RemoveExecutedActions(removed);
                    Console.WriteLine(ex.Message);
                    autoEvent.Set();
                    return;
                }
            }
            if (removed.Count != 0)
                RemoveExecutedActions(removed);
        }
        public void CreateAction(Model.Action newAction)
        {
            repository.Create(newAction);
        }

        public void UpdateAction(Model.Action action)
        {
            repository.Update(action);
        }

        public void UpdateRenovationAction(RenovationActionModel action)
        {
            Model.Action temp = ReadAction(action.Id);
            temp.ExecutionDate = action.ExecutionDate;
            RenovationAction renovationAction = new RenovationAction(action.ExpirationDate, action.Id_room, action.Renovation);
            temp.Object = renovationAction;
            repository.Update(temp);
        }

        public void UpdateChangeAction(ChangeActionModel action, int count)
        {
            Model.Action changedAction = ReadAction(action.Id);
            changedAction.ExecutionDate = action.ExecutionDate;
            ChangeRoomAction renovationAction = new ChangeRoomAction(action.Id_incoming_room, action.Id_outgoing_room, action.Id_equipment, action.Count);
            changedAction.Object = renovationAction;
            Room room = RoomService.Instance.Read(action.Id_outgoing_room);
            RevertActualCountWhenUpdating(room, action, count);
            repository.Update(changedAction);
            RoomService.Instance.Update(room);
        }

        public void DeleteRenovationAction(RenovationActionModel action)
        {
            DeleteAction(action.Id);
            Room room = RoomService.Instance.Read(action.Id_room);
            room.Renovating = false;
            RoomService.Instance.Update(room);
        }

        public void DeleteChangeAction(ChangeActionModel action)
        {
            DeleteAction(action.Id);
            Room room = RoomService.Instance.Read(action.Id_outgoing_room);
            RevertActualCount(room, action);
            RoomService.Instance.Update(room);
        }

        public void DeleteAction(int id)
        {
            repository.Delete(id);
        }

        public Model.Action ReadAction(int id)
        {
            return repository.Read(id);
        }

        public List<Model.Action> GetAllActions()
        {
            return repository.GetAll();
        }

        public ObservableCollection<RenovationActionModel> GetAllRenovationActions()
        {
            List<Model.Action> actions = GetAllActions();
            ObservableCollection<RenovationActionModel> result = new ObservableCollection<RenovationActionModel>();
            RenovationAction renovation;
            foreach (Model.Action action in actions)
            {
                if(action.Type == ActionType.renovation)
                {
                    renovation = (RenovationAction)action.Object;
                    result.Add(new RenovationActionModel(action.Id, action.ExecutionDate, renovation.ExpirationDate, 
                        RoomService.Instance.Read(renovation.Id_room).DesignationCode,renovation.Id_room, renovation.Renovation));
                }
            }
            return result;
        }

        public ObservableCollection<ChangeActionModel> GetAllChangeRoomActions()
        {
            List<Model.Action> actions = GetAllActions();
            ObservableCollection<ChangeActionModel> result = new ObservableCollection<ChangeActionModel>();
            ChangeRoomAction changeRoomAction;
            foreach(Model.Action action in actions)
            {
                if(action.Type == ActionType.changePosition)
                {
                    changeRoomAction = (ChangeRoomAction) action.Object;
                    result.Add(new ChangeActionModel(action.Id, action.ExecutionDate, changeRoomAction.Id_incoming_room, 
                        changeRoomAction.Id_outgoing_room, changeRoomAction.Id_equipment, changeRoomAction.Count,
                        RoomService.Instance.Read(changeRoomAction.Id_incoming_room).DesignationCode, 
                        RoomService.Instance.Read(changeRoomAction.Id_outgoing_room).DesignationCode, 
                        EquipmentService.Instance.ReadEquipmentType(changeRoomAction.Id_equipment).Name));
                }
            }

            return result;
        }

        public void SaveActions(List<Model.Action> actions)
        {
            repository.SaveActions(actions);
        }

        private void RevertActualCount(Room room, ChangeActionModel action)
        {
            for (int i = 0; i < room.Equipment.Count; i++)
            {
                if (room.Equipment[i].Identifier == action.Id_equipment)
                {
                    room.Equipment[i].Actual_count += action.Count;
                    return;
                }
            }
            throw new LocalisedException("EquipmentDoesntExists");
        }

        private void RevertActualCountWhenUpdating(Room room, ChangeActionModel action, int count)
        {
            for (int i = 0; i < room.Equipment.Count; i++)
            {
                if (room.Equipment[i].Identifier == action.Id_equipment)
                {
                    room.Equipment[i].Actual_count += count;
                    return;
                }
            }
            throw new LocalisedException("EquipmentDoesntExists");
        }

        private void RemoveExecutedActions(List<Model.Action> removed)
        {
            List<Model.Action> actions = GetAllActions();
            foreach (Model.Action it in removed)
            {
                foreach(Model.Action action in actions)
                {
                    if(it.Id == action.Id)
                    {
                        actions.Remove(action);
                        break;
                    }
                }
            }
            SaveActions(actions);
        }

        public static ActionService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new ActionService();
                        }
                    }
                }
                return instance;
            }
        }
    }
}