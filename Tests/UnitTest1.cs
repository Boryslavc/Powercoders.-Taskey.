using Model;

namespace Tests
{
    public class Tests
    {
        ModelController controller = new ModelController();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ModelSavesTasks()
        {
            //create task
            var settings = new TaskSettings("basic task name", "task description", false, new DateTime());
            controller.CreateTask(settings);

            //save it, clear the list, load from memory
            controller.SaveTasks();
            var expected = controller.GetAllTasks();
            controller.ClearList();
            controller.LoadTasks();
            var actual = controller.GetAllTasks();

            //assert
            Assert.AreEqual(expected[0].Settings.Name, actual[0].Settings.Name);
            Assert.AreEqual(expected[0].Settings.Description, actual[0].Settings.Description);
            Assert.AreEqual(expected[0].Settings.DueDate, actual[0].Settings.DueDate);
            Assert.AreEqual(expected[0].Settings.IsDone, actual[0].Settings.IsDone);
        }
    }
}