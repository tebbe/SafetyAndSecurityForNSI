﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ModelView
{
    public partial class SignInUserInfo
    {
            public long UserId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string NID { get; set; }
    }
}