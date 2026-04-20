// File:    ManagerService.cs
// Author:  halid
// Created: Saturday, 16 April 2022 01:46:35
// Purpose: Definition of Class ManagerService

using Model;
using System;
using System.Collections.Generic;
using Repository;
using ZdravoCorp.Service.Interfaces;

namespace Service
{
   public class ManagerService : ICrud<Manager>, IManagerService
    {
      private ManagerService instance;
      
      public void Create(Manager newManager)
      {
         throw new NotImplementedException();
      }
      
      public void Update(Manager manager)
      {
         throw new NotImplementedException();
      }
      
      public void Delete(int id)
      {
         throw new NotImplementedException();
      }
      
      public Manager Read(int id)
      {
         throw new NotImplementedException();
      }
      
      public List<Manager> GetAll()
      {
         throw new NotImplementedException();
      }
   
   }
}