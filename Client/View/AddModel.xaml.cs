//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//AddModel.xaml.cs - класс диалогового окна
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.Models;
using Client.ViewModels;

namespace Client.View
{
    /// <summary>
    /// Логика взаимодействия для AddModel.xaml
    /// </summary>
    public static class View
    {
        public static BusinessContext c = new BusinessContext();
        public static ViewModel vmodel = new ViewModel(c);
    }
    /// <summary>
    /// Класс диалогового окна
    /// </summary>
    public partial class AddModel : Window
    {
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public AddModel()
        {
            InitializeComponent();
            DataContext = View.vmodel;
        }
        /// <summary>
        /// Обработчик события, возникающего при редактировании строк DataGrid
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void GridParameterRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            (DataContext as ViewModel).AddNewDeviceAndParameter(e.Row.DataContext);
        }
        /// <summary>
        /// Обработчик события, возникающего при изменении содержимого ComboBox
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void EtoFIOSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            View.vmodel.SaveToUser();
        }
        /// <summary>
        /// Обработчик события, возникающего при нажатие кнопки "Ok"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            View.vmodel.IsButtonInDialogClick();
            View.vmodel.SaveToUser();
            //закрытие окна
            this.Close();
        }
    }
}

