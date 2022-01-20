﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain;

namespace Cofoundry.Web.Admin
{
    public class AdminAuthorizeAttribute : AuthorizeUserAreaAttribute
    {
        public AdminAuthorizeAttribute()
            : base(CofoundryAdminUserArea.Code)
        {
        }
    }
}