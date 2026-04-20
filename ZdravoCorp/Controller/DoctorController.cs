/***********************************************************************
 * Module:  RoomController.cs
 * Author:  halid
 * Purpose: Definition of the Class Controller.RoomController
 ***********************************************************************/

using Model;
using System;
using Repository;
using Service;
using System.Collections.Generic;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class DoctorController : ICrud<Doctor>
    {
        public IDoctorService service = DoctorService.Instance;
        public void Create(Model.Doctor newDoctor)
        {
            service.Create(newDoctor);
        }

        public void Update(Model.Doctor updatedRoom)
        {
            service.Update(updatedRoom);
        }

        public void Delete(int doctor)
        {
            service.Delete(doctor);
        }

        public Doctor Read(int doctor)
        {
            return service.Read(doctor);
        }
        
        public List<Doctor> GetAll()
        {
            return service.GetAll();
        }

        public void CreateDoctorType(DoctorType newDoctorType)
        {
            service.CreateDoctorType(newDoctorType);
        }

        public void UpdateDoctorType(DoctorType doctorType)
        {
            service.UpdateDoctorType(doctorType);
        }

        public void DeleteDoctorType(DoctorType doctorType)
        {
            service.DeleteDoctorType(doctorType.Type);
        }

        public DoctorType ReadDoctorType(DoctorType doctorType)
        {
            return service.ReadDoctorType(doctorType.Type);
        }

        public List<DoctorType> GetAllDoctorType()
        {
            return service.GetAllDoctorType();
        }
    }
}