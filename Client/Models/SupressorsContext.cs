//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//SupressorsContext.cs - класс для создания таблиц в БД
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Client.Models
{
    /// <summary>
    /// Класс, необходимый для создания таблиц в БД
    /// </summary>
    public class SupressorsContext : DbContext
    {
        /// <summary>
        /// Коллекция всех сущностей типа Device, которые имеют свойства: имя изделия, мин. напряжение, макс. напряжение, номинальное, ток.
        /// </summary>
        public DbSet<Device> Devices { get; set; }
        /// <summary>
        /// Коллекция всех сущностей типа Measurement, которые имеют свойства: дата проведения, Ф.И.О., напряжение, ток.
        /// </summary>
        public DbSet<Measurement> Measurements { get; set; }
        /// <summary>
        /// Коллекция всех сущностей типа User, которые имеют свойства: Ф.И.О.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
