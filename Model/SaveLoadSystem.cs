namespace Model
{
    internal class SaveLoadSystem
    {
        public SaveLoadSystem(ModelController controller, IDataService service) 
        {
            this.controller = controller;
            this.fileService = service;
        }

        private IDataService fileService;
        private ModelController controller;

        internal void Save()
        {
            var all = controller.GetAllTasks();

            if (fileService != null && all.Count > 0)
                fileService.Save(all);
            
        }

        internal void Load() 
        {
            var list = fileService.Load<STask>();

            if(list != null && list.Count > 0)
                controller.ChangeAllTasks(list);
        }
    }
}
