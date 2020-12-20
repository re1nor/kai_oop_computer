using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace SecondAttempt
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.SetWindowSize(120,40);

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
            Console.WriteLine();
            #region TestWarehouse
            Computer[] bs = { 
            new Game("HyperPC", TypeProcessor.AMD,8, TypeDrive.HDD, TypeVideocard.Radeon,1919),
            new Notebook("Dell", TypeProcessor.Intel,16, TypeDrive.SSD, 15.6, 2.5 ),
            new Game("IBM", TypeProcessor.Other,27, TypeDrive.Other, TypeVideocard.Radeon,1424),
            new Notebook("HydraBoo", TypeProcessor.AMD, 10, TypeDrive.HDD, 15.6, 1.5 ),
            new Game("Samsung", TypeProcessor.Other,6, TypeDrive.Other, TypeVideocard.Radeon,768),
            new Notebook("Acer", TypeProcessor.Intel, 8, TypeDrive.SSD, 14, 3 ),
            new Game("Pantum", TypeProcessor.AMD,12, TypeDrive.HDD, TypeVideocard.Other,1000),
            new Notebook("Asus", TypeProcessor.Intel,4, TypeDrive.SSD, 14, 4),
            new Game("RE1NOR", TypeProcessor.Intel,64, TypeDrive.SSD, TypeVideocard.Nvidia,2626),
            new Notebook("HP", TypeProcessor.AMD, 4, TypeDrive.HDD, 13, 2.3 ),
            };
            Console.WriteLine();
            #endregion
            #region TestWareHouseWithEvents
            WareHouseWithEvents Wh = new WareHouseWithEvents();
            Clients[] cl =
            {
             new Clients("Bulat"),
             new Clients("Alexandr"),
             new Clients("Danil"),
             new Clients("Petr Konstantinovich"),
             new Clients("Diana HydraBoo"),
             new Clients("Dinara")
            };
            Wh.InitDB();
            Random rnd = new Random();

            Thread[] thArray = new Thread[cl.Length];
            ActionsReader[] acts = new ActionsReader[cl.Length];
            for (int i = 0; i < cl.Length; i++)
            {
                acts[i] = new ActionsReader(cl[i], bs[rnd.Next(bs.Length)], Wh, 1200 + (i % 2) * 300);
                acts[i].InitEvent();
                thArray[i] = new Thread(acts[i].DoActions);
                thArray[i].Start();
            }
#endregion
            #region ОжиданиеЗавершения
            bool b = true;
            while (b)
            {
                b = false;
                foreach (var thread in thArray)
                {
                    b = thread.IsAlive || b;
                }
            }
            Console.WriteLine("All threads end work!");
            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine();
            
            Console.WriteLine("Journal");

            for (int i = 0; i < cl.Length; i++)
            {
                foreach (Operation op in Wh.GetJournalForClient(cl[i]))
                    Console.WriteLine(op);
            }
            #endregion
            #region XmlReader
            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
            Wh.WriteToXml_Journal("out2.xml");
            
            #endregion
            Console.WriteLine();
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("LIST DB           LIST DB           LIST DB           LIST DB           LIST DB           LIST DB           LIST DB     ");
            Console.WriteLine();
            #region testDB
            Wh.ListDB();
            Console.WriteLine();
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("LISTDB FOR CLIENT         LISTDB FOR CLIENT         LISTDB FOR CLIENT         LISTDB FOR CLIENT        LISTDB FOR CLIENT");
            Console.WriteLine();


            for (int i = 0; i < cl.Length; i++)
            {
                Wh.ListDBForClient(cl[i]);
            }
            Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("LISTDB INTERVAL TIME         LISTDB INTERVAL TIME         LISTDB INTERVAL TIME         LISTDB INTERVAL TIME             ");
            Console.WriteLine();
            Console.WriteLine("Информация за последние 5 минут:");
            Console.WriteLine("От: "+DateTime.Now.AddSeconds(-300) + " / До:"+ DateTime.Now);
            Console.WriteLine();
            Wh.ListDBInterval(DateTime.Now,DateTime.Now.AddSeconds(-300));

            // очистка последних N элементов таблицы Operations
              Wh.CleanOperations(20);
            Wh.QuitDB();
            #endregion
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
