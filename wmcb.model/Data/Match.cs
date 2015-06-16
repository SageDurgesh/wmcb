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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Description { get; set; }
        public int? TeamWon { get; set; }       
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
		public bool IsReviewed { get; set; }
		
        public virtual Tournament Tournament { get; set; }
        [ForeignKey("Tournament")]
        public int? TournamentId { get; set; }

        public virtual Division Division { get; set; }
        [ForeignKey("Division")]
        public int? DivisionId { get; set; }
		
		public int? SubmittedBy { get; set; }
        public int? ReviewerID { get; set; }
        public string ReviewerNotes { get; set; }
    }
}
