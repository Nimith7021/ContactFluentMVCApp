using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactAppFluentMVC.Models;
using FluentNHibernate.Mapping;

namespace ContactAppFluentMVC.Mappings
{
    public class UserMap:ClassMap<User>
    {
        public UserMap() {

            Table("Users");
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.FName);
            Map(u => u.LName);
            Map(u => u.IsAdmin);
            Map(u => u.IsActive);
            HasMany(c => c.Contacts).Inverse().Cascade.All();
        }    
    }
}