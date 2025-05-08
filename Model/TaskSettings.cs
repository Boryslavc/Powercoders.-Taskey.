namespace Model
{
    public class TaskSettings
    {
        public TaskSettings() { } // needed for json converter

        public TaskSettings(string name, string description, bool isDone, DateTime dueDate)
        {
            Name = name;
            Description = description;
            IsDone = isDone;
            DueDate = dueDate;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime DueDate { get; set; }

        // Helps to determine, how tasks will be displayed in a list
        public override string ToString()
        {
            return Name;
        }
    }
}
