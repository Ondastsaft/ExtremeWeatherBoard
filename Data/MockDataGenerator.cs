namespace ExtremeWeatherBoard.Data
{
    using ExtremeWeatherBoard.DAL;
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

        ////private async Task LoadIdentityUsers()
        ////{
        ////    for (int i = 11; i <= 20; i++)
        ////    {
        ////        if (await _userManager.FindByNameAsync($"test{i}") == null)
        ////        {
        ////            var user = new IdentityUser
        ////            {
        ////                UserName = $"test{i}",
        ////                Email = $"test{i}@example.com",
        ////                EmailConfirmed = true,
        ////                SecurityStamp = Guid.NewGuid().ToString() // Set the SecurityStamp here
        ////            };
        ////            string password = $"Password{i}{i}{i}!";
        ////            await _userManager.CreateAsync(user, password);
        ////        }
        ////    }
        ////}
        //private async Task LoadUserDatas()
        //{
        //    var identityUsers = await _userManager.Users.ToListAsync();
        //    if (identityUsers != null && identityUsers.Count > 0)
        //    {
        //        var adminUser = identityUsers[0];
        //        await _adminService.PostAdminUserDataAsync(adminUser.Id);

        //        for (int i = 0; i <= identityUsers.Count; i++)
        //        {
        //            var user = identityUsers[i];
        //            await _userDataService.PostUserDataAsync(user.Id);
        //        }
        //    }
        //}
        //private async Task LoadCategories()
        //{
        //    var adminUser = _context.AdminUserDatas.FirstOrDefault();
        //    if (adminUser != null)
        //    {
        //        for (int i = 1; i < 5; i++)
        //        {
        //            Category category = new Category()
        //            {
        //                Title = $"Title Category {i}",
        //                TimeStamp = DateTime.Now,
        //                CreatorAdminUserData = adminUser
        //            };
        //            //await _context.Categories.AddAsync(category);
        //            //await _context.SaveChangesAsync();
        //            await _categoryApiService.PostCategoryAsync($"Title Category {i}", adminUser);
        //        }
        //    }
        //}
        //private async Task LoadSubCategories()
        //{
        //    var adminUser = await _context.AdminUserDatas.FirstOrDefaultAsync();
        //    var categories = await _categoryApiService.GetCategoriesAsync();
        //    if (categories != null && categories.Count > 0)
        //    {
        //        foreach (var savedCategory in categories)
        //        {
        //            var categorysSubcategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(savedCategory.Id);
        //            if (categorysSubcategories != null)
        //            {
        //                for (int i = 0; i < 5 - categorysSubcategories.Count; i++)
        //                {
        //                    SubCategory subCategory = new SubCategory()
        //                    {
        //                        ParentCategoryId = savedCategory.Id,
        //                        Title = $"Title SubCategory {i}",
        //                        TimeStamp = DateTime.Now,
        //                        CreatorAdminUserData = adminUser
        //                    };
        //                    await _context.SubCategories.AddAsync(subCategory);
        //                    await _context.SaveChangesAsync();
        //                }
        //            }
        //        }
        //    }
        //}
        //private async Task LoadDiscussionThreads()
        //{
        //    var subCategories = await _context.SubCategories.ToListAsync();
        //    bool threadsExist = await _context.DiscussionThreads.AnyAsync();
        //    if (subCategories is List<SubCategory> && !threadsExist)
        //    {
        //        var users = await _context.UserDatas.ToListAsync();
        //        if (users is List<UserData> && users.Count > 0)
        //        {
        //            int count = users.Count();
        //            int idCounter = 1;
        //            int userCounter = 1;
        //            foreach (var subCategory in subCategories)
        //            {
        //                for (int i = 0; i < 9; i++)
        //                {
        //                    var user = users.FirstOrDefault(u => u.Id == userCounter);
        //                    if (user is UserData)
        //                    {
        //                        DiscussionThread subThread = new DiscussionThread()
        //                        {
        //                            DiscussionThreadUserData = user
        //                            ,
        //                            SubCategory = subCategory
        //                            ,
        //                            Title = $"DiscussionThread id {i} of subcategory {subCategory.Id}"
        //                            ,
        //                            TimeStamp = DateTime.Now
        //                            ,
        //                            Text = LoremIpsum
        //                        };
        //                        await _context.DiscussionThreads.AddAsync(subThread);
        //                        await _context.SaveChangesAsync();
        //                        if (userCounter == count)
        //                        {
        //                            userCounter = 0;
        //                        }
        //                        userCounter++;
        //                        idCounter++;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //}
        //private async Task LoadComments()
        //{
        //    var users = await _context.UserDatas.ToListAsync();
        //    var discussionThreads = await _context.DiscussionThreads.ToListAsync();
        //    if (users != null && discussionThreads != null)
        //    {
        //        foreach (DiscussionThread discussionThread in discussionThreads)
        //        {
        //            for (int i = 0; i <= users.Count - 1; i++)
        //            {
        //                Comment newComment = new Comment()
        //                {
        //                    CommentUserData = users[i]
        //                    ,
        //                    ParentDiscussionThreadId = discussionThread.Id
        //                    ,
        //                    TimeStamp = DateTime.Now
        //                    ,
        //                    Title = $"UD {i}-DT {discussionThread.Id}-SC{discussionThread.SubCategoryId}"
        //                    ,
        //                    Text = LoremIpsum
        //                };
        //                await _context.Comments.AddAsync(newComment);
        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //    }
        //}

        //public async Task LoadMessages()
        //{
        //    var users = await _context.UserDatas.ToListAsync();
        //    if (users != null)
        //    {
        //        for (int i = 0; i < users.Count; i++)
        //        {
        //            for (int j = 0; j < users.Count; j++)
        //            {
        //                if (i != j)
        //                {
        //                    Message newMessage = new Message()
        //                    {
        //                        Receiver = users[j]
        //                        ,
        //                        Sender = users[i]
        //                        ,
        //                        Text = LoremIpsum
        //                        ,
        //                        TimeStamp = DateTime.Now
        //                        ,
        //                        Title = $"Message from {users[i].Id} to {users[j].Id}"
        //                        ,
        //                        Read = false
        //                    };
        //                    await _context.Messages.AddAsync(newMessage);
        //                    await _context.SaveChangesAsync();
        //                }
        //            }
        //        }
        //    }
        //}
        //public async Task MockLoadsOfDataAsync()
        //{
        //    ////await LoadIdentityUsers();
        //    ////await LoadUserDatas();
        //    ////await LoadCategories();
        //    ////await LoadSubCategories();
        //    ////await LoadDiscussionThreads();
        //    ////await LoadComments();
        //    ////await LoadMessages();
        //}
    }
}


