using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SecondAttempt
{
    public class Storage<T> : System.Collections.IEnumerator
    {
        public Storage()
        {
            _objs = new List<T>(); //Массив хранящий все Obj находящиеся в хранилище
            _pos = -1;
        }

        T _currentobj = default;
        protected List<T> _objs; //Массив хранящий все Obj находящиеся в хранилище
        int _pos;

        public object Current { get { return _currentobj; } }
        public void Dispose()
        {
            _currentobj = default;
            _objs.Clear();
            _pos = -1;
        }

        /// <summary>
        /// Добавляет переданный Obj в хранилище
        /// </summary>
        public int AddAObj(T obj)
        {
            try
            {
                _objs.Add(obj);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Console.WriteLine(" Add Obj ={0}", obj);
            }
            return _objs.Count - 1;
        }

        /// <summary>
        /// Удаляет переданный Obj из хранилища
        /// </summary>
        public void RemoveObj(T obj)
        {
            try
            {
                _objs.Remove(obj);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Console.WriteLine("Remove Obj ={0}", obj);
            }
        }

        public void RemoveObj(int index)
        {
            _objs.RemoveAt(index);
        }
        // реализация метода интерфейса
        public bool MoveNext()
        {
            if (_pos < _objs.Count - 1)
            {
                _pos++;
                _currentobj = _objs[_pos];
                return true;
            }
            else
            {
                _currentobj = _objs[_pos];
                return false;
            }
        }

        public void Reset()
        {

            _currentobj = default;
            _pos = -1;
        }

        public int GetCount() { return _objs.Count; }

        public bool IsContains(T selObj)
        {
            return _objs.Contains(selObj);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _objs.GetEnumerator();
            // return _objs;
        }
        // индексатор
        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < _objs.Count) return (T)_objs[index];
                else throw new Exception("Out of Range");
            }
            set { if (index == _objs.Count) this.AddAObj(value); else _objs[index] = value; }
        }
        /// <summary>
        /// Создает выборку Obj в соответствии с переданным типом
        /// </summary>
        public IEnumerable<T> GetOnlyType<T>()
        {
            for (int i = 0; i < _objs.Count; i++)
            {
                if (_objs[i] is T)
                {
                    yield return (T)(object)_objs[i];
                }
            }
        }
    }
    class WareHouse : Storage<Computer>
    {
        public WareHouse() : base() { }
        public void AddComputer(Computer item)
        {
            _objs.Add(item);
        }

        public void DeleteComputer(Computer item)
        {
            _objs.Remove(item);
        }

        public IEnumerable<Computer> ViewGameByTypeVideocard(TypeVideocard selectTM)
        {
            foreach (Computer curcomp in _objs)
            {
                if (curcomp is Game game)
                    if (game.Videocard == selectTM)
                    {
                        yield return curcomp;
                    }
            }
        }
        public IEnumerable<Computer> ViewNotebookByDuration(double selectTM)
        {
            foreach (Computer curcomp in _objs)
            {
                if (curcomp is Notebook notebook)
                    if (notebook.Duration == selectTM)
                    {
                        yield return curcomp;
                    }
            }
        }
    }

}

