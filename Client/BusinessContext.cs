//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//BusinessContext.cs - класс описывающий логику взаимодействия с БД
//Разработчик: Безлепкин А. С.
using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Класс, описывающий логику взаимодействия с БД
    /// </summary>
    public sealed class BusinessContext : IDisposable, IBusinessContext
    {
        private readonly SupressorsContext context;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessContext"/> class.
        /// </summary>
        public BusinessContext()
        {
            context = new SupressorsContext();
        }

        /// <summary>
        /// Gets the underlying <see cref="DataContext"/>.
        /// </summary>
        public SupressorsContext DataContext
        {
            get { return context; }
        }
        /// <summary>
        /// Получение коллекции устройств из БД
        /// </summary>
        /// <returns>Коллекция устройств</returns>
        public ObservableCollection<Device> GetDevices()
        {
            return new ObservableCollection<Device>((from s in context.Devices
                                                        orderby s.DeviceName
                                                        select s).ToList());
        }
        /// <summary>
        /// Получение коллекции измерений из БД
        /// </summary>
        /// <returns>Коллекция измерений</returns>
        public ObservableCollection<Measurement> GetMeasuremets()
        {
            return new ObservableCollection<Measurement>((from d in context.Measurements
                                                   orderby d.NumberSupressor
                                                   select d).ToList());
        }
        /// <summary>
        /// Получение коллекции пользователей из БД
        /// </summary>
        /// <returns>Коллекция пользователей</returns>
        public ObservableCollection<User> GetUsers()
        {
            return new ObservableCollection<User>((from d in context.Users
                                            orderby d.Surname
                                            select d).ToList());
        }
        /// <summary>
        /// Метод получения макс. напряжения из БД
        /// </summary>
        /// <param name="b">Устройство</param>
        /// <returns>Макс. напряжение</returns>
        public double GetUmax(Device b)
        {
            return (from s in context.Devices
             where s.ID == b.ID
             select s.VoltageMax).First();
        }
        /// <summary>
        /// Метод получения мин. напряжения из БД
        /// </summary>
        /// <param name="b">Устройство</param>
        /// <returns>Мин. напряжение</returns>
        public double GetUmin(Device b)
        {
            return (from s in context.Devices
             where s.ID == b.ID
             select s.VoltageMin).First();
        }
        /// <summary>
        /// Метод получения ном. напряжения из БД
        /// </summary>
        /// <param name="b">Устройство</param>
        /// <returns>Ном. напряжение</returns>
        public double GetUnom(Device b)
        {
            return (from s in context.Devices
                    where s.ID == b.ID
                    select s.VoltageNom).First();
        }
        /// <summary>
        /// Метод получения устройства из БД, имя которого совпадает с именем переданного параметра
        /// </summary>
        /// <param name="b">Параметры</param>
        /// <returns>Устройство</returns>
        public Device GetDeviceForAddNewDeviceAndParameter(Device b)
        {
            return context.Devices.FirstOrDefault(g => g.DeviceName == b.DeviceName);
        }
        /// <summary>
        /// Метод добавления параметров в БД
        /// </summary>
        /// <param name="b">Параметер</param>
        public void AddDeviceInDb(Device b)
        {
            context.Devices.Add(b);
        }
        /// <summary>
        /// Метод сохранения изменений в БД
        /// </summary>
        public void SaveDb()
        {
            context.SaveChanges();
        }
        /// <summary>
        /// Метод получения пользователя
        /// </summary>
        /// <param name="SurnameInDialog"> Ф.И.О.</param>
        /// <returns>Пользователь</returns>
        public User GetUserForSaveToUser(string ComboBoxSurnameInDialog)
        {
            return (from s in context.Users
                         where s.Surname == ComboBoxSurnameInDialog
                         select s).FirstOrDefault();
        }
        /// <summary>
        /// Метод добавления пользователя в БД
        /// </summary>
        /// <param name="user">Пользователь</param>
        public void AddUsersInDb(User user)
        {
            context.Users.Add(user);
        }
        /// <summary>
        /// Метод добавления измерений в БД
        /// </summary>
        /// <param name="measure">Параметер</param>
        public void AddMeasurementsInDb(Measurement measure)
        {
            context.Measurements.Add(measure);
        }
        #region IDisposable Members
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing)
                return;

            if (context != null)
                context.Dispose();

            disposed = true;
        }
        #endregion
    }

}
