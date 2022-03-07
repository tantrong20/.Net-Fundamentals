using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    public partial class Project 
    {
       
        public Project(string name, string userId) : this()
        {
            Name = name;
            CreatedBy = userId;
            CreatedOn = DateTime.Now;
        }

        public void AddBusiness(string name)
        {
            Businesses.Add(new Business(this, name));
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }
    }
}


