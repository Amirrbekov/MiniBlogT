using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities;

public class Post : IEntity
{
    public int Id { get; set; }
    [Display(Name = "Main"), Required, StringLength(50)]
    public string Name { get; set; }
    public string Content { get; set; }
    [Display(Name = "Content Image")]
    public string? ImageUrl { get; set; }
    [ScaffoldColumn(false)]
    public DateTime CreateDate { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

}
