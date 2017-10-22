//project Client , version 1.1; Visual Studio 2013, version 12.0.21005.1; 
//.NET Framework 4.5.50938;
//Server.cs - класс, имитирующий работу супрессора
//Разработчик: Безлепкин А. С.
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Threading;

namespace SocketServer
{
    /// <summary>
    /// Класс, в котором организован приём сообщений от клиента
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Ток источника
        /// </summary>
        static public double Isource;
        /// <summary>
        /// Напряжение источника
        /// </summary>
        static public double Usource;
        /// <summary>
        /// Счётчик измерений
        /// </summary>
        static public int counter = 0;
        /// <summary>
        /// Случайное напряжение ограничения
        /// </summary>
        static public double r;
        /// <summary>
        /// Время
        /// </summary>
        static public double t = 0;
        /// <summary>
        /// Вспомагательный массив для передачи результатов
        /// </summary>
        static public string[] masString = new string[2000];
        /// <summary>
        /// Массив содержащий результаты измерений
        /// </summary>
        static public double[] masOne = new double[2000];
        /// <summary>
        /// Массив содержащий принятов сообщение от клиента
        /// </summary>
        static public string[] MasToReceive = new string[2];
        /// <summary>
        /// Расширенная нижняя граница напряжения
        /// </summary>
        static public double U1 = 0;
        /// <summary>
        /// Расширенная верхняя граница напряжения
        /// </summary>
        static public double U2 = 0;
        /// <summary>
        /// Измеренное напряжение
        /// </summary>
        static public double Uizm = 0;
        /// <summary>
        /// Время фронта
        /// </summary>
        static public double Tfront = 0.01;
        /// <summary>
        /// Время спада макс.
        /// </summary>
        static public double TspadMax = 0;
        /// <summary>
        /// Время спада мин.
        /// </summary>
        static public double TspadMin = 1.01;
        /// <summary>
        /// Случайное число
        /// </summary>
        static public int q = 0;
        /// <summary>
        /// Передаваемое сообщение
        /// </summary>
        static public string reply;
        /// <summary>
        /// Коэффициент в степени экспоненты
        /// </summary>
        static public double a = -Math.Log(0.5) / 1.05;
        /// <summary>
        /// Экземпляр класса генерирующего случайные значения
        /// </summary>
        static public Random rand = new Random();
        /// <summary>
        /// Метод пересылки между клиентом и сервером
        /// </summary>
        static public void SendToClient()
        {
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 4510);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    if (counter == 0)
                    {
                        Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);
                    }
                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[65536];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Полученное сообщение: " + data + "\n");
                        Console.WriteLine("Проведены все измерения\n\n");
                        counter = 0;
                        t = 0;
                        Uizm = 0;
                        reply = "";
                        continue;
                    }

                    MasToReceive = data.Split('&');
                    Isource = Convert.ToDouble(MasToReceive[0]);
                    Usource = Convert.ToDouble(MasToReceive[1]);
                    // Показываем данные на консоли
                    if (counter == 0)
                    {
                        Console.Write("Полученный сообщение: " + "I= " + Isource + " U= " + Usource + "\n\n");
                    }
                    // Отправляем ответ клиенту\
                    U2 = MainClass.Umax + 0.01 * MainClass.Umax;
                    U1 = MainClass.Umin - 0.01 * MainClass.Umin;
                Y:  r = Convert.ToDouble(rand.Next(Convert.ToInt32(U1 * 10), Convert.ToInt32(MainClass.Umax * 10))) / 10.0;
                    //Вычисление времени, когда экспонента упадет практически до 0
                    TspadMax = (Math.Log(0.01 / Usource) / (-a)) + 0.01;
                    //генерация случайного числа
                    q = rand.Next(0, 100);
                    //Вызов метода, формирующего сообщение
                    MessageToClient();
                    //Добавление в промежуточный массив
                    masString = Array.ConvertAll<double, string>(masOne, Convert.ToString);
                    //Вывод информации в консоль в зависимости от проведенных измерений
                    if (reply == "Крышка не закрыта!")
                        Console.WriteLine("Измерение № " + (counter + 1) + ":" + reply + "\n");
                    else if (reply == "Нет питания!")
                        Console.WriteLine("Измерение № " + (counter + 1) + ":" + reply + "\n");
                    else if (masOne[200] < 1)
                        Console.WriteLine("Измерение № " + (counter + 1) + ":" + " Разрыв!" + "\n");
                    else if (masOne[199] - r > 1)
                        Console.WriteLine("Измерение № " + (counter + 1) + ":" + " Короткое замыкание!" + "\n");
                    else Console.WriteLine("Измерение № " + (counter + 1) + ":" + " Выполнено" + "\n");
                    if (q >= 5)
                        reply = String.Join(".", masString
                                         .Select(s => s.ToString())
                                         .ToArray()); ;
                    byte[] msg = Encoding.UTF8.GetBytes(reply);

                    handler.Send(msg);//отправка клиенту

                    t = 0;
                    //повторить измерение
                    if (reply == "Крышка не закрыта!")
                    {
                        reply = null;
                        goto Y;
                    }

                    counter++;
                    q = 0;
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
        /// <summary>
        /// Метод, формирующий импульс напряжения
        /// </summary>
        /// <param name="Uizmer">Текущее значение напряжения</param>
        /// <param name="time">Время</param>
        /// <returns>Значение напряжения</returns>
        static public double Impulse(double Uizmer, double time)
        {
            //для фронта один алгоритм - линейный рост
            if (time <= Tfront)
                Uizmer = Usource * (time / Tfront);
            // для спада экспоненциальная зависимость
            else if (time > Tfront && time <= TspadMax)
                Uizmer = Usource * (Math.Exp(-a * (time - Tfront)));
            //Если упало почти в ноль
            if (Uizmer <= 0.01)
                return 0;
            return Uizmer;
        }
        /// <summary>
        /// Метод, формирующий сообщение для клиента
        /// </summary>
        static public void MessageToClient()
        {
            //Если высокое напряжение источника, то будет КЗ, пройдет весь импульс
            if (Usource >= 1500)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (q < 2)
                    {
                        reply = "Крышка не закрыта!";
                        break;
                    }

                    if (i <= 200)
                    {
                        if (i != 0 && i <= 200)
                        {
                            t = t + 0.00005;
                        }
                        Uizm = Impulse(Uizm, t);
                        if (q > 95 && q <= 100) //разрыв
                            Uizm = 0.01;
                    }
                    if (i > 200 && i <= 1000)
                    {
                        t = t + TspadMax / 800;
                        Uizm = Impulse(Uizm, t);

                        if (q > 95 && q <= 100)//разрыв
                            Uizm = 0.01;
                    }
                    masOne[i] = Uizm;
                    masOne[i + 1000] = t;
                }
            }
            else
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (Usource < 0.5)
                    {
                        reply = "Нет питания!";
                        break;
                    }
                    if (q < 2)
                    {
                        reply = "Крышка не закрыта!";
                        break;
                    }

                    if (i <= 200)
                    {
                        if (i != 0 && i <= 200)
                        {
                            t = t + 0.00005;
                        }
                        Uizm = Impulse(Uizm, t);
                        if (Uizm > r)
                            if (q >= 10 && q <= 90)//ограничение напряжения
                                Uizm = r;
                        if (q > 95 && q <= 100)//разрыв
                            Uizm = 0.01;
                    }
                    if (i > 200 && i <= 1000)
                    {
                        t = t + TspadMax / 800;
                        Uizm = Impulse(Uizm, t);
                        if (Uizm > r)
                            if (q >= 10 && q <= 90)//ограничение напряжения
                                Uizm = r;
                        if (q > 95 && q <= 100)//разрыв
                            Uizm = 0.01;
                    }

                    masOne[i] = Uizm;
                    masOne[i + 1000] = t;

                }
            }
        }

    }
}