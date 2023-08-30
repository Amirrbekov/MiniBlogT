using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;

public class Category : IEntity
{
    public int Id { get; set; }
    [Display(Name = "Sub Category")]
    public int ParentId { get; set; }
    [Display(Name = "Category Name"), Required, StringLength(50)]
    public string Name { get; set; }
    [Display(Name = "Category Description"), DataType(DataType.MultilineText)]
    public string Description { get; set; }
    [Display(Name = "Category Image")]
    public string? ImageUrl { get; set; }
}
