using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.Model.Rooms;

namespace Controller
{
    public class ActionController
    {
        public ActionService actionService = new ActionService();
        public void CreateAction(Model.Action newAction)
        {
            actionService.CreateAction(newAction);
        }

        public void UpdateRenovationAction(RenovationActionModel action)
        {
            actionService.UpdateRenovationAction(action);
        }

        public void DeleteRenovationAction(RenovationActionModel action)
        {
            actionService.DeleteRenovationAction(action);
        }

        public void DeleteChangeAction(ChangeActionModel action)
        {
            actionService.DeleteChangeAction(action);
        }

        public void UpdateChangeAction(ChangeActionModel action, int count)
        {
            actionService.UpdateChangeAction(action, count);
        }

        public void DeleteAction(int identificator)
        {
            actionService.DeleteAction(identificator);
        }

        public Model.Action ReadAction(int identificator)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<RenovationActionModel> GetAllRenovationActions()
        {
            return actionService.GetAllRenovationActions();
        }

        public ObservableCollection<ChangeActionModel> GetAllChangeRoomActions()
        {
            return actionService.GetAllChangeRoomActions();
        }

    }
}
