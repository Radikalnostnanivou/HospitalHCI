/***********************************************************************
 * Module:  Employee.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.Employee
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Employee : User , Serializable
    {
        protected float salary;
        protected DateTime enrolementDate;
        public DateTime workStartTime { get; set; }
        public DateTime workEndTime { get; set; }    
        protected DateTime vacationStartTime;
        protected DateTime vacationEndTime;
        protected int vacationDays;

        public DateTime WorkStartTime { get => workStartTime; set => workStartTime = value; }   
        public DateTime WorkEndTime { get => workEndTime; set => workEndTime = value; }
        protected Employee(int id, string password, string username, string name, string surname, string jmbg, string email, string address, string phoneNumber, Gender gender, DateTime dateOfBirth, List<Notification> notification, List<AppointmentSurvey> survey, float salary, DateTime enrolementDate, DateTime workStartTime, DateTime workEndTime, DateTime vacationStartTime, DateTime vacationEndTime, int vacationDays) : base(id, password, username, name, surname, jmbg, email, address, phoneNumber, gender, dateOfBirth, notification, survey)
        {
            this.salary = salary;
            this.enrolementDate = enrolementDate;
            this.workStartTime = workStartTime;
            this.workEndTime = workEndTime;
            this.vacationStartTime = vacationStartTime;
            this.vacationEndTime = vacationEndTime;
            this.vacationDays = vacationDays;
        }

        protected Employee()
        {
        }

        protected Employee(User user)
        {
        }

        protected Employee(Employee employee) : base((User)employee)
        {
            this.salary = employee.salary;
            this.enrolementDate = employee.enrolementDate;
            this.workStartTime = employee.workStartTime;
            this.workEndTime = employee.workEndTime;
            this.vacationStartTime = employee.vacationStartTime;
            this.vacationEndTime = employee.vacationEndTime;
            this.vacationDays = employee.vacationDays;
        }

        public Employee(float salary, DateTime enrolementDate, DateTime workStartTime, DateTime workEndTime, DateTime vacationStartTime, DateTime vacationEndTime, int vacationDays, User user) : base(user)
        {
            this.salary = salary;
            this.enrolementDate = enrolementDate;
            this.workStartTime = workStartTime;
            this.workEndTime = workEndTime;
            this.vacationStartTime = vacationStartTime;
            this.vacationEndTime = vacationEndTime;
            this.vacationDays = vacationDays;
        }

        private void EmployeeInfoToListString(List<string> result)
        {
            result.Add(salary.ToString());
            result.Add(enrolementDate.ToString());
            result.Add(workStartTime.ToString());
            result.Add(workEndTime.ToString());
            result.Add(vacationStartTime.ToString());
            result.Add(vacationEndTime.ToString());
            result.Add(vacationDays.ToString());
        }

        private string[] ReadEmployeeInfo(string[] values)
        {
            int i = 0;
            salary = float.Parse(values[i++]);
            enrolementDate = DateTime.Parse(values[i++]);
            workStartTime = DateTime.Parse(values[i++]);
            workEndTime = DateTime.Parse(values[i++]);
            vacationStartTime = DateTime.Parse(values[i++]);
            vacationEndTime = DateTime.Parse(values[i++]);
            vacationDays = int.Parse(values[i++]);
            return values.Skip(i).ToArray();
        }
        public void FromCSV(string[] values)
        {
            values = ReadEmployeeInfo(values);
            base.FromCSV(values);
        }

        public List<string> ToCSV()
        {
            List<string> result = new List<string>();
            EmployeeInfoToListString(result);
            result.AddRange(base.ToCSV());
            return result;
        }
    }

    
}