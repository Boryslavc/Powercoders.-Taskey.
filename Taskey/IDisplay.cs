using Model;

namespace Taskey
{
    internal interface IDisplay
    {
        internal event EventHandler<ActionParameters> OnAction;
        void Display();
        void AddItemToList(TaskSettings settings);
        void RemoveItemFromList(TaskSettings settings);
        void UnsubscribeEvents();
    }

    internal class ActionParameters
    {
        internal ActionType actionType;
        internal TaskSettings taskSettings;
    }

    public enum ActionType
    {
        CreateTask,
        DeleteTask,
        UpdateTask,
    }
}
