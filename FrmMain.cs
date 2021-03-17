using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;

namespace Trabalho_Deposito
{
    public partial class FrmMain : Form
    {
        private static SpeechRecognitionEngine engine;
        static string falaValidade = string.Empty;
        static string falaQuantidade = string.Empty;
        static string falaQuantidadeP = string.Empty;



        static bool validadeBool = false;
        static bool quantidadeBool = true;
        public FrmMain()
        {
            InitializeComponent();
        }
        public static void recValidade(object s,SpeechRecognizedEventArgs e)
        {
            if (validadeBool)
            {
                falaQuantidadeP = e.Result.Confidence.ToString();
                falaValidade = e.Result.Text;
            }
        }

        public static void recQuantidade(object s, SpeechRecognizedEventArgs e)
        {
            if (quantidadeBool)
            {
                falaQuantidade = e.Result.Text;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            carregarVocabularioValidade();
            carregarVocabularioQuantidade();
        }


        void carregarVocabularioValidade()
        {
            engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-BR"));
            engine.SetInputToDefaultAudioDevice();
            Choices dia = new Choices("primeiro", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove", "dez", "onze", "doze", "treze", "quatorze", "quinze", "dizesseis", "dizessete", "dizoito", "dizenove", "vinti", "vinti um", "vinti dois", "vinti três", "vinti quatro", "vinti cinco", "vinti seis", "vinti sete", "vinti oito", "vinti nove", "trinta", "trinte um");
            Choices mes = new Choices("do um", "do dois", "do tres", "do quatro", "do cinco", "do seis", "do sete", "do oito", "do nove", "do dez","do onze","do doze");
            Choices ano = new Choices("de dois mil dizenove", "de dois mil e vinte", "de dois mil e vinte um", "de dois mil e vinte dois", "de dois mil e vinte três", "de dois mil e vinte quatro", "de dois mil e vinte cinco");
           
            GrammarBuilder grammarBuilderValidade = new GrammarBuilder();
            
            grammarBuilderValidade.Append(dia);
            grammarBuilderValidade.Append(mes);
            grammarBuilderValidade.Append(ano);

            Grammar grammar = new Grammar(grammarBuilderValidade);
            engine.LoadGrammar(grammar);
            
            engine.RecognizeAsync(RecognizeMode.Multiple);
            
            engine.SpeechRecognized += recValidade;
            
            lblStatus.Text = "Ouvindo Validade";
        }
        void carregarVocabularioQuantidade()
        {
            engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-BR"));
            engine.SetInputToDefaultAudioDevice();

            Choices numero = new Choices("um","dois","três","quatro","cinco","seis","sete","oito","nove","zero");
            

            GrammarBuilder grammarBuilderNumero = new GrammarBuilder();

            grammarBuilderNumero.Append(numero);
           

            Grammar grammar = new Grammar(grammarBuilderNumero);
            engine.LoadGrammar(grammar);

            engine.RecognizeAsync(RecognizeMode.Multiple);

            engine.SpeechRecognized += recQuantidade;

            lblStatus.Text = "Ouvindo Quantidade";
        }
     
        
        
        
        private void timer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(falaValidade)==false)
            {
                ExibirValidade();
            }
            if (string.IsNullOrEmpty(falaQuantidade)==false)
            {
                ExibirQuantidade();
            }




        }
        void ExibirValidade()
        {
            string[] partes = falaValidade.Split(' ');
            string etapaAno=string.Empty;



            //falaValidade = string.Format("{0} {1} {2} {3} {4}", partes[0], partes[1], partes[2], partes[3], partes[4]);
            //txtValidade.Text = falaValidade.Replace(" ", "");
            lblPorcentagem.Text = falaQuantidadeP;
            txtValidade.Text = falaValidade;
            etapaAno = string.Empty;
            falaValidade = string.Empty;
        }
        void ExibirQuantidade()
        {
            txtQuantidade.Text = falaQuantidade;
            falaQuantidade = string.Empty;

        }
    }
}
