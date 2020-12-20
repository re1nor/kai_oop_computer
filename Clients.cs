using System;
using System.Collections.Generic;
using System.Text;

namespace SecondAttempt
{
    public class Clients : IActions
    {
        public string FullName;
        public bool Active { get; set; }
        private static int ID = 0;
        private int Id_clients;
        public int IDClients { get { return Id_clients; } }
       
        public override string ToString()
        {
            return FullName;
        }

        public Clients(string FullName)
        {
            this.FullName = FullName;
            ID++;
            Id_clients = ID;
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
