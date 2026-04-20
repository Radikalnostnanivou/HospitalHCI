using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Controller;
using Repository;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.Repository.Interfaces;

namespace Service
{
    public class VacationService : ICrud<Vacation> , IVacationService
    {
        private static VacationService instance = null;

        private IVacationRepository repository = VacationRepository.Instance;

        public List<Vacation> GetAll()
        {
            return repository.GetAll();
        }

        public void Create(Vacation newVacation)
        {
            repository.Create(newVacation);
            DoctorController doctorController = new DoctorController();
            Doctor doctor = doctorController.Read(newVacation.DoctorID);
            doctor.AddVacation(newVacation);
            doctorController.Update(doctor);
        }

        public Vacation Read(int id)
        {
            return repository.Read(id);
        }

        public void Update(Vacation vacation)
        {
            repository.Update(vacation);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Boolean AcceptVacation(Doctor doctor, Vacation vacation)
        {
            return repository.AcceptVacation(doctor, vacation);
        }

        public Boolean RejectVacation(Doctor doctor, Vacation vacation,String comment)
        {
            return repository.RejectVacation(doctor, vacation,comment);
        }

        public VacationService()
        { }

        public static VacationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VacationService();
                }
                return instance;
            }
        }
    }
}
