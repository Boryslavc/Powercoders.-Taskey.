using Model;

namespace Presenter
{
    public class PresenterController
    {
        public PresenterController(IView view) 
        {
            this.view = view;
            model = new ModelController();
            model.LoadTasks();
            InitializeEvents();
        }

        private readonly IView view;
        private readonly ModelController model;

        private void InitializeEvents()
        {
            view.OnCreateTask += NewTaskAdded;
            view.OnDeleteTask += DeleteTask;
        }

        private void NewTaskAdded(object? sender, TaskSettings arg)
        {
            model.CreateTask(arg);
            view.Update(UpdateAfter.TaskCreation, arg);
        }

        private void DeleteTask(object? sender, STask task)
        {
            model.DeleteTask(task);
            view.Update(UpdateAfter.TaskDeletion, task.Settings);
        }

        //ToDo: realize unsubscribe in IDispose 
        public void OnApplicationQuit()
        {
            model.SaveTasks();
            view.OnCreateTask -= NewTaskAdded;
            view.OnDeleteTask -= DeleteTask;
        }
    }
}