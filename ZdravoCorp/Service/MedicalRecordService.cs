using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using ZdravoCorp.Repository.Interfaces;
using ZdravoCorp.Service.Interfaces;

namespace Service
{
    public class MedicalRecordService : ICrud<MedicalRecord> , IMedicalRecordService
    {
        private static MedicalRecordService instance = null;

        private IMedicalRecordRepository repository = MedicalRecordRepository.Instance;

        public void Create(MedicalRecord newRecord)
        {
            repository.Create(newRecord);
        }

        public void Update(MedicalRecord record)
        {
            repository.Update(record);
        }

        public void Delete(int record)
        {
            repository.Delete(record);
        }

        public MedicalRecord Read(int record)
        {
            return repository.Read(record);
        }

        public List<MedicalRecord> GetAll()
        {
            return repository.GetAll();
        }

        public MedicalRecordService()
        {

        }

        public static MedicalRecordService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalRecordService();
                }
                return instance;
            }
        }
    }
}
