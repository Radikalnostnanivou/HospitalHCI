
using Model;
using System;
using Service;
using System.Collections.Generic;
using Repository;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class GuestController : ICrud<Guest>
    {
        public IGuestService service = GuestService.Instance;
        public void Create(Guest newGuest)
        {
            service.Create(newGuest);
        }

        public Guest Read(int id)
        {
            return service.Read(id);
        }

        public void Update(Guest guest)
        {
            service.Update(guest);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }

        public List<Guest> GetAll()
        {
            return service.GetAll();
        }

    }
}