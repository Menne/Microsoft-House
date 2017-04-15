using System;
using System.ComponentModel.DataAnnotations;

namespace Backend
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }
	}
}
