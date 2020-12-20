using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
namespace SecondAttempt
{


    class WareHouseWithEvents : WareHouse
    {
        public List<Operation> listops;
        //объекты бд
        public SqlConnection con;
        public SqlDataAdapter daEv, daTC, daComp, daCl, daOp;
        public SqlCommandBuilder cmdEv, cmdTC, cmdComp, cmdCl, cmdOp;
        public DataSet DS;
        public DataTable dtEv, dtTC, dtComp, dtCl, dtOp;
        int idop = 0;
      
        public WareHouseWithEvents()
        {
            listops = new List<Operation>();
        }
        //Обработчик событий
        public virtual void OnEventComputer(Operation opr)
        {
            Console.WriteLine("On Event {0}", opr);
            lock (this)
            {
                if (opr == null) { Console.WriteLine("opr is null"); return; }
                if (opr.cl == null) { Console.WriteLine("cl is null"); return; }
                try
                {
                    listops.Add(opr);
                    //DB
                    Computer curcomp = opr.comp;
                    Clients curcl = opr.cl;

                    int idcomp = curcomp.IDComp;
                    int idcl = curcl.IDClients;

                    DataRow[] selectcomp = dtComp.Select(string.Format("Idcomp={0}", idcomp));
                    DataRow[] selectcl = dtCl.Select(string.Format("Idcl={0}", idcl));

                    if (selectcomp.Length == 0 && selectcl.Length == 0)
                    {
                        DataRow drcomp = dtComp.NewRow();
                        DataRow drcl = dtCl.NewRow();

                        drcomp["IdComp"] = idcomp;
                        drcomp["Maker"] = curcomp.Maker;
                        drcomp["Processor"] = curcomp.Processor;
                        drcomp["IdTC"] = ID_TypeComp(curcomp);
                        dtComp.Rows.Add(drcomp);

                        drcl["IdCl"] = idcl;
                        drcl["Name"] = curcl.FullName;
                        dtCl.Rows.Add(drcl);
                    }
                    //Операции

                    DataRow dropr = dtOp.NewRow();
                    idop++;

                    dropr["IdOp"] = idop;
                    dropr["IdComp"] = idcomp;
                    dropr["IdCl"] = idcl;
                    dropr["IdEv"] = opr.to;
                    dropr["DateTime"] = opr.timeop;
                    dropr["Message"] = opr.Message;

                    dtOp.Rows.Add(dropr);
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    Console.WriteLine("Event={0} Computer={1} Clients={2}", opr.to, opr.comp, opr.cl);
                }
                
            }
        }
        //Отчет по времени
        public IEnumerable<Operation> GetOperationsWithTimeInterval(DateTime beginDT, DateTime endDT)
        {
            IEnumerable<Operation> query = from oj in listops
                                           where (oj.timeop >= beginDT && oj.timeop <= endDT)
                                           select oj;
            return query;
        }
        //Отчет по читателям
        public IEnumerable<Operation> GetJournalForClient(Clients curclient)
        {
            IEnumerable<Operation> query = from oj in listops
                                           where (oj.cl == curclient)
                                           select oj;
            return query;
        }
        //Журнал XML
        public void WriteToXml_Journal(string namefile)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = (" ")
            };
            XmlWriter writer = XmlWriter.Create(namefile, settings);
            int id = 1;
            writer.WriteStartElement("Operations");
            foreach (var op in listops)
            {
                writer.WriteStartElement("Operation");
                writer.WriteAttributeString("ID", id.ToString());
                writer.WriteElementString("Type", op.to.ToString());
                writer.WriteStartElement("DateTime");
                writer.WriteElementString("Date", op.timeop.ToLongDateString());
                writer.WriteElementString("Time", op.timeop.ToShortTimeString());
                writer.WriteEndElement();
                writer.WriteStartElement("Clients");
                writer.WriteElementString("Name", op.cl.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("Computer");
                if (op.comp is Game)
                {
                    writer.WriteElementString("TypeComputer", "Game");
                }
                else
                {
                    writer.WriteElementString("TypeComputer", "Notebook");
                }
                writer.WriteElementString("Maker", op.comp.Maker);
                writer.WriteEndElement();
                writer.WriteEndElement();
                id++;
            }
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
        //Вывод журнала XML
        public void ReadXml_Journal(string namefile)
        {
            XmlReader xmlreader = XmlReader.Create(namefile);
            while (xmlreader.Read())
            {
                if (xmlreader.IsStartElement())
                {
                    Console.WriteLine("<{0}> ", xmlreader.Name);
                    if (xmlreader.HasAttributes)
                    {
                        Console.WriteLine("Attributes of <" + xmlreader.Name + ">");
                        while (xmlreader.MoveToNextAttribute())
                        {
                            Console.WriteLine(" {0}={1}", xmlreader.Name, xmlreader.Value);
                        }
                        xmlreader.MoveToElement();
                    }
                    if (xmlreader.HasValue) Console.WriteLine(xmlreader.Value);
                }
                if (xmlreader.HasValue) Console.WriteLine(xmlreader.Value);
                if (xmlreader.NodeType == XmlNodeType.EndElement) Console.WriteLine("</{0}>", xmlreader.Name);
            }
            xmlreader.Close();
        }

        public int ID_TypeComp(Computer comp)
        {
            if (comp is Game) return 1;
            if (comp is Notebook) return 2;
            return 0;
        }
        public void InitDB()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\PROJECT\VS\SecondAttempt\SecondAttempt\CompBD.mdf;Integrated Security=True");
            con.Open();
            DS = new DataSet("DB");

            daEv = new SqlDataAdapter("select * from Events", con);
            daTC = new SqlDataAdapter("select * from TypeComputer", con);
            daComp = new SqlDataAdapter("select * from Computer", con);
            daCl = new SqlDataAdapter("select * from Clients", con);
            daOp = new SqlDataAdapter("select * from Operations", con);

            cmdEv = new SqlCommandBuilder(daEv);
            cmdTC = new SqlCommandBuilder(daTC);
            cmdComp = new SqlCommandBuilder(daComp);
            cmdCl = new SqlCommandBuilder(daCl);
            cmdOp = new SqlCommandBuilder(daOp);

            daEv.Fill(DS, "Events");
            daTC.Fill(DS, "TypeComputer");
            daComp.Fill(DS, "Computer");
            daCl.Fill(DS, "Clients");
            daOp.Fill(DS, "Operations");

            dtEv = DS.Tables["Events"];
            dtTC = DS.Tables["TypeComputer"];
            dtComp = DS.Tables["Computer"];
            dtCl = DS.Tables["Clients"];
            dtOp = DS.Tables["Operations"];

            idop = dtOp.Rows.Count;

            ViewDS(DS);
        }
        public void QuitDB()
        {
            daComp.Update(DS, "Computer");
            daCl.Update(DS, "Clients");
            daOp.Update(DS, "Operations");
            con.Close();
        }

        public void ViewDS(DataSet DS)
        {
            Console.WriteLine("DataSet is named: {0}", DS.DataSetName);
            // Вывести каждую таблицу.
            foreach (DataTable dt in DS.Tables)
            {
                ViewDataTable(dt);
            }
        }

        public void ViewDataTable(DataTable dt)
        {
            Console.WriteLine("\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
            Console.WriteLine("Table =>  {0}", dt.TableName);
            // Вывести имена столбцов.
            for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
            {
                Console.Write(dt.Columns[curCol].ColumnName + "\t");
            }
            Console.WriteLine();
            // Вывести DataTable.
            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                {
                    Console.Write(dt.Rows[curRow][curCol].ToString() + "\t");
                }
                Console.WriteLine();
                
            }
            Console.WriteLine("\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");

        }
        //Просмотр БД
        public void ListDB()
        {
            var query = from tj in dtOp.AsEnumerable()
                        join tb in dtComp.AsEnumerable() on tj.Field<int>("IdComp") equals tb.Field<int>("IdComp")
                        join tc in dtCl.AsEnumerable() on tj.Field<int>("IdCl") equals tc.Field<int>("IdCl")
                        join te in dtEv.AsEnumerable() on tj.Field<int>("IdEv") equals te.Field<int>("IdEv")
                        select new
                        {
                            makerb = tb.Field<string>("Maker"),
                            processorb = tb.Field<string>("Processor"),
                            namer = tc.Field<string>("Name"),
                            dataevent = tj.Field<DateTime>("DateTime"),
                            nevents = te.Field<string>("Name"),
                            idtb = tb.Field<int>("IdTC"),
                            mes = tj.Field<string>("Message"),
                            idCl = tc.Field<int>("IdCl")
                        };
            foreach (var op in query)
            {
                DataRow[] tcomp = dtTC.Select(string.Format("IdTC={0}", op.idtb));
                Console.WriteLine("{0}  {1}  {2}  {3}  {4}  {5}  {6}", op.makerb, op.processorb, op.namer, tcomp[0]["Type"], op.nevents, op.dataevent, op.mes);
                
            }   
        }
        //Просмотр БД для клиента
        public void ListDBForClient(Clients curcl)
        {
            
            var query = from tj in dtOp.AsEnumerable()
                        join tb in dtComp.AsEnumerable() on tj.Field<int>("IdComp") equals tb.Field<int>("IdComp")
                        join tc in dtCl.AsEnumerable() on tj.Field<int>("IdCl") equals tc.Field<int>("IdCl")
                        join te in dtEv.AsEnumerable() on tj.Field<int>("IdEv") equals te.Field<int>("IdEv")
                        select new
                        {
                            makerb = tb.Field<string>("Maker"),
                            processorb = tb.Field<string>("Processor"),
                            namer = tc.Field<string>("Name"),
                            dataevent = tj.Field<DateTime>("DateTime"),
                            nevents = te.Field<string>("Name"),
                            idtb = tb.Field<int>("IdTC"),
                            mes = tj.Field<string>("Message"),
                            idCl = tc.Field<int>("IdCl")
                        };
            foreach (var op in query)
            {
                if (curcl.IDClients == op.idCl)
                {
                    DataRow[] tcomp = dtTC.Select(string.Format("IdTC={0}", op.idtb));
                    Console.WriteLine("{0}  {1}  {2}  {3}  {4}  {5}  {6}", op.makerb, op.processorb, op.namer, tcomp[0]["Type"], op.nevents, op.dataevent, op.mes);
                }
                
            }
            ;
        }
        
        public void ListDBInterval(DateTime dTMax, DateTime dtMin)
        {
            var query = from tj in dtOp.AsEnumerable()
                        join tb in dtComp.AsEnumerable() on tj.Field<int>("IdComp") equals tb.Field<int>("IdComp")
                        join tc in dtCl.AsEnumerable() on tj.Field<int>("IdCl") equals tc.Field<int>("IdCl")
                        join te in dtEv.AsEnumerable() on tj.Field<int>("IdEv") equals te.Field<int>("IdEv")
                        select new
                        {
                            makerb = tb.Field<string>("Maker"),
                            processorb = tb.Field<string>("Processor"),
                            namer = tc.Field<string>("Name"),
                            dataevent = tj.Field<DateTime>("DateTime"),
                            nevents = te.Field<string>("Name"),
                            idtb = tb.Field<int>("IdTC"),
                            mes = tj.Field<string>("Message"),
                            idCl = tc.Field<int>("IdCl")
                        };
            foreach (var op in query)
            {
                if (op.dataevent < dTMax && op.dataevent > dtMin)
                {
                        DataRow[] tcomp = dtTC.Select(string.Format("IdTC={0}", op.idtb));
                        Console.WriteLine("{0}  {1}  {2}  {3}  {4}  {5}  {6}", op.makerb, op.processorb, op.namer, tcomp[0]["Type"], op.nevents, op.dataevent, op.mes);
                        
                }
            }
        }
        //Очистка таблицы операций
        public void CleanOperations(int id)
        {
            try
            {

                string sql = "Delete from Operations where IdOp < @id";

                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = sql
                };

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                int rowCount = cmd.ExecuteNonQuery();
                DS.AcceptChanges();
                Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
                Console.WriteLine("Очистил " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }


        }
    }

    
}