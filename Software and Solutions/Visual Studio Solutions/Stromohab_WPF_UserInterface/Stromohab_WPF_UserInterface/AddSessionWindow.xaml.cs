using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Stromohab_DataAccessLayer;


namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddSessionWindow : Window
    {
        public AddSessionWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //using (TasksTableAdapter tasksTableAdapter = new TasksTableAdapter())
            //{
            //    comboBoxTaskList.ItemsSource = tasksTableAdapter.GetTasks();
            //}
        }

        private void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            //using (Sessions_has_tasksTableAdapter sessionsHasTasksTableAdapter = new Sessions_has_tasksTableAdapter())
            //{
            //    using (TasksTableAdapter tasksTableAdapter = new TasksTableAdapter())
            //    {
            //        sessionsHasTasksTableAdapter.Insert(0, tasksTableAdapter.GetIDByName(comboBoxTaskList.Text)[0].idTask,
            //                                            0);
            //    }
            //}
        }

    }
}
