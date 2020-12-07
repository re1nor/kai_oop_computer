using System;
using System.Collections.Generic;
using System.Text;

namespace SecondAttempt
{
    public enum TypeVideocard
    {
        Nvidia =0,
        Radeon,
        Other
    }
    class Game : Computer
    {
        public TypeVideocard Videocard;
        private double power;
        public double Power
        {
            // Проверка мощности видеокарты на нулевое и отрицательное значение. 
            set
            {
                if (value > 0)
                {
                    power = value;
                }
                else
                {
                    Console.WriteLine($"Мощность видеокарты не может быть {value}!");
                }
            }
            get
            {
                return power;
            }
        }
        public Game(string Maker, TypeProcessor Processor, int ram, TypeDrive Drive, TypeVideocard Videocard, double Power) : base(Maker, Processor, ram, Drive) 
        {
            this.Videocard = Videocard;
            this.Power = Power;
            Console.WriteLine($"Производитель:{Maker}/ Процессор: {Processor}/ Объем ОЗУ: {ram}gb/ Накопитель: {Drive}/Видеокарта: {Videocard}/ Мощность видеокарты: {Power} tflops");
        }
        public override void GetInfo()
        {
            
            Console.WriteLine($"Видеокарта: {Videocard}/ Мощность видеокарты: {Power} tflops");
        }

    }
}
