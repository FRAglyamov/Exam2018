﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module2.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public List<ApplicationUser> SharedUserId { get; set; }
    }
}
