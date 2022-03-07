using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{

    public partial class Business : EntityBase<int>
    {
        public Business(Project project, string name) : this()
        {
            this.Name = name;
            this.Project = project;
        }

        public Business(int projectId, string name) : this()
        {
            this.Name = name;
            this.ProjectId = projectId;
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }

    
    }
}
