using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeituraDeArquivoCSV
{
    public class Pessoa
    {
        private string nome;
        private string sobrenome;           
        private DateTime dtNascimento;
        private string telefone;
        private string email;
        public override string ToString()
        {
            return nome + " " + sobrenome;
        }
        public string Nome
        {
            get { return nome; }
        }

        public string Sobrenome
        {
            get { return sobrenome; }
        }

        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public DateTime DtNascimento
        {
            get { return dtNascimento; }
        }

        public Pessoa(string nome, string sobrenome,
            DateTime dtNascimento, string telefone, string email)
        {
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.dtNascimento = dtNascimento;
            this.telefone = telefone;
            this.email = email;
        }
    }
}
