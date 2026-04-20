using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.Repository.Interfaces;

namespace Service
{
    public class GuestService : ICrud<Guest> , IGuestService
    {

        private static GuestService instance = null;

        private GuestRepository repository = GuestRepository.Instance;
        List<Guest> guests = new List<Guest>();

        public void Create(Guest newGuest)
        {
            repository.CreateGuest(newGuest);
        }

        public Guest Read(int id)
        {
            return repository.ReadGuest(id);
        }

        public void Update(Guest guest)
        {
            repository.UpdateGuest(guest);
        }

        public void Delete(int id)
        {
            repository.DeleteGuest(id);
        }

        public List<Guest> GetAll()
        {
            return repository.GetAllGuests();
        }

        public GuestService()
        {

        }

        public static GuestService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GuestService();
                }
                return instance;
            }
        }
       


    }
}