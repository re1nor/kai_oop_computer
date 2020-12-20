using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecondAttempt
{
    public delegate void EventClient(Operation evClient);
    class ActionsReader
    {
        public Clients cl; // Источник события - клиент
        public event EventClient event_actions;
        public WareHouseWithEvents Wh;
        public Computer comp_use; // Компьютер, с которой взаимодействует читатель
        public int timing;
        static Random rnd = new Random();


        public ActionsReader() { cl = null; comp_use = null; Wh = null; timing = 0; } //Пустой конструктор 
        public ActionsReader(Clients c, Computer k, WareHouseWithEvents wh, int t) //Конструктор 
        {
            cl = c; comp_use = k; Wh = wh; timing = t;
        }
        // Регистрация обработчика событий 
        public void InitEvent()
        {
            if (Wh != null) event_actions += Wh.OnEventComputer;
        }

        // Действие - использование
        public void Use(Clients clients, Computer curcomp, int intervalUse)
        {
            if (curcomp == null) { return; }
            if (clients == null) { return; }
            Operation ops = new Operation();
            ops.to = TypeOperation.StartUse;
            ops.cl = clients;
            ops.comp = curcomp;
            ops.Message = "Начало использования";
            clients.Active = false;

            if (event_actions != null) event_actions(ops);
            Thread.Sleep(intervalUse);
            ops = new Operation();
            ops.to = TypeOperation.StopUse;
            ops.cl = clients;
            ops.comp = curcomp;
            ops.Message = "Конец пользования";
            if (event_actions != null) event_actions(ops);
            clients.Active = true;
  
        }
        //Действие - Выключение
        public void Off(Clients clients, Computer curcomp)
        {
            if (clients == null) { return; }
            Operation ops = new Operation();
            ops.to = TypeOperation.Off;
            ops.cl = clients;
            ops.comp = curcomp;
            ops.Message = "Выключили компьютер";
            if (event_actions != null) event_actions(ops);
        }
        //Действие - Включение
        public void On(Clients clients, Computer curcomp)
        {
            if (clients == null) { return; }
            Operation ops = new Operation();
            ops.to = TypeOperation.On;
            ops.cl = clients;
            ops.comp = curcomp;
            ops.Message = "Включили компьютер";
            if (event_actions != null) event_actions(ops);
        }

        //Генерация событий
        public void DoActions()
        {
            Thread.Sleep(2000);
            if (cl != null && comp_use != null && timing > 0)
            {
                Thread.Sleep(rnd.Next(timing));
                On(cl, comp_use);
                Thread.Sleep(rnd.Next(timing));
                Use(cl, comp_use, timing);
                Thread.Sleep(rnd.Next(timing));
                Off(cl, comp_use);
            }
        }
    }
}