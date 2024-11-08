namespace ToDoList
{
	class Task
	{
		public string Description { get; set; }
		public Priority TaskPriority { get; set; }

		public Task(string description, Priority priority)
		{
			Description = description;
			TaskPriority = priority;
		}
	}
}