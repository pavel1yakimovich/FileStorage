﻿using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class BllUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public IEnumerable<BllRole> Role { get; set; }
    }
}