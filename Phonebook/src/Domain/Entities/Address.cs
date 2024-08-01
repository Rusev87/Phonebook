using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Entities;

public partial class Address
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public AddressType Type { get; set; }
    public string AddressDetail { get; set; } = null!;
    public virtual Person Person { get; set; } = null!;
    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
}
