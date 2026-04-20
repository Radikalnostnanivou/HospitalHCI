// File:    MedicationType.cs
// Author:  halid
// Created: Sunday, 17 April 2022 01:56:20
// Purpose: Definition of Class MedicationType

using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorp.View.Core;

namespace Model
{
    public class MedicationType : ObservableObject, Serializable
    {
        private int id;
        private String name;
        private String manufacturer;
        private String description;
        private List<MedicationType> replacement;
        private List<MedicationType> ingredients;

        public int Id
        {
            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Manufacturer
        {
            get => manufacturer;
            set
            {
                if (value != manufacturer)
                {
                    manufacturer = value;
                    OnPropertyChanged();
                }
            }
        }    
        public String Description
        {
            get => description;
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<MedicationType> Replacement
        {
            get => replacement;
            set
            {
                if (value != replacement)
                {
                    replacement = value;
                    OnPropertyChanged();
                }
            }
        }

        public String ReplacementTable
        {
            get 
            {
                string result = "";
                foreach(MedicationType item in replacement)
                {
                    if(item.Name != null)
                    {
                        result += item.Name + ", ";
                    }
                }
                if(result.Length > 0)
                {
                    result = result.Remove(result.Length - 2);
                }
                return result; 
            }
        }

        public List<MedicationType> Ingredients
        {
            get => ingredients;
            set
            {
                if(value != ingredients)
                {
                    ingredients = value;
                    OnPropertyChanged();
                }
            }
        }

        public MedicationType(int id)
        {
            this.Id = id;
        }

        public MedicationType(int id, string name, string manufacturer, string description) : this(id)
        {
            this.name = name;
            this.manufacturer = manufacturer;
            this.description = description;
            Replacement = new List<MedicationType>();
            Ingredients = new List<MedicationType>();
        }

        public MedicationType(MedicationType type)
        {
            this.Id = type.Id;
            this.name = type.Name;
            this.manufacturer = type.Manufacturer;
            this.description = type.Description;
            Replacement = type.Replacement;
            Ingredients = type.Ingredients;
        }

        public MedicationType()
        {

        }

        public MedicationType(string name, string manufacturer, string description)
        {
            this.name = name;
            this.manufacturer = manufacturer;
            this.description = description;
            Replacement = new List<MedicationType>();
        }

        private void InfoToListString(List<string> result)
        {
            result.Add(id.ToString());
            result.Add(Name);
            result.Add(Manufacturer);
            result.Add(Description);
        }

        private void ReplacementsToListString(List<string> result)
        {
            result.Add(Replacement.Count.ToString());
            foreach(MedicationType type in Replacement)
            {
                result.Add(type.Id.ToString());
            }
        }

        private void IngredientsToStrings(List<string> result)
        {
            result.Add(Ingredients.Count.ToString());
            foreach (MedicationType type in Ingredients)
            {
                result.Add(type.Id.ToString());
            }
        }

        public List<string> ToCSV()
        {
            List<string> result = new List<string>();
            InfoToListString(result);
            ReplacementsToListString(result);
            IngredientsToStrings(result);
            return result;
        }

        private string[] ReadInfo(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            Name = values[i++];
            Manufacturer = values[i++];
            Description = values[i++];
            return values.Skip(i).ToArray();
        }

        private string[] ReadReplacements(string[] values)
        {
            Replacement = new List<MedicationType>();
            int i = 0;
            int count = int.Parse(values[i++]) + i;
            for(; i < count; i++)
            {
                Replacement.Add(new MedicationType(int.Parse(values[i])));
            }
            return values.Skip(i).ToArray();
        }

        private string[] ReadIngredients(string[] values)
        {
            Ingredients = new List<MedicationType>();
            int i = 0;
            int count = int.Parse(values[i++]);
            for (; i < count; i++)
            {
                Ingredients.Add(new MedicationType(int.Parse(values[i])));
            }
            return values.Skip(i).ToArray();
        }

        public void FromCSV(string[] values)
        {
            values = ReadInfo(values);
            values = ReadReplacements(values);
            ReadIngredients(values);
        }
    }
}