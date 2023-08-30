using Entities;

namespace EcommerceApp.Models;

public class HomePageViewModel
{
    public List<Category> CategoryList { get; set; }
    public List<Slider> SliderList { get; set; }
    public List<News> NewsList { get; set; }
    public List<Post> PostList { get; set; }

}
