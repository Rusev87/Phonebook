using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Entities;

public partial class Person
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
