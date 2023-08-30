using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities;

public class Slider : IEntity
{
    public int Id { get; set; }
    [Display(Name = "Main"), Required, StringLength(50)]
    public string Name { get; set; }
    [Display(Name = "Slider Description"), DataType(DataType.MultilineText), StringLength(150)]
    public string Description { get; set; }
    public string Link { get; set; }

    [Display(Name = "Slider Image")]
    public string? ImageUrl { get; set; }
}
