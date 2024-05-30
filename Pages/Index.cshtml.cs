using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExtremeWeatherBoard.Pages
{
    public class IndexModel : PageModel
    {
        public MockRepo MockRepo { get; set; }
        public IndexModel()
        {
            MockRepo = new MockRepo();
        }
        public void OnGet()
        {

        }
    }
}
