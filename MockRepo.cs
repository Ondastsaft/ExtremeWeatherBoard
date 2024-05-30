using ExtremeWeatherBoard.Models;

namespace ExtremeWeatherBoard
{
    public class MockRepo
    {
        public List<UserData> UserDatas { get; set; }
        public List<AdminUserData> AdminUserDatas { get; set; }
        public List<AdminLog> AdminLogs { get; set; }
        public List<Message> Messages { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Comment> Comments { get; set; }
        public List<DiscussionThread> DiscussionThreads { get; set; }
        public MockRepo()
        {
            //UserData_____________________________________________________________________________________________________________________________________________________ 
            UserDatas = new List<UserData>();
            UserDatas.Add(new UserData
            {
                Id = 1,
                MockUserId = "11",
                ImageURL = "https://via.placeholder.com/150",
                MockUser = new MockIdentityUser() { Id = "11", Email = "one@one.com", Name = "One", Password = "1OneOne@" },
                Comments = new List<Comment>(),
                ReceivedMessages = new List<Message>(),
                SentMessages = new List<Message>(),
                Threads = new List<DiscussionThread>()
            });
            UserDatas.Add(new UserData
            {
                Id = 2,
                MockUserId = "22",
                ImageURL = "https://via.placeholder.com/150",
                MockUser = new MockIdentityUser() { Id = "22", Email = "two@two.com", Name = "two", Password = "2TwoTwo@" },
                Comments = new List<Comment>(),
                ReceivedMessages = new List<Message>(),
                SentMessages = new List<Message>(),
                Threads = new List<DiscussionThread>()
            });
            UserDatas.Add(new UserData
            {
                Id = 3,
                MockUserId = "33",
                ImageURL = "https://via.placeholder.com/150",
                MockUser = new MockIdentityUser() { Id = "33", Email = "three@three.com", Name = "Three", Password = "3ThreeThree@" },
                Comments = new List<Comment>(),
                ReceivedMessages = new List<Message>(),
                SentMessages = new List<Message>(),
            });


            //AdminUserData_____________________________________________________________________________________________________________________________________________________    
            AdminUserDatas = new List<AdminUserData>();
            AdminUserDatas.Add(new AdminUserData
            {
                Id = 4,
                MockUserId = "44",
                ImageURL = "https://via.placeholder.com/150",
                MockUser = new MockIdentityUser() { Id = "44", Email = "four@four.com", Name = "Four", Password = "4FourFour@" },
                Comments = new List<Comment>(),
                ReceivedMessages = new List<Message>(),
                SentMessages = new List<Message>(),
                Categories = new List<Category>(),
                SubCategories = new List<SubCategory>(),
                AdminLogs = new List<AdminLog>(),
                Threads = new List<DiscussionThread>()

            });


            //Messages_____________________________________________________________________________________________________________________________________________________ 
            Messages = new List<Message>();
            Messages.Add(new Message
            {
                Id = 1,
                SentAt = new DateTime(2022, 1, 1, 12, 0, 0),
                Topic = "Message 1 from 1 to 2 at 20220101 12:00",
                Text = "Hello this is message 1 sent at at 20220101 12:00 from userdata id 1 to userdata id 2",
                SenderId = 1,
                ReceiverId = 2,
            });
            Messages.Add(new Message
            {
                Id = 2,
                SentAt = new DateTime(2022, 1, 1, 13, 0, 0),
                Topic = "Message 2 from 2 to 1 at 20220101 13:00",
                Text = "Hello this is message 2 sent at at 20220101 13:00 from userdata id 2 to userdata id 1",
                SenderId = 2,
                ReceiverId = 1,
            });
            Messages.Add(new Message
            {
                Id = 3,
                SentAt = new DateTime(2022, 1, 1, 14, 0, 0),
                Topic = "Message 3 from 1 to 3 at 20220101 14:00",
                Text = "Hello this is message 3 sent at at 20220101 14:00 from userdata id 1 to userdata id 3",
                SenderId = 1,
                ReceiverId = 3,
            });
            Messages.Add(new Message
            {
                Id = 4,
                SentAt = new DateTime(2022, 1, 1, 15, 0, 0),
                Topic = "Message 4 from 3 to 1 at 20220101 15:00",
                Text = "Hello this is message 4 sent at at 20220101 15:00 from userdata id 3 to userdata id 1",
                SenderId = 3,
                ReceiverId = 1,
            });
            Messages.Add(new Message
            {
                Id = 5,
                SentAt = new DateTime(2022, 1, 1, 16, 0, 0),
                Topic = "Message 5 from 2 to 3 at 20220101 16:00",
                Text = "Hello this is message 5 sent at at 20220101 16:00 from userdata id 2 to userdata id 3",
                SenderId = 2,
                ReceiverId = 3,
            });


            //Categories_____________________________________________________________________________________________________________________________________________________
            Categories = new List<Category>();
            for(int i = 0; i <= 2; i++)
            {
                Categories.Add(new Category
                {
                    Id = i + 1,
                    CreatedDate = new DateTime(2021, 1, i + 1, 12, 0, 0),
                    CreatorId = 4,
                    Name = "Category " + (i + 1),
                    SubCategories = new List<SubCategory>()
                });
            }

            //SubCategories_____________________________________________________________________________________________________________________________________________________    
            SubCategories = new List<SubCategory>();

            for (int i = 0; i <=Categories.Count-1;i++)
            {
                for (int j = 0; j <= Categories.Count - 1; j++)
                {
                    SubCategories.Add(new SubCategory
                    {
                        Id = i + 1,
                        Title = "SubCategory " + i+1,
                        CreatedAt = new DateTime(2021, 1, i + 1),
                        ParentCategoryId = i + 1,                        
                    });
                    Categories[i].SubCategories.Add(SubCategories.Last());
                }
            }

            //Threads_____________________________________________________________________________________________________________________________________________________  
            DiscussionThreads = new List<DiscussionThread>();
            for(int i = 0; i <= Categories.Count; i++)
            {
                for(int j = 0; j <= Categories[i].SubCategories.Count; j++)
                {
                    DiscussionThreads.Add(new DiscussionThread
                    {
                        Id = i + 1,
                        Title = "Thread "+ + (j + 1) +"in subcategory " + i+1,
                        Text = "Thread " + (i + 1) + " Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                        "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, " +
                        "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                        "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                        CreatedAt = new DateTime(2021, 1, i + 1, 12, 0, 0),
                        SubCategoryId = i + 1,
                        CreatorUserId = i + 1,
                        Comments = new List<Comment>(),
                    });
                    SubCategories[i].Threads.Add(DiscussionThreads.Last());
                }
            }


            //Comments_____________________________________________________________________________________________________________________________________________________
            Comments = new List<Comment>();
            for (int i = 0; i <= Categories.Count; i++)
            {
                var subCategoriesList = Categories[i].SubCategories.ToList();
                for (int j = 0; j <= Categories[i].SubCategories.Count; j++)
                {
                    var discussionThreadsList = subCategoriesList[j].Threads.ToList();
                    for (int k = 0; k <= discussionThreadsList.Count; k++)
                    { }
                    Comments.Add(new Comment
                    {
                        Id = i + 1,
                        Text = "Comment " + (i + 1) + " Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                        "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, " +
                        "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                        "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                        PostedAt = new DateTime(2021, 1, i + 1, 12, 0, 0),
                        CommentUserDataId = i + 1,
                        CommentThread = DiscussionThreads[i],
                    });
                }
            }   
        }
    }
}

