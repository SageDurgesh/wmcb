﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wmcb.model.Data
{
    public class Match
    {
        [Key]//, ForeignKey("Schedule")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Description { get; set; }
        public int? TeamWonToss { get; set; }
        public int? TeamBattedFirst { get; set; }
        public int? TeamWon { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public bool? IsReviewed { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int? HomeSubmittedBy { get; set; }
        public int? AwaySubmittedBy { get; set; }
        public int? ReviewerID { get; set; }
        public string ReviewerNotes { get; set; }

      //  public virtual Schedule Schedule { get; set; }
    }
}
