// File:    SecretaryController.cs
// Author:  halid
// Created: Saturday, 16 April 2022 01:47:12
// Purpose: Definition of Class SecretaryController

using Model;
using System;
using System.Collections.Generic;
using Service;
using Repository;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
   public class SecretaryController : ICrud<Secretary>
   {
      public ISercretaryService service = SecretaryService.Instance;
      public void Create(Secretary newSecretary)
      {
         throw new NotImplementedException();
      }
      
      public void Update(Secretary secretary)
      {
         throw new NotImplementedException();
      }
      
      public void Delete(int id)
      {
         throw new NotImplementedException();
      }
      
      public Secretary Read(int id)
      {
         throw new NotImplementedException();
      }
      
      public List<Secretary> GetAll()
      {
            return service.GetAll();
      }
   
   }
}