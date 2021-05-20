using System;
using System.Collections.Generic;
using System.Text;

namespace gestionRelationClient.Models
{
    class Role
    {
        public int RoleId { get; set; }

        public String Title { get; set; }

        public void ModifierRole(string newTitle)
        {
            this.Title = newTitle;
        }
    }
}
