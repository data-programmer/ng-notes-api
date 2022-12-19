﻿using System;
namespace NGNotesAPI.Models
{
    public class NoteModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
