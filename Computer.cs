using System;
using System.Collections.Generic;
using System.Text;

namespace SecondAttempt // Базовый класс Computer
{
    public enum TypeProcessor
    {
        AMD = 0,
        Intel,
        Other
    } 
    public enum TypeDrive
    {
        HDD = 0,
        SSD,
        Other
    }
    class Computer
    {
        public string Maker; // поле произодитель
        public TypeProcessor Processor;
        public int Ram;
        public TypeDrive Drive;

        public Computer(string Maker, TypeProcessor Processor, int ram, Type Drive)
        {
            Console.WriteLine($"Производитель: {Maker} / Процессор: {Processor} / Объем ОЗУ: {ram} / Накопитель: {Drive}");
        }
    }
}
