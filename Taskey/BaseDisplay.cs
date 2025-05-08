using System.Windows.Forms;
using Model;

namespace Taskey
{
    internal class BaseDisplay : IDisplay
    {
        internal BaseDisplay(Form form) 
        {
            this.form = form;
        }

        private event EventHandler<ActionParameters> onActionInternal;

        event EventHandler<ActionParameters> IDisplay.OnAction
        {
            add { onActionInternal += value; }
            remove { onActionInternal -= value; }
        }

        private readonly Form form;

        private GroupBox groupBox;
        private ListBox TaskListBox;

        private FlowLayoutPanel controlPanel;
        private Button CreateButton;
        private Button DeleteButton;


        private Panel detailPanel;
        private TextBox TaskNameTextBox;
        private TextBox TaskDescriptionTextBox;

        private Label DueDateLabel;
        private DateTimePicker DueDatePicker;


        public void Display()
        {
            var screenBounds = Screen.PrimaryScreen.Bounds;

            // Set form size to, say, 80% of screen width and height
            form.Width = screenBounds.Width;
            form.Height = screenBounds.Height;

            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Taskey";

            
            
            ControlPanelCreate();
            GroupBoxCreate();
            DetailedPanelCreate();

           

            // Add panels to form
            form.Controls.Add(detailPanel);
            form.Controls.Add(groupBox);
            form.Controls.Add(controlPanel);
        }

        private void ControlPanelCreate()
        {
            // Control panel at top
            controlPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = (int)(form.ClientSize.Height * 0.1),
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5),
                AutoSize = false,
                BackColor = Color.LightGray
            };

            CreateButton = new Button
            {
                Text = "Create",
                Width = controlPanel.Height / 2,
                Height = controlPanel.Height / 2,
                AutoSize = true,
                BackColor = Color.White
            };
            DeleteButton = new Button
            {
                Text = "Delete",
                Width = controlPanel.Height / 2,
                Height = controlPanel.Height / 2,
                AutoSize = true,
                BackColor = Color.White
            };

            controlPanel.Controls.Add(CreateButton);
            controlPanel.Controls.Add(DeleteButton);

            CreateButton.Click += (x,y) => 
            {
                var parameters = new ActionParameters();
                parameters.actionType = ActionType.CreateTask;
                parameters.taskSettings = new TaskSettings(TaskNameTextBox.Text,TaskDescriptionTextBox.Text, false, DueDatePicker.Value);
                onActionInternal?.Invoke(this, parameters);
            };

            DeleteButton.Click += (x, y) => 
            {
                var parameters = new ActionParameters();
                parameters.actionType = ActionType.DeleteTask;
                parameters.taskSettings = TaskListBox.SelectedItem as TaskSettings;
                if (parameters.taskSettings != null)
                    onActionInternal?.Invoke(this, parameters);
            };
        }

        private void GroupBoxCreate() 
        {
            TaskListBox = new ListBox
            {
                Dock = DockStyle.Fill,
            };

            TaskListBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;

            groupBox = new GroupBox
            {
                Text = "Tasks",  
                Dock = DockStyle.Left,
                Width = (int)(form.Height * 0.2)
            };

            groupBox.Controls.Add(TaskListBox);
        }

        private void ListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskSettings selectedTask)
            {
                itemSelected = true;
                TaskNameTextBox.Text = selectedTask.Name;
                TaskDescriptionTextBox.Text = selectedTask.Description;
                DueDatePicker.Value = selectedTask.DueDate;
            }
            else
                itemSelected = false;
        }

        private void DetailedPanelCreate()
        {
            // Detail panel in the center/right
            detailPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            var nameLabel = new Label { Text = "Name:", Top = 10, Left = 10, AutoSize = true };
            TaskNameTextBox = new TextBox { Top = 30, Left = 10, Width = 300 };

            var descriptionLabel = new Label { Text = "Description:", Top = 70, Left = 10, AutoSize = true };
            TaskDescriptionTextBox = new TextBox
            {
                Top = 90,
                Left = 10,
                Width = 300,
                Height = 100,
                Multiline = true
            };

            TaskNameTextBox.Margin = new Padding(0, 5, 0, 10);
            TaskDescriptionTextBox.Margin = new Padding(0, 5, 0, 10);

            // Due date picker
            DueDateLabel = new Label();
            DueDateLabel.Text = "Due Date:";
            DueDateLabel.Top = 200; // adjust this as needed
            DueDateLabel.Left = 10;
            DueDateLabel.AutoSize = true;
            DueDateLabel.Margin = new Padding(0, 5, 0, 0);

            DueDatePicker = new DateTimePicker();
            DueDatePicker.Format = DateTimePickerFormat.Short;
            DueDatePicker.Top = 220; // slightly below the label
            DueDatePicker.Left = 10;
            DueDatePicker.Margin = new Padding(0, 0, 0, 10);



            detailPanel.Controls.Add(nameLabel);
            detailPanel.Controls.Add(TaskNameTextBox);
            detailPanel.Controls.Add(descriptionLabel);
            detailPanel.Controls.Add(TaskDescriptionTextBox);
            detailPanel.Controls.Add(DueDateLabel);
            detailPanel.Controls.Add(DueDatePicker);
        }

        public void AddItemToList(TaskSettings task)
        {
            TaskListBox.Items.Add(task);
            ClearForm();
        }

        public void RemoveItemFromList(TaskSettings settings) => 
            TaskListBox.Items.Remove(settings);
        
        private void ClearForm()
        {
            TaskNameTextBox.Text = string.Empty;
            TaskDescriptionTextBox.Text = string.Empty;
            DueDatePicker.Value = DateTime.Today;
        }

        //ToDo: realize unsubscribe in IDispose 
        public void UnsubscribeEvents()
        {
            TaskNameTextBox.TextChanged -= OnTaskFieldChanged;
            TaskDescriptionTextBox.TextChanged -= OnTaskFieldChanged;

            CreateButton.Click -= (x, y) =>
            {
                var parameters = new ActionParameters();
                parameters.actionType = ActionType.CreateTask;
                parameters.taskSettings = new TaskSettings(TaskNameTextBox.Text, TaskDescriptionTextBox.Text, false, DueDatePicker.Value);
                onActionInternal?.Invoke(this, parameters);
            };

            DeleteButton.Click -= (x, y) =>
            {
                var parameters = new ActionParameters();
                parameters.actionType = ActionType.DeleteTask;
                parameters.taskSettings = TaskListBox.SelectedItem as TaskSettings;
                if (parameters.taskSettings != null)
                    onActionInternal?.Invoke(this, parameters);
            };

            TaskListBox.SelectedIndexChanged -= ListBox_SelectedIndexChanged;
        }
    }
}
