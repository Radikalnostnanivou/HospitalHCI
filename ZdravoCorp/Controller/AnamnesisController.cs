using Model;
using System;
using System.Collections.Generic;
using Service;
using Repository;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class AnamnesisController : ICrud<Anamnesis>
    {

        private IAnamnesisService service = AnamnesisService.Instance;
        public void Create(Anamnesis newAnamnesis)
        {
            service.Create(newAnamnesis);
        }

        public Anamnesis Read(int id)
        {
            return service.Read(id);
        }

        public void Update(Anamnesis anamnesis)
        {
            service.Update(anamnesis);

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Anamnesis> GetAll()
        {
            return service.GetAll();
        }

        public Anamnesis FindAnamnesisByAppointmentId(int id)
        {
            return service.FindAnamnesisByAppointmentId(id);
        }
        public AnamnesisController()
        {

        }
    }
}