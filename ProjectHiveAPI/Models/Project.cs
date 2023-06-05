﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHiveAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column("start_date")]
        public DateTime? StartDate { get; set; }
        [Column("end_date")]
        public DateTime? EndDate { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

    }
}
