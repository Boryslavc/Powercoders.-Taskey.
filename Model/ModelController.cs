namespace Model
{
    public class ModelController
    {
        public ModelController() 
        {
            tasks = new List<STask>();
            operations = new Stack<IOperation>();
            ResolveDependencies();
        }
        private void ResolveDependencies()
        {
            ISerializer serializer = new JSONSerializer();
            IDataService service = new FileService(serializer);

            saveLoadSystem = new SaveLoadSystem(this, service);
        }

        private List<STask> tasks;
        private Stack<IOperation> operations;
        private SaveLoadSystem saveLoadSystem;

        internal void AddTask(STask task) => tasks.Add(task);
        internal void RemoveTask(STask task) => tasks.Remove(task);
        internal void PushOperation(IOperation operation) => operations.Push(operation);
        internal void PopOperation() => operations.Pop();
        public List<STask> GetAllTasks() 
        {
            var res = new List<STask>();
            foreach (var task in tasks)
                res.Add(task);
            
            return res;
        }  
        public void ClearList()
        {
            tasks.Clear();
        }
        internal void ChangeAllTasks(List<STask> tasks) => this.tasks = tasks;

        public void SaveTasks()
        {
            saveLoadSystem.Save();
        }
        public void LoadTasks()
        {
            saveLoadSystem.Load();
        }



        public void CreateTask(TaskSettings taskSettings)
        {
            var cr = new AddOperation(this, taskSettings);
            cr.Do();
            operations.Push(cr);
        }

        public void DeleteTask(STask task)
        {
            var del = new DeleteOperation(this, task);
            del.Do();
            operations.Push(del);
        }


        
    }
}
