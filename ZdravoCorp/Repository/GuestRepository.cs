
using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class GuestRepository
    {
        private String dbPath = "..\\..\\Data\\guestsDB.csv";
        private Serializer<Guest> serializerGuest = new Serializer<Guest>();

        private static GuestRepository instance = null;
        public List<int> GetAllGuestIds()
        {
            List<Guest> guests = GetAllGuests();
            List<int> ids = new List<int>();
            foreach (Guest guest in guests)
            {
                ids.Add(guest.Id);
            }
            return ids;
        }
        public void GenerateId(Guest newGuest)
        {
            List<int> allGuestsIds = GetAllGuestIds();
            Random random = new Random();
            do
            {
                newGuest.Id = random.Next();
            }
            while (allGuestsIds.Contains(newGuest.Id));
        }
        public Boolean CreateGuest(Guest newGuest)
        {
            List<Guest> guests = GetAllGuests();
            GenerateId(newGuest);
            guests.Add(newGuest);
            serializerGuest.ToCSV(dbPath, guests);
            return true;
        }

        public Guest ReadGuest(int id)
        {
            List<Guest> guests = GetAllGuests();
            Guest guest = null;
            foreach (Guest g in guests)
            {
                if (id == g.Id)
                {
                    guest = g;
                }
            }
            return guest;
        }


        public Boolean UpdateGuest(Guest guest)
        {
            Boolean success = false;
            List<Guest> guests = GetAllGuests();
            for (int i = 0; i < guests.Count; i++)
            {
                if (guest.Id == guests[i].Id)
                {
                    guests[i] = guest;
                    serializerGuest.ToCSV(dbPath, guests);
                    success = true;
                    break;
                }
            }
            return success;

        }

        public Boolean DeleteGuest(int id)
        {
            Boolean success = false;
            List<Guest> guests = GetAllGuests();
            foreach (Guest guest in guests)
            {
                if (guest.Id == id)
                {
                    guests.Remove(guest);
                    serializerGuest.ToCSV(dbPath, guests);
                    success = true;
                    break;
                }
            }
            return success;
        }

        public List<Guest> GetAllGuests()
        {
            List<Guest> guests = serializerGuest.FromCSV(dbPath);
            return guests;

        }

        public GuestRepository()
        {

        }

        public static GuestRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GuestRepository();
                }
                return instance;
            }
        }
    }
}