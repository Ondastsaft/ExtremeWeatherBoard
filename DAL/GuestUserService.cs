using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard.DAL
{
    public static class GuestUserService
    {
        public static UserData GuestUserData{ get; } = new UserData() {Id = 0, UserId ="Guest"};
    }
}
