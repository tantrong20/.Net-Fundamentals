using _468_.Net_Fundamentals.Domain.Repositories;
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
       
        public Project(string userId, string name) : this()
        {
            Name = name;
            CreatedBy = userId;
            CreatedOn = DateTime.Now;
        }

        public void AddBusiness(string name)
        {
            //var isNameValid = businessRepo.ValidateName(name);
            this.Businesses.Add(new Business(this, name));
            // Push Add business event (project id, business name)
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }
        public void AddTag (string name)
        {
            this.Tags.Add(new Tag(this.Id, name));
        }
    }
}


