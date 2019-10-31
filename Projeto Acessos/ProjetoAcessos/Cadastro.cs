using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAcessos
{
    class Cadastro
    {

        private List<Usuario> usuarios = new List<Usuario>();
        private List<Ambiente> ambientes = new List<Ambiente>();
        Usuario u = new Usuario();
        internal List<Usuario> Usuarios
        {
            get
            {
                return usuarios;
            }

        }

        public void AdicionarUsuario(Usuario usuario)
        {
            bool verificador = false;

            foreach (Usuario u in usuarios)
            {
                if (u.Id.Equals(usuario.Id))
                {
                    Console.WriteLine("\nUsuário já existe!");
                    verificador = true;
                    break;
                }
            }
            if (verificador == false)
            {
                usuarios.Add(usuario);
                Console.WriteLine("\nUsuário cadastradao com sucesso!");
            }

        }
        public bool RemoverUsuario(Usuario usuario)
        {
            if (usuario.Ambientes.Count() == 0)
            {
                this.usuarios.Remove(usuario);
                Conexao.deleteUsuario(usuario);
                Console.WriteLine("\nRemovido com sucesso.");
            }
            else
            {
                Console.WriteLine("Impossível remover, existem permissões para este usuário.");
                return false;
            }

            return true;
        }

        public Usuario PesquisarUsuario(Usuario usuario)
        {
            bool verificador = false;
            foreach (Usuario u in Usuarios)
            {
                if (u.Id.Equals(usuario.Id))
                {
                    Console.WriteLine("\nUsuário encontrado: " + u.toString());
                    verificador = true;
                    return u;
                }
                if (!u.Id.Equals(usuario.Id))
                {
                    verificador = false;
                }
            }
            if (verificador == false)
                Console.WriteLine("\nUsuário não encontrado ");

            return usuario;
        }
        public void AdicionarAmbiente(Ambiente ambiente)
        {
            bool verificador = false;
            foreach (Ambiente a in ambientes)
            {
                if (a.Id.Equals(ambiente.Id))
                {
                    Console.WriteLine("\nAmbiente já existe!");
                    verificador = true;
                    break;
                }
            }
            if (verificador == false)
            {
                ambientes.Add(ambiente);
                Console.WriteLine("\nAmbiente cadastrado com sucesso!");
            }


        }
        public bool RemoverAmbiente(Ambiente ambiente)
        {

            if (usuarios.Count() == 0)
            {
                this.ambientes.Remove(ambiente);
                Console.WriteLine("\nAmbiente excluido com sucesso");
                return true;
            }
            else
            {
                Console.WriteLine("\nAmbiente não pode ser apagado");
                return false;
            }
        }

        public Ambiente PesquisarAmbiente(Ambiente ambiente)
        {
            bool verificador = false;
            foreach (Ambiente a in this.ambientes)
            {
                if (a.Id.Equals(ambiente.Id))
                {
                    Console.WriteLine("\nAmbiente encontrado: " + a.toString());
                    verificador = true;
                    return a;
                }
                if (!u.Id.Equals(ambiente.Id))
                {
                    verificador = false;
                }
            }
            if (verificador == false)
                Console.WriteLine("\nAmbiente não encontrado ");
            return ambiente;
        }

        public void Upload(Cadastro pCadastro)
        {
            foreach (Usuario pUsuario in pCadastro.Usuarios)
            {
                Conexao.setUsuario(pUsuario);
                foreach (Ambiente pAmbiente in pUsuario.Ambientes)
                {
                    Conexao.setAmbiente(pAmbiente);
                    Conexao.setPermissoes(pUsuario, pAmbiente);
                    foreach (Log pLog in pAmbiente.Logs)
                    {
                        Conexao.setLog(pLog, pAmbiente);
                    }
                }
            }
        }
        public void Download()
        {
            Conexao.getUsuario(this);
            Conexao.getAmbiente(this);
            Conexao.getPermissoes(this);
            Conexao.getLogs(this);
        }
    }
}
