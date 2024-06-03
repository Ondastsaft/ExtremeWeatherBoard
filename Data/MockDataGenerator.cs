namespace ExtremeWeatherBoard.Data
{
    using ExtremeWeatherBoard.Interfaces;
    using ExtremeWeatherBoard.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class MockDataGenerator : IMockData
    {
        private readonly DataRepository _datarepository;
        private readonly UserManager<IdentityUser> _userManager;
        private string LoremIpsum { get; set; }
        public MockDataGenerator(DataRepository dataRepository, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _datarepository = dataRepository;
            LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }

        string IMockData.LoremIpsum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private async Task LoadIdentityUsers()
        {
            for (int i = 1; i <= 10; i++)
            {
                if (await _userManager.FindByNameAsync($"test{i}") == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = $"test{i}",
                        Email = $"test{i}@example.com"
                    };

                    await _userManager.CreateAsync(user, "Password123!");
                }
            }
        }
        private async Task LoadUserDatas()
        {
            var identityUsers = await _userManager.Users.ToListAsync();
            if (identityUsers != null && identityUsers.Count > 0)
            {
                // Assuming the first user is the admin
                var adminUser = identityUsers[0];
                await _datarepository.AddAdminUserDataAsync(new AdminUserData { UserId = adminUser.Id });

                // Create UserData for the rest of the users
                for (int i = 1; i < identityUsers.Count; i++)
                {
                    var user = identityUsers[i];
                    await _datarepository.AddUserDataAsync(new UserData { UserId = user.Id });
                }
            }
        }
        private async Task LoadCategories()
        {
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
                        CreatedDate = DateTime.Now
                    ,
                        CreatorAdminUser = adminUser
                    };
                    await _datarepository.AddCategoryAsync(category);
                }
            }
        }
        private async Task LoadSubCategories()
        {
            await _datarepository.PopulateAdminUserDatasAsync();
            var adminUser = _datarepository.AdminUsers.FirstOrDefault();
            await _datarepository.PopulateCategoriesAsync();
            await _datarepository.PopulateSubCategoriesAsync();


            if (_datarepository.Categories.Count > 0 && _datarepository.SubCategories.Count == 0)
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
                            SubCategoryAdminUserData = adminUser
                        };
                        await _datarepository.AddSubCategoryAsync(subCategory);
                    }
                }
            }
        }
        private async Task LoadDiscussionThreads()
        {
            await _datarepository.PopulateCategoriesAsync();
            await _datarepository.PopulateSubCategoriesAsync();
            await _datarepository.PopulateDiscussionThreadsAsync();
            await _datarepository.PopulateUserDatasAsync();
            if (_datarepository.SubCategories is List<SubCategory> && _datarepository.DiscussionThreads.Count == 0 && _datarepository.Users.Count > 0)
            {
                List<UserData> users = _datarepository.Users;
                int count = users.Count();
                int userCounter = 1;
                foreach (var subCategory in _datarepository.SubCategories)
                {
                    var user = _datarepository.Users.Find(u => u.Id == userCounter);
                    if (user is UserData)
                    {
                        DiscussionThread subThread = new DiscussionThread()
                        {
                            DiscussionThreadUserData = user
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
                        if (userCounter == count)
                        {
                            userCounter = 0;
                        }
                        userCounter++;
                    }
                }
            }
        }
        private async Task LoadComments()
        {
            await _datarepository.PopulateAdminUserDatasAsync();
            var adminUser = _datarepository.AdminUsers.FirstOrDefault();

            await _datarepository.PopulateSubCategoriesAsync();
            await _datarepository.PopulateDiscussionThreadsAsync();
            await _datarepository.PopulateCommentsAsync();
            if (_datarepository.DiscussionThreads is List<DiscussionThread>
                && _datarepository.Comments.Count == 0
                && _datarepository.Users.Count != 0
                && adminUser is AdminUserData)
            {
                List<UserData> users = _datarepository.Users;
                int userCounter = 1;
                foreach (DiscussionThread discussionThread in _datarepository.DiscussionThreads)
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        if (userCounter != 6)
                        {
                            Comment newComment = new Comment()
                            {
                                CommentUserData = users[userCounter]
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
                        if (userCounter == 6)
                        {
                            Comment newComment = new Comment()
                            {
                                CommentAdminUserData = adminUser
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
                            userCounter = 0;
                        }
                        userCounter++;
                    }
                }
            }

        }
        public async Task MockLoadsOfDataAsync()
        {
            await LoadIdentityUsers();
            await LoadUserDatas();
            await LoadCategories();
            await LoadSubCategories();
            await LoadComments();




        }
    }
}


