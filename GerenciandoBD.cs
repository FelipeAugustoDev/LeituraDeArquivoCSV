using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeituraDeArquivoCSV
{
    public class GerenciandoBD : ManipulacaoDeArquivo
    {
        // Atributo/Propriedades da classe GerenciandoBD
        private List<Pessoa> pessoas;
        /// <summary>
        /// Construindo minha base generica
        /// </summary>
        public GerenciandoBD()
        {
            pessoas = new List<Pessoa>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminho"></param>
        public GerenciandoBD(string caminho)
        {
            //Está chamando o atributo de ManipulacaoDeArquivo
            //Chamado caminho
            base.caminho = caminho;
            pessoas = new List<Pessoa>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        public Pessoa[] adicionar(Pessoa pessoa)
        {           
            pessoas.Add(pessoa);
            return pessoas.ToArray();
        }

        public Pessoa[] remover(Pessoa pessoa)
        {
            pessoas.Remove(pessoa);//vai procurar a pessoa no BD e caso achar retorna true, caso nao ache, retorna false
            return pessoas.ToArray(); //transforma as pessoas em array/vetor
        }

        public Pessoa[] updateItem(Pessoa pessoa)
        {
            for (int i = 0; i < pessoas.Count; i++)
            {
                if (pessoas[i] == pessoa) pessoas[i] = pessoa;
                
            }
            return pessoas.ToArray();
        }

        /// <summary>
        /// Sobreescreve o método obterDados
        /// Com um caminho definido
        /// </summary>
        /// <returns></returns>
        protected override string obterDados()
        {
            //Ele tenta fazer a leitura do arquivo, caso não consiga
            //Impede uma excessão, retornando nulo;
            try
            {
                string textoLido = File.ReadAllText(caminho); //pega o caminho do arquivo
                return textoLido;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void salvarndoArquivo()
        {
            if (base.caminho == null)
            {
                string txt = "Nome;Sobrenome;Data de Nascimento;Telefone;Email\n"; //String texto recebe a estrutura do banco de dados.
                foreach (Pessoa pessoa in pessoas) //Procura pessoa em pessoas
                {
                    txt += pessoa.Nome + ";"; //txt Recebe o que tem na pessoa.Nome
                    txt += pessoa.Sobrenome + ";"; //txt recebe na pessoa
                    txt += pessoa.DtNascimento + ";";
                    txt += pessoa.Telefone + ";";
                    txt += pessoa.Email + "\n";
                }
                base.salvarArquivo();
            }
           
            //Futuro método a ser criado
            salvarArquivo();
            // Estou chamando o método dentro de ManipulacaoDeArquivo
            // chamado salvarArquivo que requer o argumento de texto
            base.salvarArquivo("");
        }

        /// <summary>
        ///  Salvando o arquivo com informacoes e quebras de linha
        /// </summary>
        /// <param name=".txt"> NOVO LOG</param>
        protected override void salvarArquivo()
        {
            SaveFileDialog salvamento = new SaveFileDialog(); //Cria um salvamento do arquivo.
            string txt = "Nome;Sobrenome;Data de Nascimento;Telefone;Email\n"; //String texto recebe a estrutura do banco de dados.
            foreach (Pessoa pessoa in pessoas) //Procura pessoa em pessoas
            {
                txt += pessoa.Nome + ";"; //txt Recebe o que tem na pessoa.Nome
                txt += pessoa.Sobrenome + ";"; //txt recebe na pessoa
                txt += pessoa.DtNascimento + ";";
                txt += pessoa.Telefone + ";";
                txt += pessoa.Email + "\n";
            }
            salvamento.Filter = "Arquivo CSV|*.csv";
            salvamento.Title = "Salvar Arquivo";
            if (salvamento.ShowDialog() != DialogResult.OK && salvamento.FileName == null)
            {
                new Exception("Erros ao salvar o arquivo");
                return;
            }
            if (salvamento.ShowDialog() == DialogResult.Cancel) return;
         
                FileStream abrirArquivo = (FileStream)salvamento.OpenFile();
                StreamWriter salvandoArquivo = new StreamWriter(abrirArquivo);
                salvandoArquivo.WriteLine(txt);
                salvandoArquivo.Close();
                abrirArquivo.Close();

            //base.salvarArquivo();


        }
        /// <summary>
        /// Leitura da base e organização dos dados
        /// </summary>
        /// <returns>Retorna minha base de dados</returns>
        public Pessoa[] leituraBase()
        {
            //Obtendo o arquivo - ManipulacaoDeArquivo
            string textoLido = obterDados();
            //Lanço uma excessão se não foi possivel ler o arquivo
            if (textoLido == null) throw new Exception("" +
                "Erro na leitura do arquivo");
            //Tratando os dados que estão no arquivo
            pessoas.Clear();
            int i = 0;
            foreach (var linha in textoLido.Split('\n'))
            {
                if (linha == "" || linha == "\r") break;
                if (i != 0)
                {
                    //Tratando dos dados
                    string[] tratamento = linha.Split(';');
                    //Criando o objeto de Pessoa
                    Pessoa ps = new Pessoa(tratamento[0], tratamento[1], DateTime.Parse(tratamento[2]), tratamento[3], tratamento[4]);
                    //Adicionando em pessoas as informações do arquivo
                    pessoas.Add(ps);
                }
                i++;
            }
            //Retornando os dados que foram tratados
            return pessoas.ToArray();
        }



        public Pessoa[] leituraBaseComCaminho()
        {
            //Obtendo o arquivo - ManipulacaoDeArquivo
            string textoLido = base.obterDados();
            //Lanço uma excessão se não foi possivel ler o arquivo
            if (textoLido == null) throw new Exception("" +
                "Erro na leitura do arquivo");
            //Tratando os dados que estão no arquivo
            pessoas.Clear();
            int i = 0;
            foreach (var linha in textoLido.Split('\n'))
            {
                if (linha == "" || linha == "\r") break;
                if (i != 0)
                {
                    //Tratando dos dados
                    string[] tratamento = linha.Split(';');
                    //Criando o objeto de Pessoa
                    Pessoa ps = new Pessoa(tratamento[0], tratamento[1], DateTime.Parse(tratamento[2]), tratamento[3], tratamento[4]);
                    //Adicionando em pessoas as informações do arquivo
                    pessoas.Add(ps);
                }
                i++;
            }
            //Retornando os dados que foram tratados
            return pessoas.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        public void limparBD()
        {
            pessoas.Clear();
        }

    }
}
