﻿using System;

namespace WebApplication3.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

}
