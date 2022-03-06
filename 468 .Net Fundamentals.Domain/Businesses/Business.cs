using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Business")]
    public class Business : EntityBase<int>
    {
        private Business()
        {

        }
        public Business(int projectId, string name)
        {
            this.Name = name;
            SetProjectId(projectId);
        }
        public string Name { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]      
        public virtual Project Project { get; set; }

        public virtual IList<Card> Cards { get; set; }

        public void SetProjectId(int projectId)
        {

            this.ProjectId = projectId;
        }
    }
}
