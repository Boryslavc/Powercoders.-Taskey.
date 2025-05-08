
namespace Model
{
    internal class AddOperation : IOperation
    {
        internal AddOperation(ModelController controller, TaskSettings settings) 
        {
            this.controller = controller;
            this.settings = settings;
        }

        private readonly ModelController controller;
        private TaskSettings settings;
        private STask task;

        public void Do()
        {
            task = new STask(settings);
            controller.AddTask(task);
        }

        public void Undo()
        {
            controller.RemoveTask(task);
        }
    }
}
