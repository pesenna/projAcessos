using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAcessos
{
    class Log
    {
        private DateTime dtAcesso;
        private Usuario usuario;
        private bool tipo_acesso;

        public DateTime DtAcesso
        {
            get
            {
                return dtAcesso;
            }

        }

        internal Usuario Usuario
        {
            get
            {
                return usuario;
            }

        }

        public bool Tipo_acesso
        {
            get
            {
                return tipo_acesso;
            }

        }

        public Log(DateTime dt, Usuario usu, bool acesso)
        {
            dtAcesso = dt;
            usuario = usu;
            tipo_acesso = acesso;
        }

        public string toString()
        {
            if (Tipo_acesso == true)
            {
                return ("\nData: " + DtAcesso + " Usuário: " + Usuario.Id.ToString() + "Acesso: Autorizado");
            }
            else
            {
                return ("\nData: " + DtAcesso + " Usuário: " + Usuario.Id.ToString() + "Acesso: Negado");
            }
        }

    }
}
