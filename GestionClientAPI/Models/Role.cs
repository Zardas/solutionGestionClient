﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public String Title { get; set; }

        public void ModifierRole(string newTitle)
        {
            this.Title = newTitle;
        }
    }
}
