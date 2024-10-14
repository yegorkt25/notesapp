﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class Note
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("title")]
		public string Title { get; set; }

		[Column("text")]
		public string Text { get; set; }

		[Column("userid")]
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
