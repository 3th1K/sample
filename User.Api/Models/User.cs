using System;
using System.Collections.Generic;

namespace UserService.Api.Models;

public partial class User
{
    public string? UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Gender { get; set; }

    public DateTime? RegisteredOn { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public int Id { get; set; }

    public bool? Admin { get; set; }

    public bool? Vendor { get; set; }

    public DateTime? LastLogin { get; set; }
}
