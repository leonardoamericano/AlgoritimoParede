using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        public static Program instance;

        static void Main(string[] args)
        {
            instance = instance ?? new Program();

            List<int[]> input = new List<int[]>();
            input.Add(new int[] { 1, 2, 2, 1 });
            input.Add(new int[] { 3, 1, 2 });
            input.Add(new int[] { 1, 3, 2 });
            input.Add(new int[] { 2, 4 });
            input.Add(new int[] { 3, 1, 2 });
            input.Add(new int[] { 1, 3, 1, 1 });

            int menorcorte = ContarTijolosCortados(new Parede(input));
            Console.WriteLine("");
            Console.WriteLine($"A menor quantidade de tijolos cortados é : {menorcorte}");
            Console.WriteLine("");

            var resultadoEsperado = 2;

            Console.WriteLine(menorcorte == resultadoEsperado ? "CORRETO" : $"INCORRETO {menorcorte} != {resultadoEsperado}");

            Console.ReadKey();
        }

        static int ContarTijolosCortados(Parede parede)
        {
            Console.WriteLine("");
            Console.WriteLine("Parede convertida para linhas de corte com tijolo de tamanho {1,0}");
            parede.Print();

            List<Fileira> myList = parede.Fileiras;

            List<Resultado> lista = new List<Resultado>();

            var linhas = parede.Fileiras.Count;

            Console.WriteLine("Pivo da parede para facilitar a contagem");
            for (int j = 0; j < 11; j++)
            {
                Resultado res = new Resultado();
                res.Coluna = j;

                for (int i = 0; i < linhas; i++)
                {
                    Console.Write(parede.Fileiras[i].Tijiolos[j] + ",");
                    if (parede.Fileiras[i].Tijiolos[j] == 1)
                        res.tijoloCortado++;
                }
                Console.WriteLine("");
                lista.Add(res);
            }

            var query = (from menosCortes in lista
                         orderby menosCortes.tijoloCortado
                         select menosCortes.tijoloCortado).FirstOrDefault();

            return query;
        }
    }
    public class Resultado
    {
        public int Coluna { get; set; }
        public int tijoloCortado { get; set; }
    }

    public class Tijolo
    {
        public Tijolo(int tam, int col)
        {
            List<int> laux = new List<int>();
            if (tam < 1)
                throw new Exception("O tijolo não pode ser menor ou igual a zero");

            if (tam > 1)
            {
                for (int i = 0; i < tam - 1; i++)
                {
                    laux.Add(1);
                    laux.Add(1);
                }
            }
            laux.Add(1);
            laux.Add(0);

            this.tamanho = laux.ToArray();
            this.coluna = col;
        }

        public int[] tamanho { get; private set; }
        public int coluna { get; private set; }
    }

    public class Fileira
    {
        public Fileira()
        {
            this.fileira = new List<Tijolo>();
            _tijolos = new List<int>();
        }

        private List<int> _tijolos;

        public List<Tijolo> fileira { get; private set; }
        public int linha { get; set; }
        public List<int> Tijiolos
        {
            get
            {
                int aux = 1;
                foreach (Tijolo item in fileira)
                {
                    foreach (int tam in item.tamanho)
                    {
                        if (aux < 12)
                            _tijolos.Add(tam);

                        aux++;
                    }
                }
                return _tijolos;
            }
        }
    }

    public class Parede
    {
        public Parede(List<int[]> parede)
        {
            this.Fileiras = new List<Fileira>();

            int lin = 1;

            Console.WriteLine("Input: [");
            foreach (var linha in parede)
            {
                Fileira fila = new Fileira();
                int col = 1;
                string strLinha = "[";

                foreach (var tij in linha)
                {
                    strLinha += tij + ", ";
                    ; fila.fileira.Add(new Tijolo(tij, col));
                    col++;
                }
                strLinha += "]";
                strLinha = strLinha.Replace(", ]", "]");

                fila.linha = lin;
                this.Fileiras.Add(fila);
                lin++;
                Console.WriteLine(strLinha);
            }
            Console.WriteLine("]");
            Console.WriteLine("");
        }

        public List<Fileira> Fileiras { get; set; }

        public void Print()
        {
            for (int i = 0; i < this.Fileiras.Count; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Console.Write(this.Fileiras[i].Tijiolos[j] + ",");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
}