﻿using System.Security.Claims;
using System.Collections.Generic;

namespace ASPNetCoreApp.Domain.DTO.Identity
{
    public abstract class ClaimDTO : UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
    }

    public class AddClaimDTO : ClaimDTO
    {

    }

    public class RemoveClaimDTO : ClaimDTO
    {

    }

    public class ReplaceClaimDTO : UserDTO
    {
        public Claim Claim { get; set; }
        public Claim NewClaim { get; set; }

    }
}
