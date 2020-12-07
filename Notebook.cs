using System;
using System.Collections.Generic;
using System.Text;

namespace SecondAttempt
{
    class Notebook : Computer
    {
        public double Diagonal;
        public double Duration;
        
        public Notebook(string Maker, TypeProcessor Processor, int ram, TypeDrive Drive, double Diagonal,double Duration) : base(Maker, Processor, ram, Drive) 
        {
            this.Diagonal = Diagonal;
            this.Duration = Duration;
            Console.WriteLine($"Производитель:{Maker}/ Процессор: {Processor}/ Объем ОЗУ: {ram}gb/ Накопитель: {Drive}/Диагональ матрицы: {Diagonal}/ Продолжительность работы: {Duration} часов");
        }
        public override void GetInfo()
        {

            Console.WriteLine($"Диагональ матрицы: {Diagonal}/ Продолжительность работы: {Duration} часов");
        }

    }
}
