using Presenter;
using Model;

namespace Taskey
{
    internal class View : IView
    {
        internal View(Form form, IDisplay display) 
        {
            this._form = form;
            this._display = display;

            _display.Display();

            _display.OnAction += ProcessAction;
        }


        private readonly Form _form;
        private readonly IDisplay _display;

        public event EventHandler<TaskSettings> OnCreateTask;
        public event EventHandler<STask> OnDeleteTask;
        public event EventHandler<TaskSettings> OnUpdateTask;

        private void ProcessAction(object sender, ActionParameters action)
        {
            switch (action.actionType)
            {
                case ActionType.CreateTask:
                    OnCreateTask?.Invoke(this, action.taskSettings);
                    break;
                case ActionType.DeleteTask:
                    OnDeleteTask?.Invoke(this, new STask(action.taskSettings));
                    break;
                case ActionType.UpdateTask:
                    OnUpdateTask?.Invoke(this, action.taskSettings); 
                    break;
            }
        }

        public void Update(UpdateAfter updateAfter, TaskSettings task)
        {
            switch (updateAfter)
            {
                case UpdateAfter.TaskCreation: _display.AddItemToList(task);
                    break;
                case UpdateAfter.TaskDeletion: _display.RemoveItemFromList(task);
                    break;
            }
        }

        //ToDo: realize unsubscribe in IDispose 
        public void UnsubscribeEvents()
        {
            _display.OnAction -= ProcessAction;
            _display.UnsubscribeEvents();
        }
    }
}
