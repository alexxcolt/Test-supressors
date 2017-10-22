//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//Device.cs - класс устройств
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    /// Класс, содержащий модели супрессоров
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Имя модели
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// Ток, при котором проводят тестирования
        /// </summary>
        public double Current { get; set; }
        /// <summary>
        /// Минимальное напряжение, которое должен обеспечить супрессор
        /// </summary>
        public double VoltageMin { get; set; }
        /// <summary>
        /// Номинальное напряжение
        /// </summary>
        public double VoltageNom { get; set; }
        /// <summary>
        /// Максимальное напряжение
        /// </summary>
        public double VoltageMax { get; set; }
        /// <summary>
        /// Виртуальная коллекция, содежащая измереенные напряжения, Ф.И.О., ток, дату проведения
        /// </summary>
        public virtual ObservableCollection<Measurement> Measurements { get; private set; }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Device()
        {
            Measurements = new ObservableCollection<Measurement>();
        }
        public Device(string DevName, double Cur, double VolMin, double VolNom, double VolMax)
        {
            DeviceName = DevName;
            Current = Cur;
            VoltageMin = VolMin;
            VoltageNom = VolNom;
            VoltageMax = VolMax;
        }
    }
}
