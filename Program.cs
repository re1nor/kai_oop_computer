﻿using System;

namespace SecondAttempt
{
    class Program 
    {
        static void Main(string[] args)
        {
            Game test = new Game("Dell", TypeProcessor.Intel, 16, TypeDrive.SSD, TypeVideocard.Nvidia, 1024);
            test.Used();





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
