﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Tag")]
    public class Tag : EntityBase<int>
    {
        public Tag()
        {

        }
        public Tag(int projectId, string name): this()
        {
            this.ProjectId = projectId;
            this.Name = name;
        }
        public string Name { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
