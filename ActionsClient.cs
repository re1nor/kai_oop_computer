﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecondAttempt
{
    public delegate void EventClient(Operation evClient);
    class ActionsClient
    {
        public Clients cl; // Источник события - клиент
        public event EventClient Event_actions;
        public WareHouseWithEvents Wh;
        public Computer comp_use; // Компьютер, с которой взаимодействует читатель
        public int timing;
        static readonly Random rnd = new Random();


        public ActionsClient() { cl = null; comp_use = null; Wh = null; timing = 0; } //Пустой конструктор 
        public ActionsClient(Clients c, Computer k, WareHouseWithEvents wh, int t) //Конструктор 
        {
            cl = c; comp_use = k; Wh = wh; timing = t;
        }
        // Регистрация обработчика событий 
        public void InitEvent()
        {
            if (Wh != null) Event_actions += Wh.OnEventComputer;
        }

        // Действие - использование
        public void Use(Clients clients, Computer curcomp, int intervalUse)
        {
            if (curcomp == null) { return; }
            if (clients == null) { return; }
            Operation ops = new Operation
            {
                to = TypeOperation.StartUse,
                cl = clients,
                comp = curcomp,
                Message = "Начало использования"
            };
            clients.Active = false;

            Event_actions?.Invoke(ops);
            Thread.Sleep(intervalUse);
            ops = new Operation
            {
                to = TypeOperation.StopUse,
                cl = clients,
                comp = curcomp,
                Message = "Конец пользования"
            };
            Event_actions?.Invoke(ops);
            clients.Active = true;
  
        }
        //Действие - Выключение
        public void Off(Clients clients, Computer curcomp)
        {
            if (clients == null) { return; }
            Operation ops = new Operation
            {
                to = TypeOperation.Off,
                cl = clients,
                comp = curcomp,
                Message = "Выключили компьютер"
            };
            Event_actions?.Invoke(ops);
        }
        //Действие - Включение
        public void On(Clients clients, Computer curcomp)
        {
            if (clients == null) { return; }
            Operation ops = new Operation
            {
                to = TypeOperation.On,
                cl = clients,
                comp = curcomp,
                Message = "Включили компьютер"
            };
            Event_actions?.Invoke(ops);
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