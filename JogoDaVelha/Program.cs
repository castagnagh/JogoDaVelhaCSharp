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
            //escolha da opção mostrada nas instrucoes
            int escolha = int.Parse(Console.ReadLine());
            if (escolha == 1)
            {
                //opção = 1 (logica para vs pessoa)
                inicializaTabuleiro();
                exibirTabuleiro();
                while (true)
                {
                    escolhaJogador();
                    exibirTabuleiro();
                    if (verificaVencedor('X'))
                    {
                        Console.WriteLine("---------PARABÉNS---------");
                        Console.WriteLine("O jogador X ganhou o jogo");
                        return;
                    }
                    else if (verificaVencedor('O'))
                    {
                        Console.WriteLine("---------PARABÉNS---------");
                        Console.WriteLine("O jogador O ganhou o jogo");
                        return;
                    }
                    else
                    {
                        verificaEmpate();
                    }
                }
            }
            else if (escolha == 2)
            {
                //logica para vs computador. O computador sempre começando com X
                //o computador sempre tem que começar jogando no CENTRO -> tabuleiro[1,1]
                //depois a lógica é ele jogar em um dos cantos (tabuleiro[0,0], ...02, ...20, ... 22
                //depois é analisar se tem dois X em uma linha, coluna ou vertical e completar o 3º X para ser ganhador
                //caso não tenha dois X na mesma linha, onde a 3º posição não esteja completada, por exemplo -> X X O
                //deve analisar o adversário para ver se ele ja tem 2 casas em um linha, coluna ou vertical e BLOQUEAR
                //caso não tenha duas casas o Computador deve jogar em um dos cantos
                //e o processo repetir novamente
                //se tudo isso correr bem, então apartir daqui pode ser jogado qualquer posição aleatória e dara sempre velha
                Console.WriteLine("Jogo da velha iniciado");
                inicializaTabuleiro();
                //primeira jogada é no CENTRO jogada pelo computador -> tabuleiro[1,1]
                tabuleiro[1, 1] = 'X';
                while (true)
                {
                    exibirTabuleiro();
                    //jogador JOGA 
                    jogadorJogaVsPC();
                    //aqui eu faço uma condição pra avaliar o centro (não sei se é necessario)
                    if (tabuleiro[1, 1] != '?')
                    {
                        //2 PASSO - PREENCHER OS CANTOS -> JOGADA DO COMPUTADOR, CONFORME A LÓGICA EXPLICADA A CIMA, O PROXIMO PASSO PARA O COMPUTADOR VENCER
                        //É PREENCHER OS CANTOS 
                        preencheCantos();
                        exibirTabuleiro();
                        
                        jogadorJogaVsPC();
                        //3 PASSO - VERIFICAR SE TEM DOIS ELEMENTOS NA MESMA LINHA, COLUNA E VERTICAL, CASO TENHA ENTÃO COMPLETA COM X A POSIÇÃO VAZIA
                        //verifica se tem dois X na mesma linha, coluna ou vertical e coloca o terceiro X na posição que esta vazia -> (?)
                        verificaLinha();

                        
                        //aqui já é possivel alguem ter ganhado, então verifica o vencedor
                        while (true)
                        {
                            exibirTabuleiro();
                            if (verificaVencedor('X'))
                            {
                                Console.WriteLine("---------DERROTA---------");
                                Console.WriteLine("O Computador ganhou o jogo");
                                return;
                            }
                            else if (verificaVencedor('O'))
                            {
                                Console.WriteLine("---------PARABÉNS---------");
                                Console.WriteLine("Você ganhou o jogo");
                                return;
                            }
                            else
                            {
                                verificaEmpate();
                            }
                            //caso não tenha um vencedor é a vez do Jogador
                            jogadorJogaVsPC();
                            //aqui o computador pode jogar aleatóriamente porque se chegou aqui COM CERTEZA ira dar VELHA
                            jogadaAleatoriaPC();
                            exibirTabuleiro();
                            jogadorJogaVsPC();
                            jogadaAleatoriaPC();
                        }
                    }
                }
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
            Console.WriteLine("* - A Linha (L) e a Coluna(C) começando em 0 e terminando em 2.");
            Console.WriteLine("* - O primeiro a jogar sempre é o X");
            Console.WriteLine("* - Caso escolha jogar contra o Computador ele SEMPRE será o X");
            Console.WriteLine();
            Console.WriteLine("                       C:0   C:1   C:2");
            Console.WriteLine();
            Console.WriteLine("                 L:0    ?     ?     ?");
            Console.WriteLine("                 L:1    ?     ?     ?");
            Console.WriteLine("                 L:2    ?     ?     ?");
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
                    tabuleiro[i, j] = '?';
                }
                Console.WriteLine();
            }
        }
        //variavel criada para verificar se a jogada é PAR ou IMPAR
        static int numeroJogadas = 0;

        //metodo criado para jogada do Jogador vs Jogador
        static void escolhaJogador()
        {
            Console.Write("Escolha uma linha: ");
            int linha = int.Parse(Console.ReadLine());
            Console.Write("Escolha uma coluna: ");
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
                    tabuleiro[linha, coluna] = 'O';
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

        static void escolhaComputador()
        {

            Console.Write("Escolha uma linha: ");
            int linha = int.Parse(Console.ReadLine());
            Console.Write("Escolha uma coluna: ");
            int coluna = int.Parse(Console.ReadLine());

            //verifica se a posição esta ocupada e qual jogador esta jogando se X ou O
            //-> PAR para o Jogador X e IMPAR para o Jogador 0
            if (tabuleiro[linha, coluna] == '?')
            {
                if (numeroJogadas % 2 == 0)
                {
                    tabuleiro[linha, coluna] = 'O';
                }
                else
                {
                    tabuleiro[linha, coluna] = 'X';
                }
                numeroJogadas++;
            }
            else
            {
                //Console.Clear();
                Console.WriteLine("Posição já ocupada, escolha uma posição novamente");
                exibirTabuleiro();
                escolhaComputador();
                return;
            }
        }

        //jogadorJogaVsPC
        static void jogadorJogaVsPC()
        {
            Console.WriteLine("Informe linha: ");
            int linha = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe coluna: ");
            int coluna = int.Parse(Console.ReadLine());
            if (tabuleiro[linha, coluna] == '?')
            {
                tabuleiro[linha, coluna] = 'O';
            }
            else
            {
                jogadorJogaVsPC();
            }
        }

        //PASSO 2 - PREENCHER OS CANTOS
        //(tabuleiro[0,0], ...02, ...20, ... 22
        static void preencheCantos()
        {
            Random random = new Random();
            
            bool cantoPreenchido = false;

            //enquanto o canto não for TRUE ele continua no laço até preencher o canto, 
            //quando preencher o laço termina
            while (!cantoPreenchido)
            {
                //aqui ele multiplica por 2 para ter o numero 0 e 2 sorteados, pois
                // ele pode sortear apenas o 0 ou 1, multiplicado por 2 fica -> 0 *2 = 0 && 1*2 =2
                int linhaSorte = random.Next(2)*2;
                int colunaSorte = random.Next(2)*2;
                if (tabuleiro[linhaSorte, colunaSorte] == '?')
                {
                    tabuleiro[linhaSorte, colunaSorte] = 'X';
                    cantoPreenchido = true;
                }
            }
        }

        //PASSO 3 - Verifica dois X na mesma linha, coluna e vertical para preenchimento do 3º X
        static void verificaLinha()
        {//verifica primeira linha
            if ((tabuleiro[0, 0] == tabuleiro[0, 1] && tabuleiro[0, 2] == '?') || (tabuleiro[0, 0] == tabuleiro[0, 2] && tabuleiro[0, 1] == '?') || (tabuleiro[0, 1] == tabuleiro[0, 2] && tabuleiro[0, 0] == '?'))
            {

                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[0, j] == '?')
                    {
                        tabuleiro[0, j] = 'X';
                        break;
                    }
                }
            }
            //verifica a segunda linha
            else if ((tabuleiro[1, 0] == tabuleiro[1, 1] && tabuleiro[1, 2] == '?') || (tabuleiro[1, 0] == tabuleiro[1, 2] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[1, 2] && tabuleiro[1, 0] == '?'))
            {

                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[1, j] == '?')
                    {
                        tabuleiro[1, j] = 'X';
                        break;
                    }
                }
            }
            //verifica a terceira linha
            else if ((tabuleiro[2, 0] == tabuleiro[2, 1] && tabuleiro[2, 2] == '?') || (tabuleiro[2, 0] == tabuleiro[2, 2] && tabuleiro[2, 1] == '?') || (tabuleiro[2, 1] == tabuleiro[2, 2] && tabuleiro[0, 0] == '?'))
            {

                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[2, j] == '?')
                    {
                        tabuleiro[2, j] = 'X';
                        break;
                    }
                }
            }
            //verifica primeira coluna
            else if ((tabuleiro[0, 0] == tabuleiro[1, 0] && tabuleiro[2, 0] == '?') || (tabuleiro[0, 0] == tabuleiro[2, 0] && tabuleiro[1, 0] == '?') || (tabuleiro[1, 0] == tabuleiro[2, 0] && tabuleiro[0, 0] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, 0] == '?')
                    {
                        tabuleiro[i, 0] = 'X';
                        break;
                    }
                }
            }
            //verifica segunda coluna
            else if ((tabuleiro[0, 1] == tabuleiro[1, 1] && tabuleiro[2, 1] == '?') || (tabuleiro[0, 1] == tabuleiro[2, 1] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 1] && tabuleiro[0, 1] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, 1] == '?')
                    {
                        tabuleiro[i, 1] = 'X';
                        break;
                    }
                }
            }
            //verifica terceira coluna
            else if ((tabuleiro[0, 2] == tabuleiro[1, 2] && tabuleiro[2, 2] == '?') || (tabuleiro[0, 2] == tabuleiro[2, 2] && tabuleiro[1, 2] == '?') || (tabuleiro[1, 2] == tabuleiro[2, 2] && tabuleiro[0, 2] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, 2] == '?')
                    {
                        tabuleiro[i, 2] = 'X';
                        break;
                    }
                }
            }
            //verifica diagonal principal
            else if ((tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[2, 2] == '?') || (tabuleiro[0, 0] == tabuleiro[2, 2] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 2] && tabuleiro[0, 0] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, i] == '?')
                    {
                        tabuleiro[i, i] = 'X';
                        break;
                    }
                }
            }
            //verifica diagonal secundaria
            else if ((tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[2, 0] == '?') || (tabuleiro[0, 2] == tabuleiro[2, 0] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 0] && tabuleiro[0, 2] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, i] == '?')
                    {
                        tabuleiro[i, i] = 'X';
                        break;
                    }
                }
            }
        }

        //pc faz jogada aleatória
        static void jogadaAleatoriaPC()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[i, j] == '?')
                    {
                        tabuleiro[i, j] = 'X';
                        break;
                    }
                }
                break;
            }
        }

        //metodo criado para exibir o tabuleiro depois do jogador escolher a possição
        static void exibirTabuleiro()
        {
            Console.WriteLine();
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    Console.Write(tabuleiro[i, j]);
                    if (j < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("----------");
            }
        }

        static bool verificaVencedor(char simbolo)
        {
            bool status = false;
            //testando as horizontais
            if (tabuleiro[0, 0] == tabuleiro[0, 1] && tabuleiro[0, 1] == tabuleiro[0, 2] && tabuleiro[0, 2] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[1, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[1, 2] && tabuleiro[1, 2] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[2, 0] == tabuleiro[2, 1] && tabuleiro[2, 1] == tabuleiro[2, 2] && tabuleiro[2, 2] == simbolo)
            {
                status = true;
            }
            //testando as verticais
            else if (tabuleiro[0, 0] == tabuleiro[1, 0] && tabuleiro[1, 0] == tabuleiro[2, 0] && tabuleiro[2, 0] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[0, 1] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 1] && tabuleiro[2, 1] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[0, 2] == tabuleiro[1, 2] && tabuleiro[1, 2] == tabuleiro[2, 2] && tabuleiro[2, 2] == simbolo)
            {
                status = true;
            }
            //testando verticais
            else if (tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 2] && tabuleiro[2, 2] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 0] && tabuleiro[2, 0] == simbolo)
            {
                status = true;
            }
            return status;
        }

        static void verificaEmpate()
        {
            //a variavel VERIFICA inicia como TRUE...
            //esse metodo percorre toda a matriz e verifica se a posicão [i,j] == '?'
            bool verifica = true;

            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[i, j] == '?')
                    {
                        //pois
                        verifica = false;
                        break;
                    }
                }

                if (!verifica)
                {
                    break;
                }
            }
            if (verifica)
            {
                Console.WriteLine("--------------EMPATE--------------");
                Console.WriteLine("Bah, não foi dessa vez! Deu VELHA!");
                return;
            }
        }

    }
}