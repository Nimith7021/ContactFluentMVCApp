﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactAppFluentMVC.Models;
using FluentNHibernate.Mapping;

namespace ContactAppFluentMVC.Mappings
{
    public class ContactMap:ClassMap<Contact>
    {
        public ContactMap() {

            Table("Contacts");
            Id(c => c.Id).GeneratedBy.Identity();
            Map(c => c.FName);
            Map(c => c.LName);
            Map(c => c.IsActive);
            References(u => u.User).Column("UserId").Cascade.None().Nullable();
            HasMany(cd => cd.ContactDetails).Inverse().Cascade.All();
        }
    }
}