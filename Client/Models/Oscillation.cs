//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//Oscillation.cs - класс, используемый для построения осциллограмм
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    /// Класс для построения осциллограмм
    /// </summary>
    public class Osc
    {
        /// <summary>
        /// Напряжение по оси ординат
        /// </summary>
        public double U { get; set; }
        /// <summary>
        /// Время по оси абсцисс
        /// </summary>
        public double T { get; set; }
    }
}
