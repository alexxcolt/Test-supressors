//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//MainClass.cs - класс, содержащий функцию Main
//Разработчик: Безлепкин А. С.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    /// <summary>
    /// Класс, содержащий функцию Main
    /// </summary>
    public class MainClass
    {
        /// <summary>
        /// Мин. напряжение
        /// </summary>
        static public double Umin = 37.1;
        /// <summary>
        /// Макс. напряжение
        /// </summary>
        static public double Umax = 42.9;
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args">аргумент</param>
        static void Main(string[] args)
        {
           A: try
            {
                string param;
                Console.WriteLine("Введите: \n");
                Console.WriteLine("1 - чтобы задать параметры");
                Console.WriteLine("2 - чтобы запустить сервер");
                param = Console.ReadLine();
                if (param == "1")
                {
                    Console.WriteLine("Введите Umin:");
                    Umin = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Введите Umax:");
                    Umax = Convert.ToDouble(Console.ReadLine());
                    Server.SendToClient();
                }
                if (param == "2")
                    Server.SendToClient();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Введите значения правильно!" + "\n" + "Дробные числа вводятся через запятую"+"\n");
                goto A;
            }


        }

    }
}
