using System;
using System.Collections.Generic;

namespace SecondAttempt
{
    class Program 
    {
        static void Main(string[] args)
        {
            Game test = new Game("HyperPC", TypeProcessor.AMD, 8, TypeDrive.HDD, TypeVideocard.Radeon, 568);
            test.Used();

            Console.WriteLine();

            Notebook test1 = new Notebook("Dell", TypeProcessor.Intel, 16, TypeDrive.SSD, 15.6, 2.5);
            test1.Upgrade();
            

            #region ClientsList
            List<Clients> ClientList = new List<Clients>();
            ClientList.Add(new Clients("Замалетдинов Булат Маратович", 08.12, 1.5));
            ClientList.Add(new Clients("Семенов Петр Константинович", 09.12, 0.5));
            ClientList.Add(new Clients("Батенев Александр Александрович", 09.12, 0.5));
            ClientList.Add(new Clients("Тухтаходжаев Сардорхужа Абдурахимович", 09.12, 0.5));
            ClientList.Add(new Clients("Старостин Эмиль Романович", 09.12, 0.5));
            foreach(Clients i in ClientList)
            {
                Console.WriteLine($"ФИО:{i.FullName}; Дата: {i.DateOfUse}; Время пользования: {i.TimeOfUse} часов");
            }
            #endregion


            #region Signature

            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░████░░░░░░████░░░░██░░██░░░░██░░░░████░░░░░░████░░░░░░░░░░░░████░░░░░░████░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░██░░░░██░░██░░░░░░████░░██░░░░██░░██░░░░██░░██░░░░██░░░░░░░░██░░░░██░░██░░░░██░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░██░░░░██░░██░░░░██░░██░░████░░██░░██░░░░██░░██░░░░██░░░░░░░░██░░░░██░░██░░░░██░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░██░░██░░░░██░░░░░░░░██░░██░░████░░██░░░░██░░██░░██░░░░░░░░░░██░░░░░░░░██░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░████░░░░░░██████░░░░██░░██░░░░██░░██░░░░██░░████░░░░░░░░░░░░██░░████░░██░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░██░░██░░░░██░░░░░░░░██░░██░░░░██░░██░░░░██░░██░░██░░░░░░░░░░██░░░░██░░██░░░░██░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░██░░░░██░░██░░░░░░░░██░░██░░░░██░░██░░░░██░░██░░░░██░░░░░░░░██░░░░██░░██░░░░██░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░██░░░░██░░░░████░░░░██░░██░░░░██░░░░████░░░░██░░░░██░░░░░░░░░░████░░░░░░████░░░░░░░░░░░░░░░░░░░░░░░");
            #endregion    
        }
    }
}
