using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Models
{
	public class TodoItem
	{
		public string Key { get; set; }
		public string Name { get; set; }
		public bool IsComplete { get; set; }
	}
}
