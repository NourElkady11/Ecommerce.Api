﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User:IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

    /*    public string? Username { get; set; }*/

        public Address address { get; set; }

    }
}
