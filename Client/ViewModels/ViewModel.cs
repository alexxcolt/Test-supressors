//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//ViewModel.cs - класс модели-представления
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using Client.Models;
using OxyPlot.Axes;
using OxyPlot;
using OxyPlot.Series;
using System.Threading;
using Client.View;
using System.Net;
using System.Net.Sockets;
using System.Windows.Media;
using System.Diagnostics;


namespace Client.ViewModels
{
    /// <summary>
    /// Класс модели-представления необходимый для связи представления с моделью
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Массив, содержащий данные переданные с сервера
        /// </summary>
        static public double[] masVoltageFromServer = new double[2000];
        /// <summary>
        /// Счётчик количества измерений
        /// </summary>
        static public int CounterAmountMeasurement = 0;
        /// <summary>
        /// Счётчик количества измерений для элемента ComboBox
        /// </summary>
        static public int CounterForCombo = 0;
        /// <summary>
        /// Напряжение, выводимое при выборе номера измерения
        /// </summary>
        static public double UForCombo;
        /// <summary>
        /// Свойство, которое связано с выбранным устройством в ComboBox (свойство SelectedItem)
        /// </summary>
        public object selectedDeviceInComboBox;
        public object SelectedDeviceInComboBox
        {
            get { return selectedDeviceInComboBox;}

            set { selectedDeviceInComboBox = value; OnPropertyChanged("SelectedDeviceInComboBox"); }
        }
        public object selectedNumberOfMeasureInComboBox;
        public object SelectedNumberOfMeasureInComboBox
        {
            get { return selectedNumberOfMeasureInComboBox; }

            set { selectedNumberOfMeasureInComboBox = value; OnPropertyChanged("SelectedNumberOfMeasureInComboBox"); }
        }
        /// <summary>
        /// Свойсво, которое связано со свойством IsChecked RadioButton "Once"
        /// </summary>
        public bool isRadioButtOnceChecked;
        public bool IsRadioButtOnceChecked
        {
            get { return isRadioButtOnceChecked; }

            set { isRadioButtOnceChecked = value; OnPropertyChanged("IsRadioButtOnceChecked"); }
        }
        /// <summary>
        /// Свойсво, которое связано со свойством IsChecked RadioButton "Serial"
        /// </summary>
        public bool isRadioButtSerialChecked;
        public bool IsRadioButtSerialChecked
        {
            get { return isRadioButtSerialChecked; }

            set { isRadioButtSerialChecked = value; OnPropertyChanged("IsRadioButtSerialChecked"); }
        }
        /// <summary>
        /// Свойсво, которое связано со свойством Text, TextBlockMsgFromServer. Содержит статус измериния
        /// </summary>
        public string textBlockMessageFromServer;
        public string TextBlockMessageFromServer
        {
            get { return textBlockMessageFromServer; }

            set { textBlockMessageFromServer = value; OnPropertyChanged("TextBlockMessageFromServer"); }
        }
        /// <summary>
        /// Свойство, отвечающее за доступность кнопки "Старт"
        /// </summary>
        public bool isButtonStartEnabled;
        public bool IsButtonStartEnabled
        {
            get { return isButtonStartEnabled; }

            set { isButtonStartEnabled = value; OnPropertyChanged("IsButtonStartEnabled"); }
        }
        /// <summary>
        /// Свойство, отвечающее за доступность RadioButton "Одиночное"
        /// </summary>
        public bool isRadioButtOnceEnabled;
        public bool IsRadioButtOnceEnabled
        {
            get { return isRadioButtOnceEnabled; }

            set { isRadioButtOnceEnabled = value; OnPropertyChanged("IsRadioButtOnceEnabled"); }
        }
        /// <summary>
        /// Свойство, отвечающее за доступность RadioButton "Серия"
        /// </summary>
        public bool isRadioButtSerialEnabled;
        public bool IsRadioButtSerialEnabled
        {
            get { return isRadioButtSerialEnabled; }

            set { isRadioButtSerialEnabled = value; OnPropertyChanged("IsRadioButtSerialEnabled"); }
        }
        /// <summary>
        /// Свойство, отвечающее за доступность ComboBox с номерами измерений
        /// </summary>
        public bool isComboBoxForNumberOfMeasureEnabled;
        public bool IsComboBoxForNumberOfMeasureEnabled
        {
            get { return isComboBoxForNumberOfMeasureEnabled; }

            set { isComboBoxForNumberOfMeasureEnabled = value; OnPropertyChanged("IsComboBoxForNumberOfMeasureEnabled"); }
        }
        /// <summary>
        /// Свойство, содержащее номер элемента
        /// </summary>
        public string textBoxNumberOfSupressor;
        public string TextBoxNumberOfSupressor
        {
            get { return textBoxNumberOfSupressor; }

            set { textBoxNumberOfSupressor = value; OnPropertyChanged("TextBoxNumberOfSupressor"); }
        }
        /// <summary>
        /// Свойство, отвечающее за доступность TextBox номером элемента
        /// </summary>
        public bool isTextBoxNumberSupEnabled;
        public bool IsTextBoxNumberSupEnabled
        {
            get { return isTextBoxNumberSupEnabled; }

            set { isTextBoxNumberSupEnabled = value; OnPropertyChanged("IsTextBoxNumberSupEnabled"); }
        }
        /// <summary>
        /// Свойство, содержащее значение измеренного напряжения
        /// </summary>
        public string textBlockOfVoltage;
        public string TextBlockOfVoltage
        {
            get { return textBlockOfVoltage; }

            set { textBlockOfVoltage = value; OnPropertyChanged("TextBlockOfVoltage"); }
        }
        /// <summary>
        /// Свойство, содержащее Ф.И.О. проводящего измерения
        /// </summary>
        public string textBlockOfSurnameInMainWindow;
        public string TextBlockOfSurnameInMainWindow
        {
            get { return textBlockOfSurnameInMainWindow; }

            set { textBlockOfSurnameInMainWindow = value; OnPropertyChanged("TextBlockOfSurnameInMainWindow"); }
        }
        /// <summary>
        /// Свойство, содержащее количество проводимых измерений
        /// </summary>
        public int textBoxCountOfMeasureInDialog;
        public int TextBoxCountOfMeasureInDialog
        {
            get { return textBoxCountOfMeasureInDialog; }

            set { textBoxCountOfMeasureInDialog = value; OnPropertyChanged("TextBoxCountOfMeasureInDialog"); }
        }
        /// <summary>
        /// Свойство, содержащее период между измерениями
        /// </summary>
        public double textBoxPeriodOfMeasureInDialog;
        public double TextBoxPeriodOfMeasureInDialog
        {
            get { return textBoxPeriodOfMeasureInDialog; }

            set { textBoxPeriodOfMeasureInDialog = value; OnPropertyChanged("TextBoxPeriodOfMeasureInDialog"); }
        }
        /// <summary>
        /// Свойство, содержащее Ф.И.О., выбранное в ComboBox диалогового окна
        /// </summary>
        public string comboBoxSurnameInDialog;
        public string ComboBoxSurnameInDialog
        {
            get { return comboBoxSurnameInDialog; }

            set { comboBoxSurnameInDialog = value; OnPropertyChanged("ComboBoxSurnameInDialog"); }
        }
        /// <summary>
        /// Интерфейс Бизнес-контекста
        /// </summary>
        private readonly IBusinessContext context;
        /// <summary>
        /// Свойство необходимое для построения осциллограмм
        /// </summary>
        public PlotModel plotModel;
        public PlotModel PlotModel
        {

            get { return plotModel; }

            set { plotModel = value; OnPropertyChanged("PlotModel"); }

        }
        /// <summary>
        /// Коллекция для построения осциллограмм
        /// </summary>
        public ObservableCollection<Osc> Oscs { get; set; }
        /// <summary>
        /// Коллекция устройств, которая содержит название устройства, мин., макс., номинальное напряжение, ток
        /// </summary>
        public ObservableCollection<Device> Devices { get; private set; }
        /// <summary>
        /// Коллекция параметров
        /// </summary>
        //public ObservableCollection<Parameter> Parameter { get; private set; }
        /// <summary>
        /// Коллекция измерений, содержащая измеренное напряжение, ток, дату проведения, Ф.И.О. проводившего
        /// </summary>
        public ObservableCollection<Measurement> Measurements { get; set; }
        /// <summary>
        /// Коллекция пользователей, содержащая Ф.И.О.
        /// </summary>
        public ObservableCollection<User> Users { get; private set; }
        /// <summary>
        /// Событие, возникающее при изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Коллекция номеров измерений
        /// </summary>
        public ObservableCollection<int> ComboColl { get; set; }
        /// <summary>
        /// Конструктор модели-представления
        /// </summary>
        /// <param name="context">Контекст данных</param>
        public ViewModel(IBusinessContext context)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }
            CompositionTarget.Rendering += CompositionTargetRendering;
            PlotModel = new PlotModel();
            this.context = context;
            Devices = context.GetDevices();
            //Parameter = context.GetParameters();
            Measurements = context.GetMeasuremets();
            Users = context.GetUsers();
            Oscs = new ObservableCollection<Osc>();
            ComboColl = new ObservableCollection<int>();
            SetUpModel();
            TextBlockOfVoltage = "U, В";
            TextBlockOfSurnameInMainWindow = Properties.Settings.Default.SettingName;
            TextBoxNumberOfSupressor = Properties.Settings.Default.SettingNumber.ToString();
            IsRadioButtOnceEnabled = true;
            IsButtonStartEnabled = true;
            IsRadioButtSerialEnabled = true;
            IsTextBoxNumberSupEnabled = true;
            TextBoxCountOfMeasureInDialog = Properties.Settings.Default.SettingCount;
            TextBoxPeriodOfMeasureInDialog = Properties.Settings.Default.SettingPeriod;
            ComboBoxSurnameInDialog = Properties.Settings.Default.SettingName;
            TextBlockMessageFromServer = "Статус:";
        }
        /// <summary>
        /// Создание экземпляра таймера
        /// </summary>
        private System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        /// <summary>
        /// Последнее обновление таймера в мс
        /// </summary>
        private long lastUpdateMilliSeconds;
        /// <summary>
        /// Обработчик события PropertyChanged
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Метод сохранения пользователя в БД
        /// </summary>
        public void SaveToUser()
        {
            // Запрос пользователя из бизнес-контекста
            var query = context.GetUserForSaveToUser(ComboBoxSurnameInDialog);
            //Если такого нет - создать
            if (query == null)
            {
                var user = new User { Surname = ComboBoxSurnameInDialog };
                Users.Add(user);
                context.AddUsersInDb(user);
                context.SaveDb();
            }
        }
        /// <summary>
        /// Метод построения осциллограмм
        /// </summary>
        /// <param name="FromNumber">Номер измерения</param>
        public void LoadData(int FromNumber)
        {
            
            UForCombo = 0;
            ///Создание дополнительной коллекции
            ObservableCollection<Osc> Temp = new ObservableCollection<Osc>();
            //Заполнение этой коллекции из коллекции Oscs
            for(int i=0;i<1000;i++)
            {
                double tay = 0;
                Temp.Add(new Osc() { U = Oscs[i + FromNumber * 1000].U, T = Oscs[i + FromNumber * 1000].T });
                tay=Temp[i].U;
                if(tay>UForCombo)//Для вывода напряжения при выборе измерения из комбобокса
                    UForCombo=tay;
            }
            //Создание экземпляра линий
            var lineSerie = new LineSeries
            {
                Title = "Измерение №" + (FromNumber+1).ToString(),
                Smooth = true,
            };
            //Добавление точек
            foreach(var i in Temp)
            {
                lineSerie.Points.Add(new DataPoint(i.T, i.U));
            }
            //отрисовка
            PlotModel.Series.Add(lineSerie);
            //Очищение коллекции
            Temp.Clear();
        }
        /// <summary>
        /// Отрисовка осей
        /// </summary>
        public void SetUpModel()
        {
            //Ось ортинат
            var valueAxis = new LinearAxis() { MinimumPadding = 0, MaximumPadding = 0, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "U, V", AbsoluteMinimum = 0 };
            PlotModel.Axes.Add(valueAxis);
            //Ось абсцисс
            var TimeAxis = new LinearAxis() { MinimumPadding = 0, MaximumPadding = 0, Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, TitlePosition = 1, Title = "T, ms", AbsoluteMinimum = -1 };
            PlotModel.Axes.Add(TimeAxis);
        }
        /// <summary>
        /// Метод проведения измерений
        /// </summary>
        public void SendToServer()
        {
            //Промежуточный массив с результатами измерений
            string[] masString = new string[2000];
            //Значение тока, передаваемое на сервер
            double CurToSend = 0;
            //Значение напряжения, передаваемого на сервер
            double VolToSend = 0;
            //Массив на отправку
            string[] MasToSend = new string[2];
            //Значение параметров зависит от выбранного устройства
            Device b = SelectedDeviceInComboBox as Device;
            try
            {
                CurToSend = (from s in Devices
                             where s.ID== b.ID
                             select s.Current).First();
                VolToSend = (from s in Devices
                             where s.ID == b.ID
                             select s.VoltageNom).First();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            bool i = false;
            int counter = 0;
            if (isRadioButtOnceChecked == true)
            {
                counter = 0;
            }
            else if (IsRadioButtSerialChecked == true)
            {
                counter = Properties.Settings.Default.SettingCount;
            }
            do
            {
                try
                {
                    i = false;
                    // Буфер для входящих данных
                    byte[] bytes = new byte[65536];
                    string dataFromServer = null;

                    // Соединяемся с удаленным устройством

                    // Устанавливаем удаленную точку для сокета
                    IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                    IPAddress ipAddr = ipHost.AddressList[0];
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 4510);

                    Socket sender1 = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    // Соединяем сокет с удаленной точкой
                    sender1.Connect(ipEndPoint);

                    MasToSend[0] = CurToSend.ToString();
                    MasToSend[1] = (2 * VolToSend).ToString();
                    string message = String.Join("&", MasToSend
                                 .Select(s => s.ToString())
                                 .ToArray()); ;
                    Console.WriteLine("Сокет соединяется с {0} ", sender1.RemoteEndPoint.ToString());
                    byte[] msg = Encoding.UTF8.GetBytes(message);

                    // Отправляем данные через сокет
                    int bytesSent = sender1.Send(msg);

                    // Получаем ответ от сервера
                    int bytesRec = sender1.Receive(bytes);
                    dataFromServer += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    //Если пришло сообщение "Крышка не закрыта!"
                    if (dataFromServer == "Крышка не закрыта!")
                    {
                        TextBlockMessageFromServer = "  Статус: " + dataFromServer;
                        CounterAmountMeasurement = CounterAmountMeasurement - 1;
                    }
                    //Если пришло сообщение "Нет питания!"
                    else if (dataFromServer == "Нет питания!")
                    {
                        TextBlockMessageFromServer = "  Статус: " + dataFromServer;
                        CounterAmountMeasurement = 1000;
                        CompositionTargetRendering(this, new EventArgs());
                        goto X;
                    }
                    else
                    {
                        masString = dataFromServer.Split('.');
                        //массив с результатами измерений
                        masVoltageFromServer = Array.ConvertAll<string, double>(masString, Convert.ToDouble);
                        //Вызов метода добавления результатов в БД
                        AddInCollection(CurToSend);
                    }
                    //Закрытие соединения
                X: sender1.Shutdown(SocketShutdown.Both);
                    sender1.Close();
                }
                catch (Exception ex)
                {
                    //В случае исключения, перезапустить таймер
                    stopwatch.Restart();
                    Console.WriteLine(ex.ToString());
                    i = true;
                }
                finally
                {
                }

            } while (i);

        }
        /// <summary>
        /// Метод добавления в коллекцию измерений
        /// </summary>
        /// <param name="Cur">Ток</param>
        public void AddInCollection(double Cur)
        {
            TextBlockMessageFromServer = "  Статус: " + "Идет измерение...";
            //Годность
            bool valid;
            Device b = SelectedDeviceInComboBox as Device;
            var Umin = context.GetUmin(b);
            var Umax = context.GetUmax(b);
            var Unom = context.GetUnom(b);
            //Отклонение от номинального значения
            var Deviat = Math.Abs(Umax - Umin) / 2;
            double Uizm = 0;
            //Добавление в коллекцию Oscs и определение наибольшего напряжения
            for (int q = 0; q < 1000; q++)
            {
                double temp = 0;
                temp = masVoltageFromServer[q];
                if (temp > Uizm)
                    Uizm = temp;

                Oscs.Add(new Osc() { U = masVoltageFromServer[q], T = masVoltageFromServer[q + 1000] });
            }
            //Вызов метода построения осциллограмм
            LoadData(CounterAmountMeasurement);
            //Обновить график
            PlotModel.InvalidatePlot(true);
            //Вывод измеренного напряжения
            TextBlockOfVoltage = Uizm.ToString() + " B";
            valid = (Math.Abs(Unom - Uizm) <= Deviat);
            //Создание нового экземпляра измерения
            var measure = new Measurement { device = b, NumberSupressor = Convert.ToInt32(TextBoxNumberOfSupressor), Date = DateTime.Now, Fio = TextBlockOfSurnameInMainWindow, Voltage = Uizm, Valid = valid, Current = Cur };
            //Добавление в коллекцию
            Measurements.Add(measure);
            //В БД
            context.AddMeasurementsInDb(measure);
            //Сохранение изменений
            context.SaveDb();
        }
        /// <summary>
        /// Метод останавливающий таймер и открывающий доступ к элементам
        /// </summary>
        public void TheEndOfMeasure()
        {
            //Стоп таймера
            stopwatch.Stop();
            //Вызов метода закрытия соединения
            ConnectionClose();
            //Открытия доступа к элементас
            IsButtonStartEnabled = true;
            IsRadioButtOnceEnabled = true;
            IsRadioButtSerialEnabled = true;
            IsComboBoxForNumberOfMeasureEnabled = true;
            CounterForCombo = CounterAmountMeasurement;
            //Метод создания коллекции номеров измерений
            ComboFuncForGrafh();
            //Сохранение настроек
            TextBoxNumberOfSupressor = (Convert.ToInt32(TextBoxNumberOfSupressor)+1).ToString();
            IsTextBoxNumberSupEnabled = true;
            if (TextBlockMessageFromServer.IndexOf("Нет питания!") == -1)
                TextBlockMessageFromServer = "  Статус: " + "Измерения завершены";
            CounterAmountMeasurement = 0;
        }
        /// <summary>
        /// Метод, посылающий сообщение о завершении измерений
        /// </summary>
        public void ConnectionClose()
        {
            byte[] bytes = new byte[65536];
            // Соединяемся с удаленным устройством
            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 4510);

            Socket sender1 = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender1.Connect(ipEndPoint);
            //Сообщение, означающее окончание измерений
            string message = "<TheEnd>";

            Console.WriteLine("Сокет соединяется с {0} ", sender1.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender1.Send(msg);
            sender1.Shutdown(SocketShutdown.Both);
            sender1.Close();
        }
        /// <summary>
        /// Обработчик события, в котором вызывается метод измерения
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            //Если таймер досчитал до указанного числа, вызывается метод измерений
            if (stopwatch.ElapsedMilliseconds > lastUpdateMilliSeconds + (Properties.Settings.Default.SettingPeriod*1000))
            {
                SendToServer();
                CounterAmountMeasurement++;
                lastUpdateMilliSeconds = stopwatch.ElapsedMilliseconds;
            }
            //Проверка на количество проведенных измерений
            if (IsRadioButtOnceChecked == true)
            {
                if (CounterAmountMeasurement > 0)
                {
                    TheEndOfMeasure();
                    CounterAmountMeasurement = 0;
                }
            }
            else if (IsRadioButtSerialChecked == true)
            {
                if (CounterAmountMeasurement >= TextBoxCountOfMeasureInDialog)
                {
                    TheEndOfMeasure();
                    CounterAmountMeasurement = 0;
                }
            }
        }
        /// <summary>
        /// Метод, запускающий таймер и сохранябщий настройки при нажатии кнопки "Запуск"
        /// </summary>
        public void IsButtonWillClick()
        {
            //Старт таймера
            stopwatch.Start();
            //Метод, сохраняющий настройки
            IsButtonInDialogClick();
            //Сохранение настроек
            Properties.Settings.Default.SettingNumber = Convert.ToInt32(TextBoxNumberOfSupressor)+1;
            Properties.Settings.Default.Save();
            //Закрытие доступа к элементам
            IsTextBoxNumberSupEnabled = false;
            IsComboBoxForNumberOfMeasureEnabled = false;
            IsButtonStartEnabled = false;
            IsRadioButtOnceEnabled = false;
            IsRadioButtSerialEnabled = false;
            //Очищение коллекций и области графика
            Oscs.Clear();
            ComboColl.Clear();
            PlotModel.Series.Clear();
        }
        /// <summary>
        /// Метод заполнения коллекции номерами измерений
        /// </summary>
        public void ComboFuncForGrafh()
        {
            for(int i=0;i<=CounterForCombo;i++)
            {
                ComboColl.Add(i);
            }
        }
        /// <summary>
        /// Метод выводящий осциллограмму в зависимости от выбранного номера измерения
        /// </summary>
        /// <param name="Plot1">Объект для обновления</param>
        public void IsComboBoxChanged(object Plot1)
        {
            OxyPlot.Wpf.Plot Plotva= Plot1 as OxyPlot.Wpf.Plot;
            if (IsComboBoxForNumberOfMeasureEnabled == true)
            {
                //Если выбран 0, то построить все измерения на одном графике
                if (Convert.ToInt32(SelectedNumberOfMeasureInComboBox) == 0)
                {
                    PlotModel.Series.Clear();
                    for (int i = 0; i < CounterForCombo;i++ )
                    {
                        LoadData(i);
                        TextBlockOfVoltage = UForCombo.ToString() + " B";
                        Plotva.RefreshPlot(true);
                    }
                }
                else
                {
                    //построить в соответствии с выбранным номером
                    PlotModel.Series.Clear();
                    LoadData(Convert.ToInt32(SelectedNumberOfMeasureInComboBox) - 1);
                    TextBlockOfVoltage = UForCombo.ToString() + " B";
                    Plotva.RefreshPlot(true);
                }
            }
        }
        /// <summary>
        /// Метод добавления нового устройства и параметров
        /// </summary>
        /// <param name="FromDataGridRow">Изменения в строке</param>
        public void AddNewDeviceAndParameter(object FromDataGridRow)
        {
            Device b = FromDataGridRow as Device;
            Device dev = context.GetDeviceForAddNewDeviceAndParameter(b);
            if (dev != null)
            {
                MessageBox.Show("Такое устройство уже есть!");
                //добавить параметры
                //dev.Parameter.Add(new Parameter()
                //{
                //    device = dev,
                //    propCurrent = b.propCurrent,
                //    propVoltageMax = b.propVoltageMax,
                //    propVoltageMin = b.propVoltageMin,
                //    propVoltageNom = b.propVoltageNom
                //});
            }
            else
            {
                //добавить в коллекцию устройств
                //Devices.Add(b);
                //в бд
                context.AddDeviceInDb(b);
            }
            //сохранить изменения
            context.SaveDb();
        }
        /// <summary>
        /// Метод, сохранияющий настройки диалогового окна
        /// </summary>
        public void IsButtonInDialogClick()
        {
            Properties.Settings.Default.SettingCount = TextBoxCountOfMeasureInDialog;
            Properties.Settings.Default.SettingPeriod = TextBoxPeriodOfMeasureInDialog;
            Properties.Settings.Default.SettingName = ComboBoxSurnameInDialog;
            Properties.Settings.Default.Save();
        }

     }
    }