using Microsoft.AspNetCore.Mvc;

namespace ExtremeWeatherBoard.DTO
{
    public class PageRedirectionDTO
    {
        public string PageName { get; set; }
        public Dictionary<string, int> RedirectionParameters { get; set; }
        public PageRedirectionDTO(string pageName)
        {
            PageName = pageName;
            RedirectionParameters = new Dictionary<string, int>();
        }

    }
}
