using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjetoAcessos
{

    class Ambiente
    {


        int id;
        string nome;
        Queue<Log> logs = new Queue<Log>();

        public int Id
        {
            get
            {
                return id;
            }
        }

        internal Queue<Log> Logs
        {
            get
            {
                return logs;
            }

        }

        public string Nome
        {
            get
            {
                return nome;
            }
        }

        public Ambiente()
        {
            id = 1;
            nome = "";
            logs = new Queue<Log>();
        }

        public Ambiente(int i, string n)
        {
            id = i;
            nome = n;
            logs = new Queue<Log>();
        }

        public void RegistrarLog(Log log)
        {
            if (Logs.Count == 100)
            {
                Logs.Dequeue();
                Logs.Enqueue(log);
            }
            else
            {
                Logs.Enqueue(log);
            }
        }
        public bool Equals(object obj)
        {
            return ((Ambiente)obj).id.Equals(this.id);
        }

        public string toString()
        {
            return ("\nID: " + id + " Nome: " + nome);
        }



    }
}
