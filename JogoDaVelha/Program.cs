using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace JogoDaVelha
{
    internal class Program
    {
        public static char[,] tabuleiro = new char[3, 3];
        static void Main(string[] args)
        {
            exibirInstrucoes();
            //escolha da opção acima, implementar ainda....
            int escolha = int.Parse(Console.ReadLine());
            if (escolha == 1)
            {
                //logica para vs pessoa
                inicializaTabuleiro();
                while (true)
                {
                    escolhaJogador();
                    exibirTabuleiro();
                    //verificaVencedor();
                }
            }
            else if (escolha == 2)
            {
                //logica para vs computador
            }
            else
            {
                Console.WriteLine("Fim do jogo!");
                return;
            }
        }

        static void exibirInstrucoes()
        {
            Console.WriteLine("========================REGRAS=========================");
            Console.WriteLine("* - Para jogar você deve escolher a linha e a coluna;");
            Console.WriteLine("* - A Linha (L) e a Coluna(C) começando em 1 e terminando em 3.");
            Console.WriteLine("                       C:1   C:2   C:3");
            Console.WriteLine();
            Console.WriteLine("                 L:1    ?     ?     ?");
            Console.WriteLine("                 L:2    ?     ?     ?");
            Console.WriteLine("                 L:3    ?     ?     ?");
            Console.WriteLine();
            Console.WriteLine("========================ESCOLHA========================");
            Console.WriteLine("1 - Jogar contra Jogador");
            Console.WriteLine("2 - Jogar contra Computador");
            Console.WriteLine("* - Para sair digite qualquer tecla!");
        }

        //mostra a matriz inicial (inicializa tabuleiro), preenche todas as posições com '?'
        static void inicializaTabuleiro()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    Console.Write(tabuleiro[i, j] = '?');
                }
                Console.WriteLine();
            }
        }
        //variavel criada para verificar se a jogada é PAR ou IMPAR
        static int numeroJogadas = 0;

        //metodo criado para jogada do Jogador vs Jogador
        static void escolhaJogador()
        {
            Console.WriteLine("Escolha uma linha: ");
            int linha = int.Parse(Console.ReadLine());
            Console.WriteLine("Escolha uma coluna: ");
            int coluna = int.Parse(Console.ReadLine());

            //verifica se a posição esta ocupada e qual jogador esta jogando se X ou O
            //-> PAR para o Jogador X e IMPAR para o Jogador 0
            if (tabuleiro[linha, coluna] == '?')
            {
                if (numeroJogadas % 2 == 0)
                {
                    tabuleiro[linha, coluna] = 'X';
                }
                else
                {
                    tabuleiro[linha, coluna] = '0';
                }
                numeroJogadas++;
            }
            else
            {
                //Console.Clear();
                Console.WriteLine("Posição já ocupada, escolha uma posição novamente");
                exibirTabuleiro();
                escolhaJogador();
                return;
            }
        }

        //metodo criado para exibir o tabuleiro depois do jogador escolher a posição
        static void exibirTabuleiro()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    Console.Write(tabuleiro[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}