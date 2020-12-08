using System;
using System.Collections.Generic;
using System.Text;

namespace SecondAttempt
{
    class Clients : IActions
    {
        public string FullName;
        public double DateOfUse;
        public double TimeOfUse;

        public Clients(string FullName, double DateOfUse, double TimeOfUse)
        {
            this.FullName = FullName;
            this.DateOfUse = DateOfUse;
            this.TimeOfUse = TimeOfUse;
        }
        public void GetInfo()
        {
            Console.WriteLine($"ФИО:{FullName}; Дата: {DateOfUse}; Время пользования: {TimeOfUse} часов");
        }
        public void Used()
        {
            Console.WriteLine($"{FullName} использует компьютер в данный момент");
        }  
        public void Upgrade()
        {
            Console.WriteLine("У клиентов нет возможности улучшать компьютеры");
        }
    }
}
