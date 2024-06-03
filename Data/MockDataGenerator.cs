namespace ExtremeWeatherBoard.Data
{
    using ExtremeWeatherBoard.Interfaces;
    using ExtremeWeatherBoard.Data;
    using ExtremeWeatherBoard.Pages.Shared.ViewModels;
    using ExtremeWeatherBoard.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using ExtremeWeatherBoard.Models;

    public class MockDataGenerator : IMockData
    {
        private readonly DataRepository _datarepository;
        private string LoremIpsum { get; set; }
        public MockDataGenerator(DataRepository dataRepository)
        {
            _datarepository = dataRepository;
            LoremIpsum= "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }

        string IMockData.LoremIpsum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task MockLoadsOfDataAsync()
        {
            //AdminUserData admin = new AdminUserData() { UserId = "MOCKED ADMINUSERDATA" };
            //await _datarepository.AddAdminUserDataAsync(admin);
            //for (int i = 0; i < 5; i++)
            //{
            //    UserData userData = new UserData()
            //    { UserId = "MOCKED USERDATA" };
            //}
            
            await _datarepository.PopulateAdminUserDatasAsync();
            var adminUser = _datarepository.AdminUsers.FirstOrDefault();
            await _datarepository.PopulateCategoriesAsync();

            if (_datarepository.AdminUsers is List<AdminUserData> 
                && _datarepository.AdminUsers.Count > 0 
                && _datarepository.Categories is List<Category> 
                && _datarepository.Categories.Count == 0)
            {
                
                for (int i = 0; i < 4; i++)
                {
                    Category category = new Category()
                    {
                        Title = $"Title Category {i}"
                    ,
                        Name = $"Name Category {i}"
                    ,
                        CreatedDate = DateTime.Now
                    ,
                        Creator = adminUser
                };
                await _datarepository.AddCategoryAsync(category);
            }
            await _datarepository.PopulateCategoriesAsync();
        }

            if (_datarepository.Categories.Count > 0)
            {

                foreach (Category savedCategory in _datarepository.Categories)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        SubCategory subCategory = new SubCategory()
                        {
                            ParentCategoryId = savedCategory.Id,
                            Title = $"Title Category {i}",
                            CreatedAt = DateTime.Now,             
                            Creator = adminUser
                        };
                        await _datarepository.AddSubCategoryAsync(subCategory);
                    }
                }
            }
            await _datarepository.PopulateCategoriesAsync();
            await _datarepository.PopulateSubCategoriesAsync();
            if (_datarepository.SubCategories is List<SubCategory>)
            {
                int userCounter = 1;
                foreach (var subCategory in _datarepository.SubCategories)
                {
                    DiscussionThread subThread = new DiscussionThread()
                    {
                        CreatorUserId = userCounter
                        ,
                        SubCategoryId = subCategory.Id
                        ,
                        Title = $"Subcategory of Category id {_datarepository.Categories[subCategory.ParentCategoryId]}"
                        ,
                        CreatedAt = DateTime.Now
                        ,
                        Text = LoremIpsum
                    };
                    await _datarepository.AddDiscussionThreadAsync(subThread);
                    if (userCounter == 5)
                        userCounter = 0;
                    userCounter++;
                }
            }
            await _datarepository.PopulateSubCategoriesAsync();
            await _datarepository.PopulateDiscussionThreadsAsync();
            if (_datarepository.DiscussionThreads is List<DiscussionThread>)
            {
                int userCounter = 1;
                foreach (DiscussionThread discussionThread in _datarepository.DiscussionThreads)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (userCounter != 4)
                        {
                            Comment newComment = new Comment()
                            {
                                CommentUserDataId = userCounter
                                ,
                                CommentThreadId = discussionThread.Id
                                ,
                                PostedAt = DateTime.Now
                                ,
                                Title = $"UD {userCounter}-DT {discussionThread.Id}-SC{discussionThread.SubCategoryId}"
                                ,
                                Text = LoremIpsum
                            };
                            await _datarepository.AddCommentAsync(newComment);
                        }
                        if (userCounter == 4)
                        {
                            Comment newComment = new Comment()
                            {
                                CommentAdminUserDataId = 1
                                ,
                                CommentThreadId = discussionThread.Id
                                ,
                                PostedAt = DateTime.Now
                                ,
                                Title = $"AdminUser 1-DT {discussionThread.Id}-SC{discussionThread.SubCategoryId}"
                                ,
                                Text = LoremIpsum
                            };
                            await _datarepository.AddCommentAsync(newComment);
                        }
                    }
                }
            }

            await _datarepository.PopulateUserDatasAsync();
            if(_datarepository.Users is List<UserData>)
            {
                foreach (UserData user in _datarepository.Users) 
                {
                    for (int i = 1; i < _datarepository.Users.Count; i++)
                    {
                        Message message = new Message()
                        {
                            SenderId = user.Id,
                            ReceiverId = i,
                            SentAt = DateTime.Now,
                            Topic = $"Message from user {user.Id} to user {i}",
                            Text = LoremIpsum,                            
                        };
                    }

                }
            }

        }
    }
}
