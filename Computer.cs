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
    abstract class Computer : ISellComputer
    {
        public string Maker;
        public TypeProcessor Processor;
        private int ram;
        public int Ram
        {
            // Проверка объема на нулевое и отрицательное значение. 
            set 
            {
                if (value > 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"Объем оперативной памяти не может быть {value}!");
                }           
            }
            get
            {
                return ram;
            }
        }
        public TypeDrive Drive;

        // Создаем конструктор
        public Computer(string Maker, TypeProcessor Processor, int ram, TypeDrive Drive)
        {
            this.Maker = Maker;
            this.Processor = Processor;
            this.ram = ram;
            this.Drive = Drive;
            Console.WriteLine($"Производитель: {Maker} / Процессор: {Processor} / Объем ОЗУ: {ram} / Накопитель: {Drive}");
        }
        public void Used()
        {
            Console.WriteLine("Компьютер используется");
        }
        public void Upgrade()
        {
            Console.WriteLine("Компьютер улучшен");
        }
    }
}
