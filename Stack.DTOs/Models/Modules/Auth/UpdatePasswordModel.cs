﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.DTOs.Models.Modules.Auth
{
    public class UpdatePasswordModel
    {
        public string UserID { get; set; }
        public string Password { get; set; }

    }

}
