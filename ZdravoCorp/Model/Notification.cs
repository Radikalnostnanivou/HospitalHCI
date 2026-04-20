/***********************************************************************
 * Module:  Notification.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.Notification
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Notification : Serializable
    {
        private int id;
        private DateTime dateCreated;
        private String content;

        private User user;

        public int Id { get => id; set => id = value; }
        public DateTime DateCreated { get { return dateCreated; } set { dateCreated = value; } }    
        public String Content { get { return content; } set { content = value; } }

        public Notification(DateTime dateCreated, string content, int id)
        {
            this.dateCreated = dateCreated;
            this.content = content;
            this.user.Id = id;
        }

        public Notification() { }

        /// <summary>
        /// Property for User
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (this.user == null || !this.user.Equals(value))
                {
                    if (this.user != null)
                    {
                        User oldUser = this.user;
                        this.user = null;
                        oldUser.RemoveNotification(this);
                    }
                    if (value != null)
                    {
                        this.user = value;
                        this.user.AddNotification(this);
                    }
                }
            }
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            DateCreated = DateTime.Parse(values[i++]);
            Content = values[i++];
            User = new User(int.Parse(values[i++]));
        }

        private void NotificationInfoToString(List<String> result)
        {
            result.Add(id.ToString());
            result.Add(dateCreated.ToString());
            result.Add(content);
        }

        private void CreatorToString(List<String> result)
        {
            result.Add(User.Id.ToString());
        }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();
            NotificationInfoToString(result);
            CreatorToString(result);
            return result;
        }
    }
}