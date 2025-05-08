
namespace Model
{
    internal class DeleteOperation : IOperation
    {
        public DeleteOperation(ModelController controller, STask task)
        {
            this.controller = controller;
            this.task = task;
        }

        private readonly ModelController controller;
        private readonly STask task;
        
        public void Do()
        {

        }

        public void Undo()
        {

        }
    }
}
