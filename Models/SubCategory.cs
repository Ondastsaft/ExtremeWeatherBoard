﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExtremeWeatherBoard.Interfaces;


namespace ExtremeWeatherBoard.Models
{
    public class SubCategory: ISideBarOption
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public virtual Category? ParentCategory { get; set; }

        public int SubCategoryAdminUserDataId { get; set; }
        [ForeignKey("SubCategoryAdminUserDataId")]

        public AdminUserData? SubCategoryAdminUserData { get; set; }
        public virtual ICollection<DiscussionThread>? Threads { get; set; }


    }
}
