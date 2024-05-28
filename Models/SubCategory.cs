﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ExtremeWeatherBoard.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]  
        public string? Title { get; set; }      
        [Required]
        public DateTime CreatedAt { get; set; }
        public int? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }

        public int? CreatorId { get; set; }
        [ForeignKey("CreatorId")]

        public AdminUserData? Creator { get; set; }
        public virtual ICollection<Thread>? Threads { get;}


    }
}
