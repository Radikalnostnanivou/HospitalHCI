// File:    Serializer.cs
// Author:  halid
// Created: Sunday, 10 April 2022 14:42:13
// Purpose: Definition of Class Serializer

using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class Serializer<T> where T : Serializable, new()
    {
        private char dELIMITER = '|';

        public void ToCSV(string fileName, List<T> objects)
        {
            StreamWriter streamWriter = new StreamWriter(fileName);

            foreach (Serializable obj in objects)
            {
                string line = string.Join(dELIMITER.ToString(), obj.ToCSV());
                streamWriter.WriteLine(line);
            }
            streamWriter.Close();
        }

        public List<T> FromCSV(string filename)
        {
            List<T> objects = new List<T>();

            foreach (string line in File.ReadLines(filename))
            {
                string[] csvValues = line.Split(dELIMITER);
                T obj = new T();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }

        public void ToCSVAppend(string fileName, List<T> objects)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, true);

            foreach (Serializable obj in objects)
            {
                string line = string.Join(dELIMITER.ToString(), obj.ToCSV());
                streamWriter.WriteLine(line);
            }

            streamWriter.Close();
        }

    }
}