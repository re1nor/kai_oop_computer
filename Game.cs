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
        public double Power
        {
            // Проверка мощности видеокарты на нулевое и отрицательное значение. 
            set
            {
                if (value > 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"Мощность видеокарты не может быть {value}!");
                }
            }
            get
            {
                return Power;
            }
        }
        public Game(string Maker, TypeProcessor Processor, int ram, TypeDrive Drive, TypeVideocard Videocard, double Power) : base(Maker, Processor, ram, Drive) 
        {
            this.Videocard = Videocard;
            this.Power = Power; 
            Console.WriteLine($"Производитель:{Maker}/ Процессор: {Processor}/ Объем ОЗУ: {ram}gb/ Накопитель: {Drive}/ Видеокарта: {Videocard}/ Мощность видеокарты: {Power} tflops");
        }

    }
}
