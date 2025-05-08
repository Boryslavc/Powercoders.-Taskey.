using Model;

namespace Presenter
{
    public enum UpdateAfter
    {
        TaskCreation,
        TaskDeletion,
    }

    public interface IView
    {
        public event EventHandler<TaskSettings> OnCreateTask;
        public event EventHandler<STask> OnDeleteTask;

        public void Update(UpdateAfter updateAfter, TaskSettings task);
        public void UnsubscribeEvents();
    }
}
