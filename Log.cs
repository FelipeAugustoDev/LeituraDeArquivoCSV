using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeituraDeArquivoCSV
{
    public class Log : ManipulacaoDeArquivo
    {
        const string info = "Info";
        const string warn = "warn";
        const string erro = "erro";
        const string fatal = "fatal";



        public Log()
        {
            string temp = Path.GetTempPath();
            temp = temp.Replace("\\", "\\\\");
            base.caminho = temp + "Log do dia " + DateTime.Today.ToString().Substring(0,10).Replace("/",".") + ".log";
       


            if (!File.Exists(base.caminho))
                salvarArquivo("");

        }




        public void log(int tipo, string log)
        {
            string logando = "";
            switch (tipo)
            {
                case 0:
                    logando += Log.info + " " + log;
                    break;
                case 1:
                    logando += Log.warn + " " + log;
                    break;
                case 2:
                    logando += Log.erro + " " + log;
                    break;
                case 3:
                    logando += Log.fatal + " " + log;
                    break;
                default:
                    return;
                    break;
            }
            salvarArquivo(logando);
        }

        protected override void salvarArquivo(string txt)
        {
            if (File.Exists(base.caminho))
            {
                string logAntigo = File.ReadAllText(base.caminho);
                logAntigo += txt;
                StreamWriter salvarArquivo = new StreamWriter(base.caminho);
                salvarArquivo.WriteLine(logAntigo);
                salvarArquivo.Close();
            }
            else {
                StreamWriter salvarArquivo = new StreamWriter(base.caminho);
                salvarArquivo.WriteLine(IniciandoLog());
                salvarArquivo.Close();
            }


        }


        private string IniciandoLog()
        {
            return "Log Criado as " + DateTime.Now;
        }
    }
}
