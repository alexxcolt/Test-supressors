//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//User.cs - класс пользователей
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    /// Класс, содержащий пользователей проводящих измерения
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Ф.И.О.
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="fio">Ф.И.О.</param>
        public User(string fio)
        {
            Surname = fio;
        }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public User()
        {
            Surname = "";
        }
    }
}
