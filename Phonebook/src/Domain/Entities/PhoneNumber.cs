using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Entities;

public partial class PhoneNumber
{
    public int Id { get; set; }
    public int AddressId { get; set; }
    public string Number { get; set; } = null!;
    public virtual Address Address { get; set; } = null!;
}
