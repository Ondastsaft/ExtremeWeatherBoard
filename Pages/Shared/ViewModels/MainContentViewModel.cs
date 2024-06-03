using ExtremeWeatherBoard.Interfaces;
using ExtremeWeatherBoard.Models;


namespace ExtremeWeatherBoard.Pages.Shared.ViewModels
{
    public class MainContentViewModel
    {
        public List<IMainContent> MainContentList { get; set; }
        public DiscussionThread? CommentsParentDiscussionThread { get; set; }
        public MainContentViewModel()
        {
            MainContentList = new List<IMainContent>();
        }
    }
}
