//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//Measurement.cs - класс измерений
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    /// Класс, содержащий измерения для устройств
    /// </summary>
    public class Measurement
    {
        public virtual  User User {get;set;}

        /// <summary>
        /// Идентификационный номер
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Номер супрессора
        /// </summary>
        public int NumberSupressor { get; set; }
        /// <summary>
        /// Дата проведения измерения
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Ф.И.О. человека, проводившего измерения
        /// </summary>
        public string Fio { get; set; }
        /// <summary>
        /// Ток, протекающий через супрессор
        /// </summary>
        public double Current { get; set; }
        /// <summary>
        /// Напряжение
        /// </summary>
        public double Voltage { get; set; }
        /// <summary>
        /// Виртуальный экземпляр класса устройств
        /// </summary>
        public virtual Device device { get; set; }
        /// <summary>
        /// Годность устройства
        /// </summary>
        public bool Valid { get; set; }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Measurement()
        {
            NumberSupressor = 1;
            Date = DateTime.Now;
            Fio = "";
            Voltage = 0;
        }
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="NumSup">Номер супрессора</param>
        /// <param name="time">Время проведения тестирования</param>
        /// <param name="fio"> Ф.И.О.</param>
        /// <param name="voltage">Напряжение</param>
        public Measurement(int NumSup, DateTime? time, string fio, double voltage)
        {
            NumberSupressor = NumSup;
            Date = time;
            Fio = fio;
            Voltage = voltage;
        }
    }
}
