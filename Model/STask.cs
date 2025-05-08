namespace Model
{
    public class STask
    {
        public STask() { } // needed for json converter

        public STask(TaskSettings settings)
        {
            Settings = settings;
        }
        public TaskSettings Settings { get; set; }
    }
}
