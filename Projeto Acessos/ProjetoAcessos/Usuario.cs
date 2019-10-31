using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAcessos
{
    class Usuario
    {

        int id;
        string nome;
        private List<Ambiente> ambientes;

        public int Id
        {
            get
            {
                return id;
            }
        }
        public string Nome
        {
            get
            {
                return nome;
            }

        }

        public List<Ambiente> Ambientes
        {
            get
            {
                return ambientes;
            }
        }

        public Usuario(int i, string n)
        {
            id = i;
            nome = n;
            ambientes = new List<Ambiente>();
        }

        public Usuario()
        {
            id = 1;
            nome = "";
            ambientes = new List<Ambiente>();
        }

        public bool ConcederPermissao(Ambiente ambiente)
        {
            bool verificador = false;

            if (ambientes.Count == 0)
            {
                ambientes.Add(ambiente);
            }
            else
            {
                foreach (Ambiente a in ambientes)
                {
                    if (a.Id.Equals(ambiente.Id))
                    {
                        Console.WriteLine("Esta permissão já foi concedida.");
                        verificador = true;
                    }
                    if (verificador == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Permissão concedida.");
                        Console.ResetColor();
                        ambientes.Add(ambiente);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RevogarPermissao(Ambiente ambiente)
        {
            if (ambientes.Count == 0)
            {
                ambientes.Add(ambiente);
            }
            else
            {
                foreach (Ambiente a in Ambientes)
                {
                    if (a.Id.Equals(ambiente.Id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Permissão retirada.");
                        Console.ResetColor();
                        ambientes.Remove(ambiente);
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Permissão não encontrada.");
                    }
                }
            }
            return false;
        }

        public bool Equals(object obj)
        {
            return ((Usuario)obj).id.Equals(this.id);
        }

        public string toString()
        {
            return ("\nID: " + this.id + " Nome: " + this.nome);
        }

        public void listarAmbientes()
        {
            if (ambientes.Count == 0)
            {
                Console.WriteLine("\nNão possui acessos.");
            }
            else
            {
                Console.WriteLine("\nAmbientes que possui acesso:");
                foreach (Ambiente a in this.ambientes)
                {
                    Console.WriteLine(a.toString());
                }
            }
        }
    }
}
