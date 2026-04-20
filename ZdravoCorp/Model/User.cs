/***********************************************************************
 * Module:  User.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.User
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class User : Serializable
    {
        protected int id;
        protected String password;
        protected String username;
        /*mora ovako*/
        public string name { get; set; }
        public string surname { get; set; }
        protected String jmbg;
        protected String email;
        protected String address;
        protected String phoneNumber;
        protected Gender gender;
        protected DateTime dateOfBirth;

        protected List<Notification> notification;
        public string nameSurname { get => name + " " + surname; set => name = value; }
        public string PatNameSurname { get => name + " " + surname; set => name = value; }
        public User(int id, string password, string username, string name, string surname, string jmbg, string email, string address, string phoneNumber, Gender gender, DateTime dateOfBirth, List<Notification> notification, List<AppointmentSurvey> survey)
        {
            this.id = id;
            this.password = password;
            this.username = username;
            this.name = name;
            this.surname = surname;
            this.jmbg = jmbg;
            this.email = email;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.notification = notification;
        }

        public User()
        {
        }

        public User(User user)
        {
            this.id = user.id;
            this.password = user.password;
            this.username = user.username;
            this.name = user.name;
            this.surname = user.surname;
            this.jmbg = user.jmbg;
            this.email = user.email;
            this.address = user.address;
            this.phoneNumber = user.phoneNumber;
            this.gender = user.gender;
            this.dateOfBirth = user.dateOfBirth;
            this.notification = user.notification;
        }

        public User(int id)
        {
            this.id = id;
        }


        /// <summary>
        /// Property for collection of Notification
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Notification> Notification
        {
            get
            {
                if (notification == null)
                    notification = new List<Notification>();
                return notification;
            }
            set
            {
                RemoveAllNotification();
                if (value != null)
                {
                    foreach (Notification oNotification in value)
                        AddNotification(oNotification);
                }
            }
        }
        public int Id { get => id; set => id = value; }
        public String Jmbg { get => jmbg; set => jmbg = value; }
        public String Username { get => username; set => username = value; }
        public String Password { get => password; set => password = value; }
        public String Namee { get => name; set => name = value; }
        public String Surname { get => surname; set => surname = value; }
        public String Email { get => email; set => email = value; }
        public String Address { get => address; set => address = value; }
        public String PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public Gender Gender { get => gender; set => gender = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        /// <summary>
        /// Add a new Notification in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddNotification(Notification newNotification)
        {
            if (newNotification == null)
                return;
            if (this.notification == null)
                this.notification = new List<Notification>();
            if (!this.notification.Contains(newNotification))
            {
                this.notification.Add(newNotification);
                newNotification.User = this;
            }
        }

        /// <summary>
        /// Remove an existing Notification from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveNotification(Notification oldNotification)
        {
            if (oldNotification == null)
                return;
            if (this.notification != null)
                if (this.notification.Contains(oldNotification))
                {
                    this.notification.Remove(oldNotification);
                    oldNotification.User = null;
                }
        }

        /// <summary>
        /// Remove all instances of Notification from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllNotification()
        {
            if (notification != null)
            {
                System.Collections.ArrayList tmpNotification = new System.Collections.ArrayList();
                foreach (Notification oldNotification in notification)
                    tmpNotification.Add(oldNotification);
                notification.Clear();
                foreach (Notification oldNotification in tmpNotification)
                    oldNotification.User = null;
                tmpNotification.Clear();
            }
        }

        private void UserInfoToListString(List<string> result)
        {
            result.Add(id.ToString());
            result.Add(password);
            result.Add(username);
            result.Add(name);
            result.Add(surname);
            result.Add(jmbg);
            result.Add(email);
            result.Add(address);
            result.Add(phoneNumber);
            result.Add(gender.ToString());
            result.Add(dateOfBirth.ToString());
        }

        private void UserNotificationsToListString(List<string> result)
        {
            result.Add(Notification.Count.ToString());
            foreach(Notification it in Notification)
            {
                result.AddRange(it.ToCSV());
            }
        }
        private string[] ReadUserInformation(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            Password = values[i++];
            Username = values[i++];
            name = values[i++];
            surname = values[i++];
            jmbg = values[i++];
            email = values[i++];
            address = values[i++];
            phoneNumber = values[i++];
            gender = (Gender)Enum.Parse(typeof(Gender), values[i++]);
            dateOfBirth = DateTime.Parse(values[i++]);
            return values.Skip(i).ToArray();
        }

        private void ReadUserNotifications(string[] values)
        {
            int i = 0;
            int count = int.Parse(values[i++]) + i;
            for(; i < count ; i++)
            {
                Notification notification = new Notification();
                notification.FromCSV(values);
                AddNotification(notification);
                values = values.Skip(4).ToArray();
            }
        }

        public List<String> ToCSV()
        {
            List<string> result = new List<string>();
            UserInfoToListString(result);
            UserNotificationsToListString(result);
            return result;
        }

        public void FromCSV(string[] values)
        {
            values = ReadUserInformation(values);
            ReadUserNotifications(values);
        }
    }
}