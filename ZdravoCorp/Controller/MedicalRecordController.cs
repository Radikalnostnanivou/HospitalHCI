using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Service;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class MedicalRecordController : ICrud<MedicalRecord>
    {
        public IMedicalRecordService service = MedicalRecordService.Instance;
        public void Create(MedicalRecord newRecord)
        {
            service.Create(newRecord);
        }

        public void Update(MedicalRecord record)
        {
            service.Update(record);
        }

        public void Delete(int record)
        {
            service.Delete(record);
        }

        public MedicalRecord Read(int record)
        {
            return service.Read(record);
        }

        public List<MedicalRecord> GetAll()
        {
            return service.GetAll();
        }
    }
}
