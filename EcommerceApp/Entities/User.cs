using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities;

public class User : IEntity
{
    public int Id { get; set; }
    [Display(Name = "Name"), Required, StringLength(50)]
    public string Name { get; set; }
    [Display(Name = "Surname"), Required, StringLength(50)]
    public string Surname { get; set; }
    [Display(Name = "Username"), Required, StringLength(50)]
    public string Username { get; set; }
    [Display(Name = "Password"), Required, StringLength(150)]
    public string Password { get; set; }
    [Display(Name = "Email Address"), Required, StringLength(15), EmailAddress]
    public string Email { get; set; }
    [Display(Name = "Phone Number"), Required, StringLength(15)]
    public string PhoneNumber { get; set; }

    [ScaffoldColumn(false)]
    public DateTime CreateDate { get; set; }

    public bool IsActive { get; set; }
}
