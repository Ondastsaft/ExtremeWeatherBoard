namespace ExtremeWeatherBoard.Data
{
    using ExtremeWeatherBoard.DAL;
    using ExtremeWeatherBoard.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class MockDataGenerator
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AdminService _adminService;
        private readonly CategoryApiService _categoryApiService;
        private readonly CommentService _commentService;
        private readonly DiscussionThreadService _discussionThreadService;
        private readonly MessageService _messageService;
        private readonly SubCategoryService _subCategoryService;
        private readonly UserDataService _userDataService;

        private string LoremIpsum { get; set; }
        public MockDataGenerator(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager, 
            AdminService adminService, 
            CategoryApiService categoryApiService,
            CommentService commentService,
            DiscussionThreadService discussionThreadService,
            MessageService messageService,
            SubCategoryService subCategoryService,
            UserDataService userDataService
            )
        {
            _context = context;
            _userManager = userManager;
            _adminService = adminService;
            _categoryApiService = categoryApiService;
            _commentService = commentService;
            _discussionThreadService = discussionThreadService;
            _messageService = messageService;
            _subCategoryService = subCategoryService;
            _userDataService = userDataService;
            LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }

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
                    string password = $"Password{i}{i}{i}!";
                    await _userManager.CreateAsync(user, password);
                }
            }
        }
        private async Task LoadUserDatas()
        {
            var identityUsers = await _userManager.Users.ToListAsync();
            if (identityUsers != null && identityUsers.Count > 0)
            {
                var adminUser = identityUsers[0];
                await _adminService.PostAdminUserDataAsync(adminUser.Id);

                for (int i = 1; i < identityUsers.Count; i++)
                {
                    var user = identityUsers[i];
                    await _userDataService.PostUserDataAsync(user.Id );
                }
            }
        }
        private async Task LoadCategories()
        {
            var adminUser = _context.AdminUserDatas.FirstOrDefault();
            if (adminUser != null)
            {
                for (int i = 1; i < 5; i++)
                {
                    await _categoryApiService.PostCategoryAsync($"Title Category {i}", adminUser);
                } 
            }            
        }
        private async Task LoadSubCategories()
        {
            var adminUser = await _context.AdminUserDatas.FirstOrDefaultAsync();
            var categories  = await  _categoryApiService.GetCategoriesAsync();


            if (categories is List<Category> && categories.Count > 0)
            {
                foreach (var savedCategory in categories)
                {
                    var categorysSubcategories = await _subCategoryService?.GetSubCategoriesFromParentIdAsync(savedCategory.Id);
                    for (int i = 0; i < 5-categorysSubcategories.Count; i++)
                    {
                        SubCategory subCategory = new SubCategory()
                        {
                            ParentCategoryId = savedCategory.Id,
                            Title = $"Title SubCategory {i}",
                            TimeStamp = DateTime.Now,
                            CreatorAdminUserData = adminUser
                        };
                        await _context.SubCategories.AddAsync(subCategory);
                        await _context.SaveChangesAsync();
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
                int idCounter = 1;
                int userCounter = 1;
                foreach (var subCategory in _datarepository.SubCategories)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        var user = _datarepository.Users.Find(u => u.Id == userCounter);
                        if (user is UserData)
                        {
                            DiscussionThread subThread = new DiscussionThread()
                            {
                                DiscussionThreadUserData = user
                                ,
                                SubCategory = subCategory
                                ,
                                Title = $"DiscussionThread id {i} of subcategory {subCategory.Id}"
                                ,
                                TimeStamp = DateTime.Now
                                ,
                                Text = LoremIpsum
                            };
                            await _datarepository.AddDiscussionThreadAsync(subThread);
                            if (userCounter == count)
                            {
                                userCounter = 0;
                            }
                            userCounter++;
                            idCounter++;
                        }
                    }
                }
            }
        }
        private async Task LoadComments()
        {
            await _datarepository.PopulateAdminUserDatasAsync();
            var adminUser = _datarepository.AdminUsers.FirstOrDefault();
            await _datarepository.PopulateUserDatasAsync();
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
                                ParentDiscussionThreadId = discussionThread.Id
                                ,
                                TimeStamp = DateTime.Now
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
                                ParentDiscussionThreadId = discussionThread.Id
                                ,
                                TimeStamp = DateTime.Now
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
            await LoadDiscussionThreads();
            await LoadComments();




        }
    }
}


