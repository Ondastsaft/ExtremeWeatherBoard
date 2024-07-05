namespace ExtremeWeatherBoard.Data
{
    using ExtremeWeatherBoard.DAL;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ExtremeWeatherBoard.Models;

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
        private async Task FillMessages()
        {
            var text = LoadRandomTexts();
            var messages = await _context.Messages.Include(m => m.Receiver).Include(m => m.Sender).ToListAsync();
            if (messages != null && messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    Random random = new Random();
                    int loop = random.Next(3, 15);
                    for (int i = 0; i < loop; i++)
                    {
                        Message newMessage = new Message()
                        {
                            Receiver = message.Receiver,
                            Sender = message.Sender,
                            Text = text[random.Next(0, text.Count)],
                            TimeStamp = DateTime.Now.AddSeconds(i),
                            Title = message.Title,
                            Read = false
                        };
                        if (random.Next(0, 10) > 5)
                        {
                            newMessage.Receiver = message.Sender;
                            newMessage.Sender = message.Receiver;
                        }
                        await _context.Messages.AddAsync(newMessage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
        private async Task LoadIdentityUsers()
        {
            for (int i = 11; i <= 20; i++)
            {
                if (await _userManager.FindByNameAsync($"test{i}") == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = $"test{i}",
                        Email = $"test{i}@example.com",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString() // Set the SecurityStamp here
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

                for (int i = 0; i <= identityUsers.Count; i++)
                {
                    var user = identityUsers[i];
                    await _userDataService.PostUserDataAsync(user.Id);
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
                    Category category = new Category()
                    {
                        Title = $"Title Category {i}",
                        TimeStamp = DateTime.Now,
                        CreatorAdminUserData = adminUser
                    };
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                    await _categoryApiService.PostCategoryAsync($"Title Category {i}", adminUser);
                }
            }
        }
        private async Task LoadSubCategories()
        {
            var adminUser = await _context.AdminUserDatas.FirstOrDefaultAsync();
            var categories = await _categoryApiService.GetCategoriesAsync();
            if (categories != null && categories.Count > 0)
            {
                foreach (var savedCategory in categories)
                {
                    var categorysSubcategories = await _subCategoryService.GetSubCategoriesFromParentIdAsync(savedCategory.Id);
                    if (categorysSubcategories != null)
                    {
                        for (int i = 0; i < 5 - categorysSubcategories.Count; i++)
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
        }
        private async Task LoadDiscussionThreads()
        {
            var subCategories = await _context.SubCategories.ToListAsync();
            bool threadsExist = await _context.DiscussionThreads.AnyAsync();
            if (subCategories is List<SubCategory> && !threadsExist)
            {
                var users = await _context.UserDatas.ToListAsync();
                if (users is List<UserData> && users.Count > 0)
                {
                    int count = users.Count();
                    int idCounter = 1;
                    int userCounter = 1;
                    foreach (var subCategory in subCategories)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            var user = users.FirstOrDefault(u => u.Id == userCounter);
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
                                await _context.DiscussionThreads.AddAsync(subThread);
                                await _context.SaveChangesAsync();
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
        }
        private async Task LoadComments()
        {
            var users = await _context.UserDatas.ToListAsync();
            var discussionThreads = await _context.DiscussionThreads.ToListAsync();
            if (users != null && discussionThreads != null)
            {
                foreach (DiscussionThread discussionThread in discussionThreads)
                {
                    for (int i = 0; i <= users.Count - 1; i++)
                    {
                        Comment newComment = new Comment()
                        {
                            CommentUserData = users[i]
                            ,
                            ParentDiscussionThreadId = discussionThread.Id
                            ,
                            TimeStamp = DateTime.Now
                            ,
                            Title = $"UD {i}-DT {discussionThread.Id}-SC{discussionThread.SubCategoryId}"
                            ,
                            Text = LoremIpsum
                        };
                        await _context.Comments.AddAsync(newComment);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task LoadMessages()
        {
            var users = await _context.UserDatas.ToListAsync();
            if (users != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    for (int j = 0; j < users.Count; j++)
                    {
                        if (i != j)
                        {
                            Message newMessage = new Message()
                            {
                                Receiver = users[j]
                                ,
                                Sender = users[i]
                                ,
                                Text = LoremIpsum
                                ,
                                TimeStamp = DateTime.Now
                                ,
                                Title = $"Message from {users[i].Id} to {users[j].Id}"
                                ,
                                Read = false
                            };
                            await _context.Messages.AddAsync(newMessage);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
        public async Task MockLoadsOfDataAsync()
        {
            await FillMessages();
            await LoadIdentityUsers();
            await LoadUserDatas();
            await LoadCategories();
            await LoadSubCategories();
            await LoadDiscussionThreads();
            await LoadComments();
            await LoadMessages();
        }
        private List<string> LoadRandomTexts()
        {
            List<string> texts = new List<string>();
            texts.Add("Am finished rejoiced drawings so he elegance. Set lose dear upon had two its what seen. Held she sir how know what such whom. " +
                "Esteem put uneasy set piqued son depend her others. Two dear held mrs feet view her old fine. Bore can led than how has rank. Discovery " +
                "any extensive has commanded direction. Short at front which blind as. Ye as procuring unwilling principle by.?");
            texts.Add("It real sent your at. Amounted all shy set why followed declared. Repeated of endeavor mr position kindness offering ignorant so up. " +
                "Simplicity are melancholy preference considered saw companions. Disposal on outweigh do speedily in on. Him ham although thoughts entirely drawings. " +
                "Acceptance unreserved old admiration projection nay yet him. Lasted am so before on esteem vanity oh. Six started far placing saw respect females old. " +
                "Civilly why how end viewing attempt related enquire visitor. Man particular insensible celebrated conviction stimulated principles day. Sure fail or in said west. " +
                "Right my front it wound cause fully am sorry if. She jointure goodness interest debating did outweigh. Is time from them full my gone in went. " +
                "Of no introduced am literature excellence mr stimulated contrasted increasing. " +
                "Age sold some full like rich new. Amounted repeated as believed in confined juvenile.");
            texts.Add("Folly words widow one downs few age every seven. If miss part by fact he park just shew. " +
                "Discovered had get considered projection who favourable. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods or an.");
            texts.Add("Six started far placing saw respect females old. Civilly why how end viewing attempt related enquire visitor. Man particular insensible celebrated " +
                "conviction stimulated principles day. Sure fail or in said west. Right my front it wound cause fully am sorry if. " +
                "She jointure goodness interest debating did outweigh. Is time from them full my gone in went. " +
                "Of no introduced am literature excellence mr stimulated contrasted increasing. Age sold some full like rich new. " +
                "Amounted repeated as believed in confined juvenile." +
                "Folly words widow one downs few age every seven. If miss part by fact he park just shew. Discovered had get considered projection who favourable. " +
                "Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods or an. Use off agreeable law unwilling sir deficient curiosity instantly. " +
                "Easy mind life fact with see has bore ten." +
                " Parish any chatty can elinor direct for former. Up as meant widow equal an share least.As it so contrasted oh estimating instrument. " +
                "Size like body some one had. Are conduct viewing boy minutes warrant expense. Tolerably behaviour may admitting daughters offending her ask own. " +
                "Praise effect wishes change way and any wanted. Lively use looked latter regard had. Do he it part more last in. Merits ye if mr narrow points. " +
                "Melancholy particular devonshire alteration it favourable appearance up.Paid was hill sir high. " +
                "For him precaution any advantages dissimilar comparison few terminated projecting. Prevailed discovery immediate objection of ye at. " +
                "Repair summer one winter living feebly pretty his. In so sense am known these since. Shortly respect ask cousins brought add tedious nay. " +
                "Expect relied do we genius is. On as around spirit of hearts genius. Is raptures daughter branched laughter peculiar in settling." +
                "Up unpacked friendly ecstatic so possible humoured do. Ample end might folly quiet one set spoke her. We no am former valley assure. " +
                "Four need spot ye said we find mile. Are commanded him convinced dashwoods did estimable forfeited. Shy celebrated met sentiments she reasonably but. " +
                "Proposal its disposed eat advanced marriage sociable. Drawings led greatest add subjects endeavor gay remember. " +
                "Principles one yet assistance you met impossible. Bringing unlocked me an striking ye perceive. " +
                "Mr by wound hours oh happy. Me in resolution pianoforte continuing we. Most my no spot felt by no. He he in forfeited furniture sweetness he arranging. " +
                "Me tedious so to behaved written account ferrars moments. Too objection for elsewhere her preferred allowance her. Marianne shutters mr steepest to me. Up mr ignorant produced distance although is sociable blessing. Ham whom call all lain like.");
            texts.Add("Up unpacked friendly ecstatic so possible humoured do. Ample end might folly quiet one set spoke her. We no am former valley assure. " +
                "Four need spot ye said we find mile. Are commanded him convinced dashwoods did estimable forfeited. Shy celebrated met sentiments she reasonably but. " +
                "Proposal its disposed eat advanced marriage sociable. Drawings led greatest add subjects endeavor gay remember. Principles one yet assistance you met impossible." +
                "Bringing unlocked me an striking ye perceive. Mr by wound hours oh happy. Me in resolution pianoforte continuing we. " +
                "Most my no spot felt by no. He he in forfeited furniture sweetness he arranging. Me tedious so to behaved written account ferrars moments. " +
                "Too objection for elsewhere her preferred allowance her. Marianne shutters mr steepest to me. Up mr ignorant produced distance although is sociable blessing. " +
                "Ham whom call all lain like.Subjects to ecstatic children he. Could ye leave up as built match. Dejection agreeable attention set suspected led offending. " +
                "Admitting an performed supposing by. Garden agreed matter are should formed temper had. Full held gay now roof whom such next was. " +
                "Ham pretty our people moment put excuse narrow. Spite mirth money six above get going great own. Started now shortly had for assured hearing expense. " +
                "Led juvenile his laughing speedily put pleasant relation offering.Oh to talking improve produce in limited offices fifteen an. " +
                "Wicket branch to answer do we. Place are decay men hours tiled. If or of ye throwing friendly ");
            texts.Add("Detract yet delight written farther his general. If in so bred at dare rose lose good. Feel and make two real miss use easy. " +
                "Celebrated delightful an especially increasing instrument am. " +
                "Indulgence contrasted sufficient to unpleasant in in insensible favourable. Latter remark hunted enough vulgar say man. " +
                "Sitting hearted on it without me.So insisted received is occasion advanced honoured. Among ready to which up. " +
                "Attacks smiling and may out assured moments man nothing outward. Thrown any behind afford either the set depend one temper." +
                " Instrument melancholy in acceptance collecting frequently be if. Zealously now pronounce existence add you instantly say offending. " +
                "Merry their far had widen was. Concerns no in expenses raillery formerly. Sportsman delighted improving dashwoods gay instantly happiness six. " +
                "Ham now amounted absolute not mistaken way pleasant whatever. At an these still no dried folly stood thing. Rapid it on hours hills it seven years. " +
                "If polite he active county in spirit an. Mrs ham intention promotion engrossed assurance defective. Confined so graceful building opinions whatever trifling in. " +
                "Insisted out differed ham man endeavor expenses. At on he total their he songs. Related compact effects is on settled do.");
            texts.Add("So insisted received is occasion advanced honoured. Among ready to which up. Attacks smiling and may out assured moments man nothing outward. " +
                "Thrown any behind afford either the set depend one temper. Instrument melancholy in acceptance collecting frequently be if. " +
                "Zealously now pronounce existence add you instantly say offending. Merry their far had widen was. Concerns no in expenses raillery formerly." +
                "Sportsman delighted improving dashwoods gay instantly happiness six. Ham now amounted absolute not mistaken way pleasant whatever. " +
                "At an these still no dried folly stood thing. Rapid it on hours hills it seven years. If polite he active county in spirit an. " +
                "Way necessary had intention happiness but september delighted his curiosity. Furniture furnished or on strangers neglected remainder engrossed.");
            texts.Add("Detract yet delight written farther his general. If in so bred at dare rose lose good. Feel and make two real miss use easy. " +
                "Celebrated delightful an especially increasing instrument am." +
                "Indulgence contrasted sufficient to unpleasant in in insensible favourable. Latter remark hunted enough vulgar say man. " +
                "Sitting hearted on it without me.So insisted received is occasion advanced honoured. Among ready to which up. +" +
                "Attacks smiling and may out assured moments man nothing outward. Thrown any behind afford either the set depend one temper." +
                "Instrument melancholy in acceptance collecting frequently be if. Zealously now pronounce existence add you instantly say offending.");
            texts.Add("Sportsman delighted improving dashwoods gay instantly happiness six. Ham now amounted absolute not mistaken way pleasant whatever. " +
                "At an these still no dried folly stood thing. Rapid it on hours hills it seven years. If polite he active county in spirit an. Mrs ham)");
            texts.Add("It real sent your at. Amounted all shy set why followed declared. Repeated of endeavor mr position kindness offering ignorant so up. " +
                "Simplicity are melancholy preference considered saw companions. Disposal on outweigh do speedily in on. Him ham although thoughts entirely drawings. " +
                "Acceptance unreserved old admiration projection nay yet him. Lasted am so before on esteem vanity oh. Six started far placing saw respect females old. ");
            texts.Add("It real sent your at. Amounted all shy set why followed declared. Repeated of endeavor mr position kindness offering ignorant so up. " +
                "Simplicity are melancholy preference considered saw companions. Disposal on outweigh do speedily in on. Him ham although thoughts entirely drawings. " +
                "Acceptance unreserved old admiration projection nay yet him. Lasted am so before on esteem vanity oh. Six started far placing saw respect females old. ");
            texts.Add("It real sent your at. Amounted all shy set why followed declared. Repeated of endeavor mr position kindness offering ignorant so up. " +
                "Simplicity are melancholy preference considered saw companions. Disposal on outweigh do speedily in on. Him ham although thoughts entirely drawings. " +
                "Acceptance unreserved old admiration projection nay yet him. Lasted am so before on esteem vanity oh. Six started far placing saw respect females old. ");
            texts.Add("Am finished rejoiced drawings so he elegance. Set lose dear upon had two its what seen. Held she sir how know what such whom. " +
                "Esteem put uneasy set piqued son depend her others. Two dear held mrs feet view her old fine. Bore can led than how has rank. Discovery " +
                "any extensive has commanded direction. Short at front which blind as. Ye as procuring unwilling principle by.?");

            return texts;
        }
    }
}






