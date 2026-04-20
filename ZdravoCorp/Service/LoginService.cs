using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace ZdravoCorp.Service
{
    public class LoginService
    {
        private static readonly object key = new object();
        private static LoginService instance = null;

        private IManagerRepository managerRepository = ManagerRepository.Instance;
        private IDoctorRepository doctorRepository = DoctorRepository.Instance;
        private IPatientRepository patientRepository = PatientRepository.Instance;
        private ISecretaryRepository secretaryRepository = SecretaryRepository.Instance;
        private Dictionary<string, Manager> managerMap;
        private Dictionary<string, Doctor> doctorMap;
        private Dictionary<string, Patient> patientMap;
        private Dictionary<string, Secretary> secretaryMap;

        private void InstantiateHashSet()
        {
            managerMap = managerRepository.GetUsernameHashSet();
            doctorMap = doctorRepository.GetUsernameHashSet();
            patientMap = PatientRepository.Instance.Users;
            secretaryMap = secretaryRepository.GetUsernameHashSet();
        }

        private void MergeDictionaries(Dictionary<string, string> mergedInto, Dictionary<string, string> dictionary)
        {
            foreach(var item in dictionary)
            {
                mergedInto.Add(item.Key, item.Value);
            }
        }

        public User Login(string username, string password)
        {
            if(IsPatient(username, password))
            {
                return patientMap[username];
            }
            else if(IsDoctor(username, password))
            {
                return doctorMap[username];
            }
            else if (IsManager(username, password))
            {
                return managerMap[username];
            }
            else if (IsSecretary(username, password))
            {
                return secretaryMap[username];
            }
            else
            {
                throw new LocalisedException("UserDoesntExist");
            }
        }

        public bool IsPatient(string username, string password)
        {
            if (patientMap.ContainsKey(username))
            {
                if(patientMap[username].Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsManager(string username, string password)
        {
            if (managerMap.ContainsKey(username))
            {
                if (managerMap[username].Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsDoctor(string username, string password)
        {
            if (doctorMap.ContainsKey(username))
            {
                if (doctorMap[username].Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSecretary(string username, string password)
        {
            if (secretaryMap.ContainsKey(username))
            {
                if (secretaryMap[username].Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public LoginService()
        {
            InstantiateHashSet();
        }

        public static LoginService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new LoginService();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
